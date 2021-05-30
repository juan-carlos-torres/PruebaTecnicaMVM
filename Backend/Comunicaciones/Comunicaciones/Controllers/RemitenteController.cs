using Comunicaciones.BI.DTORequest.Remitente;
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
    public class RemitenteController : ControllerBase
    {
        private readonly RemitenteRepositorio _remitenteRepositorio;
        private readonly ILogger<RemitenteController> _logger;

        public RemitenteController(RemitenteRepositorio remitenteRepositorio, ILogger<RemitenteController> logger)
        {
            _remitenteRepositorio = remitenteRepositorio;
            _logger = logger;
        }


        /// <summary>
        /// Carga el listado de los remitentes que hay en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoRemitentes()
        {
            try
            {
                var listadoFacturas = await _remitenteRepositorio.ListadoRemitentes();
                return Ok(listadoFacturas);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar las remitentes""");
            }
        }


        /// <summary>
        /// Crea un nuevo remitente con la información de envíe el usuario
        /// </summary>
        /// <param name="crearRemitenteRequest"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearRemitente([FromBody] CrearRemitenteRequest crearRemitenteRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _remitenteRepositorio.CrearRemitente(crearRemitenteRequest);
                    return Ok(@"""Se ha creado el remitente correctamente""");
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
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al crear el remitente""");
            }
        }


        /// <summary>
        /// Consulta la información del remitente para cuando se va a editar
        /// </summary>
        /// <param name="idRemitente"></param>
        /// <returns></returns>
        [HttpGet("[Action]/{idRemitente}")]
        public async Task<IActionResult> InformacionRemitente(Guid idRemitente)
        {
            try
            {
                var informacionRemitente = await _remitenteRepositorio.InformacionRemitente(idRemitente);
                return Ok(informacionRemitente);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar la información de el remitente""");
            }
        }


        /// <summary>
        /// Actualiza la información del remitente con la que envíe el usuario
        /// </summary>
        /// <param name="idRemitente"></param>
        /// <param name="actualizarRemitente"></param>
        /// <returns></returns>
        [HttpPut("[Action]/{idRemitente}")]
        public async Task<IActionResult> EditarRemitente(Guid idRemitente, [FromBody] EditarRemitenteRequest actualizarRemitente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _remitenteRepositorio.EditarRemitente(idRemitente, actualizarRemitente);
                    return Ok(@"""Se ha actualizado correctamente la información del remitente""");
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
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al actualizar el remitente""");
            }
        }


        /// <summary>
        /// Elimina el remitente seleccionado junto con todas sus dependencias
        /// </summary>
        /// <param name="idRemitente"></param>
        /// <returns></returns>
        [HttpDelete("[Action]/{idRemitente}")]
        public async Task<IActionResult> EliminarRemitente(Guid idRemitente)
        {
            try
            {
                await _remitenteRepositorio.EliminarRemitente(idRemitente);
                return Ok(@"""Se ha eliminado el remitente correctamente""");
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al eliminar el remitente""");
            }
        }



    }
}
