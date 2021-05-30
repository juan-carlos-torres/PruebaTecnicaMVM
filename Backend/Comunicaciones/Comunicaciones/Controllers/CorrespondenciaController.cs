using Comunicaciones.BI.DTORequest.Correspondencia;
using Comunicaciones.BI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Comunicaciones.Controllers
{
    [Route("[controller]")]
    public class CorrespondenciaController : ControllerBase
    {

        private readonly CorrespondenciaRepositorio _correspondenciaRepositorio;
        private readonly ILogger<CorrespondenciaController> _logger;

        public CorrespondenciaController(CorrespondenciaRepositorio correspondenciaRepositorio, ILogger<CorrespondenciaController> logger)
        {
            _correspondenciaRepositorio = correspondenciaRepositorio;
            _logger = logger;
        }


        /// <summary>
        /// Carga el listado de todas las correspondencias que hay en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoCorrespondencias()
        {
            try
            {
                var listadoCorrespondencias = await _correspondenciaRepositorio.ListadoCorrespondencias();
                return Ok(listadoCorrespondencias);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar las correspondencias""");
            }
        }

        /// <summary>
        /// Crea la nueva correspondencia con la información que haya enviado el usuario
        /// </summary>
        /// <param name="crearCorrespondenciaRequest"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearCorrespondencia([FromBody] CrearCorrespondenciaRequest crearCorrespondenciaRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _correspondenciaRepositorio.CrearCorrespondencia(crearCorrespondenciaRequest);
                    return Ok(@"""Se ha creado la correspondencia correctamente""");
                }
                else
                {
                    var listadoErrores = ModelState
                                        .Where(y => y.Value.ValidationState == ModelValidationState.Invalid)
                                        .Select(y => new
                                        {
                                            Campo = new string(y.Key?.ToArray()),
                                            Error = y.Value?.Errors?.FirstOrDefault()?.ErrorMessage
                                        })
                                        .ToList();

                    return StatusCode(StatusCodes.Status400BadRequest, $@"""Por favor valide la información enviada: {JsonConvert.SerializeObject(listadoErrores)}""");
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al crear la correspondencia""");
            }
        }

        /// <summary>
        /// Consulta la información de la correspondencia para cuando se va a editar
        /// </summary>
        /// <param name="idCorrespondencia"></param>
        /// <returns></returns>
        [HttpGet("[Action]/{idCorrespondencia}")]
        public async Task<IActionResult> InformacionCorrespondencia(Guid idCorrespondencia)
        {
            try
            {
                var informacionCorrespondencia = await _correspondenciaRepositorio.InformacionCorrespondencia(idCorrespondencia);
                return Ok(informacionCorrespondencia);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar la información de la correspondencia""");
            }
        }

        /// <summary>
        /// Actualiza la información de la correspondencia
        /// </summary>
        /// <param name="idCorrespondencia"></param>
        /// <param name="actualizarCorrespondencia"></param>
        /// <returns></returns>
        [HttpPut("[Action]/{idCorrespondencia}")]
        public async Task<IActionResult> EditarCorrespondencia(Guid idCorrespondencia, [FromBody] ActualizarCorrespondenciaRequest actualizarCorrespondencia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _correspondenciaRepositorio.ActualizarCorrespondencia(idCorrespondencia, actualizarCorrespondencia);
                    return Ok(@"""Se ha actualizado correctamente la información de la correspondencia""");
                }
                else
                {
                    var listadoErrores = ModelState
                                        .Where(y => y.Value.ValidationState == ModelValidationState.Invalid)
                                        .Select(y => new
                                        {
                                            Campo = new string(y.Key?.ToArray()),
                                            Error = y.Value?.Errors?.FirstOrDefault()?.ErrorMessage
                                        })
                                        .ToList();

                    return StatusCode(StatusCodes.Status400BadRequest, $@"""Por favor valide la información enviada: {JsonConvert.SerializeObject(listadoErrores)}""");
                }
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al actualizar la correspondencia""");
            }
        }

        /// <summary>
        /// Elimina la correspondencia y todas sus dependencias
        /// </summary>
        /// <param name="idCorrespondencia"></param>
        /// <returns></returns>
        [HttpDelete("[Action]/{idCorrespondencia}")]
        public async Task<IActionResult> EliminarCorrespondencia(Guid idCorrespondencia)
        {
            try
            {
                await _correspondenciaRepositorio.EliminarCorrespondencia(idCorrespondencia);
                return Ok(@"""Se ha eliminado la correspondencia correctamente""");
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al eliminar la correspondencia""");
            }
        }


        /// <summary>
        /// Carga el listado de los tipos de correspondencia para poder crear una nueva
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoTiposCorrespondencia()
        {
            try
            {
                var listadoTiposCorrespondencia = await _correspondenciaRepositorio.ListadoTiposCorrespondencia();
                return Ok(listadoTiposCorrespondencia);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar los tipos de correspondencia""");
            }
        }


        /// <summary>
        /// Carga el listado de los remitentes para poder crear una nueva correspondencia
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoRemitentes()
        {
            try
            {
                var listadoTiposCorrespondencia = await _correspondenciaRepositorio.ListadoRemitentes();
                return Ok(listadoTiposCorrespondencia);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar los remitentes""");
            }
        }

        /// <summary>
        /// Carga el listado de los destinatarios para poder crear una nueva correspondencia
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoDestinatarios()
        {
            try
            {
                var listadoTiposCorrespondencia = await _correspondenciaRepositorio.ListadoDestinatarios();
                return Ok(listadoTiposCorrespondencia);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar los destinatarios""");
            }
        }

    }
}
