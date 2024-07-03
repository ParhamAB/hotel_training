using hotel_training.DataBase;
using hotel_training.Model.HotelModels;
using hotel_training.Model.ResponseModels;

namespace hotel_training.Services
{
    public class PlacesServices
    {
        private readonly PlacesDB _placesDB;
        public PlacesServices(PlacesDB placeDB)
        {
            _placesDB = placeDB;
        }

        public BaseResponseModel GetAllPlaces(string title, string type, int pageSize, int pageNumber)
        {
            try
            {
                IEnumerable<PlaceModel> result = _placesDB.GetAllPlaces(title,type,pageSize, pageNumber);
                return new BaseResponseModel(false, true, 200, result);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }

        public BaseResponseModel AddPlace(AddPlaceModel placeModel)
        {
            try
            {
                var result = _placesDB.AddPlace(placeModel);
                if (result.added == true)
                {
                    return new BaseResponseModel(false, true, 200, result);
                }
                return new BaseResponseModel(true, false, 400, result);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }

        public BaseResponseModel EditPlace(EditPlaceModel editModel)
        {
            try
            {
                var result = _placesDB.EditPlace(editModel);
                if (result.edited == true)
                {
                    return new BaseResponseModel(false, true, 200, result);
                }
                return new BaseResponseModel(true, false, 400, result);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }

        public BaseResponseModel DeletePlace(DeletePlaceModel deletePlace)
        {
            try
            {
                var result = _placesDB.DeletePlace(deletePlace);
                if (result.deleted == true)
                {
                    return new BaseResponseModel(false, true, 200, result);
                }
                return new BaseResponseModel(true, false, 400, result);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }
    }
}
