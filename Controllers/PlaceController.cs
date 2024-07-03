using hotel_training.Model.HotelModels;
using hotel_training.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hotel_training.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class PlaceController : Controller
    {

        private readonly PlacesServices _placesServices;

        public PlaceController(PlacesServices placesServices)
        {
            _placesServices = placesServices;
        }

        [HttpGet(Name = "getAllPlaces")]
        public IActionResult getAllPlaces(string title = "", string type = "", int pageSize = 15, int pageNumber = 1)
        {
            try
            {
                var result = _placesServices.GetAllPlaces(title, type, pageSize, pageNumber);
                return Ok(result);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost(Name = "addPlace")]
        public IActionResult AddPlace([FromBody] AddPlaceModel placeModel)
        {
            try
            {
                var result = _placesServices.AddPlace(placeModel);
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

        [HttpPut(Name = "EditPlace")]
        public IActionResult EditPlace([FromBody] EditPlaceModel editModel)
        {
            try
            {
                var result = _placesServices.EditPlace(editModel);
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

        [HttpDelete(Name = "DeletePlace")]
        public IActionResult DeletePlace([FromBody] DeletePlaceModel deletePlace)
        {
            try
            {
                var result = _placesServices.DeletePlace(deletePlace);
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
