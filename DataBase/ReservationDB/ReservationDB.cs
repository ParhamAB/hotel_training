using Dapper;
using hotel_training.Model.HotelModels;
using hotel_training.Model.ReservationModel;
using System.Data;

namespace hotel_training.DataBase
{
    public class ReservationDB
    {
        private readonly Connection _connection;
        public ReservationDB(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<ReservationModel> GetAllReservation(int pageSize, int pageNumber)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    var parameters = new { PageSize = pageSize, PageNumber = pageNumber };
                    var result = connection.Query<ReservationModel>("GetAllReservations", parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                return [];
            }

        }

        public dynamic AddReservation(AddReservation resModel)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@ReservationDate", resModel.ReservationDate);
                    parameters.Add("@UserId", resModel.UserId);
                    parameters.Add("@PlaceId", resModel.PlaceId);
                    parameters.Add("@Message", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
                    connection.Execute("AddReservation", parameters, commandType: CommandType.StoredProcedure);
                    string message = parameters.Get<string>("@Message");

                    if (message == "Added successfully")
                        return new { added = true, message };
                    else
                        return new { added = false, message };

                }

            }
            catch (Exception ex)
            {
                return new { added = false, message = ex.ToString() };
            }
        }

        public dynamic DeleteReservation(DeletePlaceModel deletePlace)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    string sql = $"DELETE FROM Reservations WHERE Id = @Id;\r\n";
                    connection.Query(sql, deletePlace);
                    return new { deleted = true, message = "deleted seccessfully" };
                }

            }
            catch (Exception ex)
            {
                return new { deleted = false, message = ex.ToString() };
            }
        }
    }
}
