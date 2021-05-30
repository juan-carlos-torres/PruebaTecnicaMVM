using Comunicaciones.BI.DTORequest.Correspondencia;
using Comunicaciones.BI.DTOResponse.Correspondencia;
using Comunicaciones.DAL.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.Services
{
    public class CorrespondenciaRepositorio
    {
        private readonly DBCOMUNICACIONESContext _dbContext;
        public CorrespondenciaRepositorio(DBCOMUNICACIONESContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Consulta el listado de las correspondencias ordenandolas por fecha
        /// </summary>
        /// <returns></returns>
        public async Task<List<CorrespondenciaDetalleResponse>> ListadoCorrespondencias()
        {
            try
            {
                var listadoCorrespondencias = await _dbContext.Correspondencia
                                                    .OrderByDescending(c => c.CorFechaCreacion)
                                                    .Select(c => new CorrespondenciaDetalleResponse
                                                    {
                                                        Id = c.CorId,
                                                        Consecutivo = c.CorConsecutivo,
                                                        TipoCorrespondencia = c.CorIdTipoCorrespondenciaNavigation.TcoNombre,
                                                        NombresRemitente = $"{c.CorIdRemitenteNavigation.RemNombres} {c.CorIdRemitenteNavigation.RemApellidos}",
                                                        NombresDestinatario = $"{c.CorIdDestinatarioNavigation.FunNombres} {c.CorIdDestinatarioNavigation.FunApellidos}",
                                                        Asunto = c.CorAsunto,
                                                        Descripcion = c.CorDescripcion,
                                                        FechaCreacion = c.CorFechaCreacion.ToString("dd/MM/yyyy hh:mm tt")
                                                    })
                                                    .ToListAsync();

                return listadoCorrespondencias;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Llama al SP para poder crear una nueva correspondencia con la información correspondiente
        /// </summary>
        /// <param name="crearCorrespondenciaRequest"></param>
        /// <returns></returns>
        public async Task CrearCorrespondencia(CrearCorrespondenciaRequest crearCorrespondenciaRequest)
        {
            try
            {

                var query = "EXEC SP_CREACION_CORRESPONDENCIA @id_tipo_correspondencia, @id_remitente, @id_destinatario, @asunto, @descripcion";
                var parametros = new object[]
                {
                    new SqlParameter("@id_tipo_correspondencia", crearCorrespondenciaRequest.IdTipoCorrespondencia),
                    new SqlParameter("@id_remitente",crearCorrespondenciaRequest.IdRemitente),
                    new SqlParameter("@id_destinatario",crearCorrespondenciaRequest.IdDestinatario),
                    new SqlParameter("@asunto",crearCorrespondenciaRequest.Asunto),
                    new SqlParameter("@descripcion",crearCorrespondenciaRequest.Descripcion)
                };

                await _dbContext.Database.ExecuteSqlRawAsync(query, parametros);
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Consulta la correspondencia para cargar la información por defecto cuando se va a editar
        /// </summary>
        /// <param name="idCorrespondencia"></param>
        /// <returns></returns>
        public async Task<InformacionCorrespondenciaResponse> InformacionCorrespondencia(Guid idCorrespondencia)
        {
            try
            {
                var correspondencia = await _dbContext.Correspondencia.FindAsync(idCorrespondencia);

                var informacionCorrespondencia = new InformacionCorrespondenciaResponse
                {
                    Consecutivo = correspondencia.CorConsecutivo,
                    IdTipoCorrespondencia = correspondencia.CorIdTipoCorrespondencia,
                    IdRemitente = correspondencia.CorIdRemitente,
                    IdDestinatario = correspondencia.CorIdDestinatario,
                    Asunto = correspondencia.CorAsunto,
                    Descripcion = correspondencia.CorDescripcion
                };

                return informacionCorrespondencia;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Actualiza la información de la correspondencia indicada
        /// </summary>
        /// <param name="idCorrespondencia"></param>
        /// <param name="actualizarCorrespondenciaRequest"></param>
        /// <returns></returns>
        public async Task ActualizarCorrespondencia(Guid idCorrespondencia, ActualizarCorrespondenciaRequest actualizarCorrespondenciaRequest)
        {
            try
            {
                var correspondencia = await _dbContext.Correspondencia.FindAsync(idCorrespondencia);
                correspondencia.CorAsunto = actualizarCorrespondenciaRequest.Asunto;
                correspondencia.CorDescripcion = actualizarCorrespondenciaRequest.Descripcion;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Elimina la correspondencia junto con todas sus relaciones
        /// </summary>
        /// <param name="idCorrespondencia"></param>
        /// <returns></returns>
        public async Task EliminarCorrespondencia(Guid idCorrespondencia)
        {
            try
            {
                var correspondencia = await _dbContext.Correspondencia
                                            .Include(c => c.CorrespondenciaAuditoria)
                                            .FirstOrDefaultAsync(c => c.CorId == idCorrespondencia);
                _dbContext.RemoveRange(correspondencia.CorrespondenciaAuditoria);
                _dbContext.Remove(correspondencia);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Carga el listado de los tipos de correspondencia para poder crear y/o editar una correspondencia
        /// </summary>
        /// <returns></returns>
        public async Task<List<TipoCorrespondenciaResponse>> ListadoTiposCorrespondencia()
        {
            try
            {
                var listadoTiposCorrespondencia = await _dbContext.TipoCorrespondencia
                                                    .Where(t => t.TcoActivo)
                                                    .Select(t => new TipoCorrespondenciaResponse
                                                    {
                                                        Id = t.TcoId,
                                                        Nombre = t.TcoNombre
                                                    })
                                                    .ToListAsync();

                return listadoTiposCorrespondencia;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Carga el listado de los remitentes para poder crear y/o editar una correspondencia
        /// </summary>
        /// <returns></returns>
        public async Task<List<RemitenteResponse>> ListadoRemitentes()
        {
            try
            {
                var listadoRemitentes = await _dbContext.Remitentes
                                                    .Select(r => new RemitenteResponse
                                                    {
                                                        Id = r.RemId,
                                                        Nombre = $"{r.RemNombres} {r.RemApellidos}"
                                                    })
                                                    .ToListAsync();

                return listadoRemitentes;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Carga el listado de los remitentes activos para poder crear y/o editar una correspondencia
        /// </summary>
        /// <returns></returns>
        public async Task<List<DestinatarioResponse>> ListadoDestinatarios()
        {
            try
            {
                var listadoDestinatarios = await _dbContext.Funcionarios
                                                    .Where(f => f.FunActivo)
                                                    .Select(f => new DestinatarioResponse
                                                    {
                                                        Id = f.FunId,
                                                        Nombre = $"{f.FunNombres} {f.FunApellidos}"
                                                    })
                                                    .ToListAsync();

                return listadoDestinatarios;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }
    }
}
