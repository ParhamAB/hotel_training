using hotel_training.DataBase;
using hotel_training.Model.HotelModels;
using hotel_training.Model.ReservationModel;
using hotel_training.Model.ResponseModels;

namespace hotel_training.Services
{
    public class ReservationServices
    {
        private readonly ReservationDB _resDB;
        public ReservationServices(ReservationDB resDB)
        {
            _resDB = resDB;
        }

        public BaseResponseModel GetAllReservation(int pageSize, int pageNumber)
        {
            try
            {
                IEnumerable<ReservationModel> result = _resDB.GetAllReservation(pageSize, pageNumber);
                return new BaseResponseModel(false, true, 200, result);
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }

        public BaseResponseModel AddReservation(AddReservation resModel)
        {
            try
            {
                var result = _resDB.AddReservation(resModel);
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


        public BaseResponseModel DeleteReservation(DeletePlaceModel deletePlace)
        {
            try
            {
                var result = _resDB.DeleteReservation(deletePlace);
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
