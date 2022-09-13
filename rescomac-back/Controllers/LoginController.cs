using Microsoft.AspNetCore.Mvc;
using rescomac_back.business.Interface;
using rescomac_back.repository.Dto;
using System.Net;

namespace rescomac_back.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private const string ERROR_PROCESAR_INFORMACION = "Error al procesar la información.";

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost("[action]/{email}/{password}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ApiResponse<bool>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ApiResponse<bool>))]
        public async Task<IActionResult> ValidarUsuario(string email, string password)
        {
            try
            {
                var usuarioValido = await _loginService.ValidarUsuario(email, password);
                
                var response = new ApiResponse<bool>(usuarioValido)
                {
                    IsSuccess = usuarioValido
                };

                if (usuarioValido)
                {
                    response.ReturnMessage = $"Usuario existe en el sistema.";
                    return Ok(response);
                }
                else
                {
                    response.ReturnMessage = $"El usuario o contraseña es incorrecto.";
                    return BadRequest(response);
                }
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