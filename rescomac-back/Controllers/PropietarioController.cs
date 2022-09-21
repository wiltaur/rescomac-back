using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using rescomac_back.business.Interface;
using rescomac_back.repository.Dto;
using System.Net;

namespace rescomac_back.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyCorsImplementation")]
    public class PropietarioController : ControllerBase
    {
        private readonly IPropietarioService _propietarioService;
        private const string ERROR_PROCESAR_INFORMACION = "Error al procesar la información.";

        public PropietarioController(IPropietarioService propietarioService)
        {
            _propietarioService = propietarioService;
        }

        /// <summary>
        /// Método para crear una propiedad en el catálogo.
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> AddPropiedad([FromBody] PropiedadRequest propiedad)
        {
            try
            {
                var result = await _propietarioService.AddPropiedad(propiedad);

                var response = new ApiResponse<bool>(result)
                {
                    IsSuccess = true,
                    ReturnMessage = $"La información ha sido guardada exitosamente"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método para modificar un propiedado en el catálogo.
        /// </summary>
        /// <param name="propiedad"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> UpdatePropiedad([FromBody] PropiedadModRequest propiedad)
        {
            try
            {
                var result = await _propietarioService.UpdatePropiedad(propiedad);

                var response = new ApiResponse<bool>(result)
                {
                    IsSuccess = result,
                    ReturnMessage = result ? $"La información ha sido modificada exitosamente" : "No se encontró la propiedad en la Base de Datos."
                };
                return result ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método para eliminar un propiedado en el catálogo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("[action]/{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> DeletePropiedad(int id)
        {
            try
            {
                var result = await _propietarioService.DeletePropiedad(id);

                var response = new ApiResponse<bool>(result)
                {
                    IsSuccess = result,
                    ReturnMessage = result ? $"Se eliminó la propiedad exitosamente" : "No se encontró la propiedad en la Base de Datos."
                };
                return result ? Ok(response) : NotFound(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método para listar todas las propiedades.
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<List<PropiedadModRequest>>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<List<PropiedadModRequest>>))]
        public async Task<IActionResult> GetAllPropiedades()
        {
            try
            {
                var result = await _propietarioService.GetAllPropiedad();

                var response = new ApiResponse<List<PropiedadModRequest>>(result)
                {
                    IsSuccess = true,
                    ReturnMessage = $"La información ha sido consultada exitosamente"
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(BadRequestResult(ex));
            }
        }

        /// <summary>
        /// Método privado para el manejo del mensaje a imprimir cuando se presenta excepciones.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        [NonAction]
        private ApiResponse<string> BadRequestResult(Exception ex)
        {
            var message = ex.InnerException == null ? ex.Message : ex.InnerException.Message;
            return new ApiResponse<string>(message)
            {
                IsSuccess = false,
                ReturnMessage = ERROR_PROCESAR_INFORMACION
            };
        }

    }
}