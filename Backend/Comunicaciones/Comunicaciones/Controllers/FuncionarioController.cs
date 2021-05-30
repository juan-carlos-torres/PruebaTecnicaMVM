using Comunicaciones.BI.DTORequest.Funcionario;
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
    public class FuncionarioController : ControllerBase
    {
        private readonly FuncionarioRepositorio _funcionarioRepositorio;
        private readonly ILogger<FuncionarioController> _logger;


        public FuncionarioController(FuncionarioRepositorio funcionarioRepositorio, ILogger<FuncionarioController> logger)
        {
            _funcionarioRepositorio = funcionarioRepositorio;
            _logger = logger;
        }

        /// <summary>
        /// Carga el listado de los funcionarios que hay  en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[Action]")]
        public async Task<IActionResult> ListadoFuncionarios()
        {
            try
            {
                var listadoFacturas = await _funcionarioRepositorio.ListadoFuncionarios();
                return Ok(listadoFacturas);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar las funcionarios""");
            }
        }


        /// <summary>
        /// Crea el funcionario con la información proporcionada por el usuario
        /// </summary>
        /// <param name="crearFuncionarioRequest"></param>
        /// <returns></returns>
        [HttpPost("[Action]")]
        public async Task<IActionResult> CrearFuncionario([FromBody] CrearFuncionarioRequest crearFuncionarioRequest)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _funcionarioRepositorio.CrearFuncionario(crearFuncionarioRequest);
                    return Ok(@"""Se ha creado el funcionario correctamente""");
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
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al crear el funcionario""");
            }
        }

        /// <summary>
        /// Consulta la información del funcionario para cuando se vaya a editar
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        [HttpGet("[Action]/{idFuncionario}")]
        public async Task<IActionResult> InformacionFuncionario(Guid idFuncionario)
        {
            try
            {
                var informacionFuncionario = await _funcionarioRepositorio.InformacionFuncionario(idFuncionario);
                return Ok(informacionFuncionario);
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al consultar la información de el funcionario""");
            }
        }


        /// <summary>
        /// Actualiza la información del funcionario con la que se envíe
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <param name="actualizarFuncionario"></param>
        /// <returns></returns>
        [HttpPut("[Action]/{idFuncionario}")]
        public async Task<IActionResult> EditarFuncionario(Guid idFuncionario, [FromBody] EditarFuncionarioRequest actualizarFuncionario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _funcionarioRepositorio.EditarFuncionario(idFuncionario, actualizarFuncionario);
                    return Ok(@"""Se ha actualizado correctamente la información del funcionario""");
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
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al actualizar el funcionario""");
            }
        }


        /// <summary>
        /// Elimina el funcionario junto con todas sus dependencias
        /// </summary>
        /// <param name="idFuncionario"></param>
        /// <returns></returns>
        [HttpDelete("[Action]/{idFuncionario}")]
        public async Task<IActionResult> EliminarFuncionario(Guid idFuncionario)
        {
            try
            {
                await _funcionarioRepositorio.EliminarFuncionario(idFuncionario);
                return Ok(@"""Se ha eliminado el funcionario correctamente""");
            }
            catch (Exception err)
            {
                _logger.LogError(err.ToString());
                return StatusCode(StatusCodes.Status500InternalServerError, @"""Ha ocurrido un error al eliminar el funcionario""");
            }
        }
    }
}
