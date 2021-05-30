using Comunicaciones.BI.DTORequest.Remitente;
using Comunicaciones.BI.DTOResponse.Remitente;
using Comunicaciones.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comunicaciones.BI.Services
{
    public class RemitenteRepositorio
    {
        private readonly DBCOMUNICACIONESContext _dbContext;
        public RemitenteRepositorio(DBCOMUNICACIONESContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Carga el listado de los remitentes que hay en el momento guardados en la DB
        /// </summary>
        /// <returns></returns>
        public async Task<List<RemitenteDetalleResponse>> ListadoRemitentes()
        {
            try
            {
                var listadoRemitentes = await _dbContext.Remitentes
                                                    .Select(r => new RemitenteDetalleResponse
                                                    {
                                                        Id = r.RemId,
                                                        Nombres = r.RemNombres,
                                                        Apellidos = r.RemApellidos,
                                                        Identificacion = r.RemIdentificacion,
                                                        FechaCreacion = r.RemFechaCreacion.ToString("dd/MM/yyyy hh:mm tt")
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
        /// Crea un nuevo remitente con la información suministrada
        /// </summary>
        /// <param name="crearRemitenteRequest"></param>
        /// <returns></returns>
        public async Task CrearRemitente(CrearRemitenteRequest crearRemitenteRequest)
        {
            try
            {
                var nuevoRemitente = new Remitente
                {
                    RemId = Guid.NewGuid(),
                    RemNombres = crearRemitenteRequest.Nombres,
                    RemApellidos = crearRemitenteRequest.Apellidos,
                    RemIdentificacion = crearRemitenteRequest.Identificacion,
                    RemFechaCreacion = DateTime.Now
                };

                await _dbContext.AddAsync(nuevoRemitente);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Carga la información del remitente para poder editarla después
        /// </summary>
        /// <param name="idRemitente"></param>
        /// <returns></returns>
        public async Task<InformacionRemitenteResponse> InformacionRemitente(Guid idRemitente)
        {
            try
            {
                var remitente = await _dbContext.Remitentes.FindAsync(idRemitente);

                var informacionRemitente = new InformacionRemitenteResponse
                {
                    Nombres = remitente.RemNombres,
                    Apellidos = remitente.RemApellidos,
                    Identificacion = remitente.RemIdentificacion
                };

                return informacionRemitente;
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


        /// <summary>
        /// Actualiza la información de un remitente con la que envíe el usuario
        /// </summary>
        /// <param name="idRemitente"></param>
        /// <param name="editarRemitenteRequest"></param>
        /// <returns></returns>
        public async Task EditarRemitente(Guid idRemitente, EditarRemitenteRequest editarRemitenteRequest)
        {
            try
            {
                var remitente = await _dbContext.Remitentes.FindAsync(idRemitente);
                remitente.RemNombres = editarRemitenteRequest.Nombres;
                remitente.RemApellidos = editarRemitenteRequest.Apellidos;
                remitente.RemIdentificacion = editarRemitenteRequest.Identificacion;
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }

        /// <summary>
        /// Elimina el remitente con todas sus dependencias
        /// </summary>
        /// <param name="idRemitente"></param>
        /// <returns></returns>
        public async Task EliminarRemitente(Guid idRemitente)
        {
            try
            {
                var Remitente = await _dbContext.Remitentes
                                            .Include(r => r.RemitenteAuditoria)
                                            .Include(r => r.Correspondencia)
                                                .ThenInclude(c => c.CorrespondenciaAuditoria)
                                            .FirstOrDefaultAsync(r => r.RemId == idRemitente);
                _dbContext.RemoveRange(Remitente.RemitenteAuditoria);
                _dbContext.RemoveRange(Remitente.Correspondencia.SelectMany(c => c.CorrespondenciaAuditoria));
                _dbContext.RemoveRange(Remitente.Correspondencia);
                _dbContext.Remove(Remitente);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception err)
            {
                throw new Exception(err.ToString());
            }
        }


    }
}
