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
    public class CorrespondenciaRepositorioTest
    {
        private readonly CorrespondenciaRepositorio _correspondenciaRepositorio;


        public CorrespondenciaRepositorioTest()
        {
            string cadenaConexion = "Server=localhost;Database=DBCOMUNICACIONES;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true";
            var services = new ServiceCollection();
            services.AddDbContext<DBCOMUNICACIONESContext>(options => options.UseSqlServer(cadenaConexion));
            services.AddScoped<CorrespondenciaRepositorio, CorrespondenciaRepositorio>();
            var serviceProvider = services.BuildServiceProvider();
            _correspondenciaRepositorio = serviceProvider.GetService<CorrespondenciaRepositorio>();
        }


        [Fact]
        public async Task ListadoCorrespondenciasTest()
        {
            var listadoCorrespondencias = await _correspondenciaRepositorio.ListadoCorrespondencias();
            
            listadoCorrespondencias[0].Asunto.ShouldBe("Asunto de prueba de correspondencia");
            listadoCorrespondencias[0].Consecutivo.ShouldBe("CI00000005");
            listadoCorrespondencias[0].Descripcion.ShouldBe("Descripción de prueba");

            listadoCorrespondencias.Count.ShouldBe(2);
            listadoCorrespondencias[1].Asunto.ShouldBe("Validación documentos");
            listadoCorrespondencias[1].Consecutivo.ShouldBe("CI00000006");
            listadoCorrespondencias[1].Descripcion.ShouldBe("Se deben de verificar los documentos informados");
        }


        [Fact]
        public async Task InformacionCorrespondenciaTest()
        {
            var idCorrespondencia = Guid.Parse("3E0FF6E0-4042-4603-86CA-B1FE572BFB5E");
            var informacionCorrespondencia = await _correspondenciaRepositorio.InformacionCorrespondencia(idCorrespondencia);
            informacionCorrespondencia.Asunto.ShouldBe("Validación documentos");
            informacionCorrespondencia.Consecutivo.ShouldBe("CI00000006");
            informacionCorrespondencia.Descripcion.ShouldBe("Se deben de verificar los documentos informados");
            informacionCorrespondencia.IdDestinatario.ShouldBe(Guid.Parse("67521281-a717-4e6a-85ac-e8c01fefe505"));
            informacionCorrespondencia.IdTipoCorrespondencia.ShouldBe(Guid.Parse("95a3ff66-8946-4432-abc9-918e7e68bb25"));
        }

    }
}
