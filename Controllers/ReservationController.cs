using hotel_training.Model.HotelModels;
using hotel_training.Model.ReservationModel;
using hotel_training.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_training.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ReservationController : Controller
    {
        private readonly ReservationServices _resServices;

        public ReservationController(ReservationServices resServices)
        {
            _resServices = resServices;
        }

        [HttpGet(Name = "getAllReservation")]
        public IActionResult getAllReservation(int pageSize = 15, int pageNumber = 1)
        {
            try
            {
                var result = _resServices.GetAllReservation(pageSize, pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(Name = "AddReservation")]
        public IActionResult AddReservation([FromBody] AddReservation resModel)
        {
            try
            {
                var result = _resServices.AddReservation(resModel);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete(Name = "DeleteReservation")]
        public IActionResult DeleteReservation([FromBody] DeletePlaceModel deletePlace)
        {
            try
            {
                var result = _resServices.DeleteReservation(deletePlace);
                if (result.isSuccess)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
