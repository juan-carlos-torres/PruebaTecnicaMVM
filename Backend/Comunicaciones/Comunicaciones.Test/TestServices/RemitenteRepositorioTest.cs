using Comunicaciones.BI.Services;
using Comunicaciones.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Comunicaciones.Test.TestServices
{
    public class RemitenteRepositorioTest
    {
        private readonly RemitenteRepositorio _remitenteRepositorio;

        public RemitenteRepositorioTest()
        {
            string cadenaConexion = "Server=localhost;Database=DBCOMUNICACIONES;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true";
            var services = new ServiceCollection();
            services.AddDbContext<DBCOMUNICACIONESContext>(options => options.UseSqlServer(cadenaConexion));
            services.AddScoped<RemitenteRepositorio, RemitenteRepositorio>();
            var serviceProvider = services.BuildServiceProvider();
            _remitenteRepositorio = serviceProvider.GetService<RemitenteRepositorio>();
        }


        [Fact]
        public async Task ListadoRemitentesTest()
        {
            var listadoRemitentes = await _remitenteRepositorio.ListadoRemitentes();
            listadoRemitentes.Count.ShouldBe(1);
            listadoRemitentes[0].Nombres.ShouldBe("Daniel");
            listadoRemitentes[0].Apellidos.ShouldBe("Goméz");
            listadoRemitentes[0].Identificacion.ShouldBe("123889481");
        }


        [Fact]
        public async Task InformacionRemitenteTest()
        {
            var idRemitente = Guid.Parse("03207FC4-E4C5-4B1A-8979-946A90B1F3DD");
            var informacionRemitente = await _remitenteRepositorio.InformacionRemitente(idRemitente);
            informacionRemitente.Nombres.ShouldBe("Daniel");
            informacionRemitente.Apellidos.ShouldBe("Goméz");
            informacionRemitente.Identificacion.ShouldBe("123889481");
        }
    }
}
