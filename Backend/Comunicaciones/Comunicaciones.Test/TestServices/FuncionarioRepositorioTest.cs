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
    public class FuncionarioRepositorioTest
    {
        private readonly FuncionarioRepositorio _funcionarioRepositorio;

        public FuncionarioRepositorioTest()
        {
            string cadenaConexion = "Server=localhost;Database=DBCOMUNICACIONES;User Id=sa;Password=sa2019%;MultipleActiveResultSets=true";
            var services = new ServiceCollection();
            services.AddDbContext<DBCOMUNICACIONESContext>(options => options.UseSqlServer(cadenaConexion));
            services.AddScoped<FuncionarioRepositorio, FuncionarioRepositorio>();
            var serviceProvider = services.BuildServiceProvider();
            _funcionarioRepositorio = serviceProvider.GetService<FuncionarioRepositorio>();
        }


        [Fact]
        public async Task ListadoFuncionariosTest()
        {
            var listadoFuncionarios = await _funcionarioRepositorio.ListadoFuncionarios();
            listadoFuncionarios.Count.ShouldBe(2);
            listadoFuncionarios[0].Nombres.ShouldBe("Juan Carlos");
            listadoFuncionarios[0].Apellidos.ShouldBe("Torres Torres");
            listadoFuncionarios[0].Identificacion.ShouldBe("1001053192");
            listadoFuncionarios[0].Rol.ShouldBe("Destinatario");

            listadoFuncionarios[1].Nombres.ShouldBe("Funcionario");
            listadoFuncionarios[1].Apellidos.ShouldBe("Prueba");
            listadoFuncionarios[1].Identificacion.ShouldBe("12345");
            listadoFuncionarios[1].Rol.ShouldBe("Destinatario");
        }


        [Fact]
        public async Task InformacionFuncionarioTest()
        {
            var idFuncionario = Guid.Parse("61D07337-52E3-45DD-969E-B467566592F2");
            var informacionFuncionario = await _funcionarioRepositorio.InformacionFuncionario(idFuncionario);
            informacionFuncionario.Nombres.ShouldBe("Juan Carlos");
            informacionFuncionario.Apellidos.ShouldBe("Torres Torres");
            informacionFuncionario.Identificacion.ShouldBe("1001053192");
            informacionFuncionario.IdRol.ShouldBe(Guid.Parse("4b3f74bc-aa30-49df-8bf4-8a3b5d9ffa0c"));
        }
    }
}
