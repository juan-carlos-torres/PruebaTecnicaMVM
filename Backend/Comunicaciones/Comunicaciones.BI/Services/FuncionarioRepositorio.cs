using Comunicaciones.BI.DTORequest.Funcionario;
using Comunicaciones.BI.DTOResponse.Funcionario;
using Comunicaciones.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.Services
{
    public class FuncionarioRepositorio
    {

        private readonly DBCOMUNICACIONESContext _dbContext;
        public FuncionarioRepositorio(DBCOMUNICACIONESContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Carga el listado de los funcionarios que hay en el momento
        /// </summary>
        /// <returns></returns>
        public async Task<List<FuncionarioDetalleResponse>> ListadoFuncionarios()
        {
            try
            {
                var listadoFuncionarios = await _dbContext.Funcionarios
                                                    .Select(f => new FuncionarioDetalleResponse
                                                    {
                                                        Id = f.FunId,
                                                        Rol = f.FunIdRolNavigation.RolNombre,
                                                        Nombres = f.FunNombres,
                                                        Apellidos = f.FunApellidos,
                                                        Identificacion = f.FunIdentificacion,
                                                    })
                                                    .ToListAsync();

                return listadoFuncionarios;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Crea un nuevo funcionario con la información suministrada
        /// </summary>
        /// <param name="crearFuncionarioRequest"></param>
        /// <returns></returns>
        public async Task CrearFuncionario(CrearFuncionarioRequest crearFuncionarioRequest)
        {
            try
            {
                var nuevoFuncionario = new Funcionario
                {
                    FunId = Guid.NewGuid(),
                    FunIdRol = crearFuncionarioRequest.IdRol,
                    FunNombres = crearFuncionarioRequest.Nombres,
                    FunApellidos = crearFuncionarioRequest.Apellidos,
                    FunIdentificacion = crearFuncionarioRequest.Identificacion,
                    FunActivo = true
                };

                await _dbContext.AddAsync(nuevoFuncionario);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Consulta la información del funcionario para poderse editar
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        public async Task<InformacionFuncionarioResponse> InformacionFuncionario(Guid idFuncionario)
        {
            try
            {
                var funcionario = await _dbContext.Funcionarios.FindAsync(idFuncionario);

                var informacionFuncionario = new InformacionFuncionarioResponse
                {
                    IdRol = funcionario.FunIdRol,
                    Nombres = funcionario.FunNombres,
                    Apellidos = funcionario.FunApellidos,
                    Identificacion = funcionario.FunIdentificacion,
                };

                return informacionFuncionario;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Edita la información del funcionario seleccionado
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <param name="editarFuncionarioRequest"></param>
        /// <returns></returns>
        public async Task EditarFuncionario(Guid idFuncionario, EditarFuncionarioRequest editarFuncionarioRequest)
        {
            try
            {
                var funcionario = await _dbContext.Funcionarios.FindAsync(idFuncionario);
                funcionario.FunNombres = editarFuncionarioRequest.Nombres;
                funcionario.FunApellidos = editarFuncionarioRequest.Apellidos;
                funcionario.FunIdentificacion = editarFuncionarioRequest.Identificacion;
                funcionario.FunActivo = editarFuncionarioRequest.Activo;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Elimina el funcionario junto con sus dependencias
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        public async Task EliminarFuncionario(Guid idFuncionario)
        {
            try
            {
                var funcionario = await _dbContext.Funcionarios
                                            .Include(f => f.Correspondencia)
                                                .ThenInclude(r => r.CorrespondenciaAuditoria)
                                            .FirstOrDefaultAsync(f => f.FunId == idFuncionario);
                _dbContext.RemoveRange(funcionario.Correspondencia.SelectMany(c => c.CorrespondenciaAuditoria));
                _dbContext.RemoveRange(funcionario.Correspondencia);
                _dbContext.Remove(funcionario);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }
    }
}
