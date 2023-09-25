using hotel_training.Model.AuthModels;
using hotel_training.Model.ResponseModels;
using hotel_training.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_training.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class AuthController : Controller
    {
        private readonly AuthServices _authServices;

        public AuthController(AuthServices authServices)
        {
            _authServices = authServices;
        }

        [HttpPost(Name = "Login")]
        public IActionResult Login([FromBody] LoginAndSignUpModel model)
        {
            try
            {
                BaseResponseModel user = _authServices.login(model.Username, model.Password);
                if (user.isSuccess != false)
                {
                    return Ok(user);
                }
                else
                {
                    return NotFound(user);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [HttpPost(Name = "SignUp")]
        public IActionResult SignUp([FromBody] LoginAndSignUpModel model)
        {
            try
            {
                BaseResponseModel user = _authServices.signUp(model);
                if (user.isSuccess != false)
                {
                    return Ok(user);
                }
                else
                {
                    return BadRequest(user);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
