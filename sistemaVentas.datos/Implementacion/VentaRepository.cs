using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using sistemaVentas.datos.dbContext;
using sistemaVentas.datos.Interfaces;
using sistemaVentas.entity;

namespace sistemaVentas.datos.Implementacion
{
    public class VentaRepository : GenericRepository<Venta>, IVentaRepository
    {
        private readonly DbventaContext _dbContext;

        public VentaRepository(DbventaContext dbContext): base (dbContext)
            {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta entidad)
        {
            Venta VentaGenerada = new Venta();

            using (var transaccion = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach ( DetalleVenta dv in entidad.DetalleVenta)
                    {
                        Producto productoEncontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();

                        productoEncontrado.Stock = productoEncontrado.Stock - dv.Cantidad;
                        _dbContext.Productos.Update(productoEncontrado);

                    }
                    await _dbContext.SaveChangesAsync();

                    NumeroCorrelativo correlativo = _dbContext.NumeroCorrelativos.Where(n => n.Gestion == "venta").First();
                    correlativo.UltimoNumero = correlativo.UltimoNumero + 1;
                    correlativo.FechaActualizacion = DateTime.Now;
                    _dbContext.NumeroCorrelativos.Update(correlativo);
                    await _dbContext.SaveChangesAsync();

                    string ceros = string.Concat(Enumerable.Repeat("0", correlativo.CantidadDigitos.Value));
                    string numVenta = ceros + correlativo.UltimoNumero.ToString();

                    numVenta = numVenta.Substring(numVenta.Length - correlativo.CantidadDigitos.Value, correlativo.CantidadDigitos.Value);
                    entidad.NumeroVenta = numVenta;

                    await _dbContext.Venta.AddAsync(entidad);
                    await _dbContext.SaveChangesAsync();

                    VentaGenerada = entidad;

                    transaccion.Commit();



                } catch (Exception ex)
                {
                    transaccion.Rollback();
                    throw ex;

                }
            }

            return VentaGenerada;
        }

        public async Task<List<DetalleVenta>> Reporte(DateTime FechaInicio, DateTime FechaFin)
        {
            List<DetalleVenta> listaResumen = await _dbContext.DetalleVenta
                .Include(v =>v.IdVentaNavigation)
                .ThenInclude(u =>u.IdUsuarioNavigation)
                .Include(v =>v.IdVentaNavigation)
                .ThenInclude(tdv => tdv.IdTipoDocumentoVentaNavigation)
                .Where(dv => dv.IdVentaNavigation.FechaRegistro.Value.Date >= FechaInicio.Date &&
                dv.IdVentaNavigation.FechaRegistro.Value.Date <= FechaFin.Date).ToListAsync();

            return listaResumen;
        }
    }
}
    