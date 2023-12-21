using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using sistemaVentas.datos.dbContext;
using Microsoft.EntityFrameworkCore;
using sistemaVentas.datos.Interfaces;
using sistemaVentas.datos.Implementacion;
//using sistemaVentas.negocios.Implementacion;
//using sistemaVentas.negocios.Implementacion;



namespace sistemaVentas.control
{
    public static class Dependencia
    {
        public static void InyectarDependencia(this IServiceCollection services,IConfiguration configuration) 
        {
            services.AddDbContext<DbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("CadenaSQL"));
            });

            services.AddTransient(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            services.AddScoped<IVentaRepository, VentaRepository>();


        }
    }
}
