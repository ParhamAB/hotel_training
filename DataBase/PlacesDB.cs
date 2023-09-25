using Dapper;
using hotel_training.Model.AuthModels;
using hotel_training.Model.HotelModels;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data;

namespace hotel_training.DataBase
{
    public class PlacesDB
    {
        private readonly Connection _connection;
        public PlacesDB(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<PlaceModel> GetAllPlaces(string title,string type,int pageSize, int pageNumber)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    var parameters = new { PageSize = pageSize, PageNumber = pageNumber , Title = title, PlaceType = type };
                    var result = connection.Query<PlaceModel>("GetAllPlaces", parameters, commandType: CommandType.StoredProcedure);
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                return [];
            }

        }

        public dynamic AddPlace(AddPlaceModel placeModel)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    string sql = "INSERT INTO Places (Title, Address, PlaceType, GeographicLocation)\r\nSELECT @Title, @Address, @PlaceType, @GeographicLocation\r\nWHERE NOT EXISTS (\r\n    SELECT 1 FROM Places WHERE GeographicLocation = @GeographicLocation\r\n);";
                    connection.Query(sql, placeModel);
                    return new { added = true, message = "send seccessfully" };
                }

            }
            catch (Exception ex)
            {
                return new { added = true, message = ex.ToString() };
            }
        }

        public dynamic EditPlace(EditPlaceModel editModel)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    string sql = $"UPDATE Places\r\nSET \r\n    Title = @Title,\r\n    Address = @Address,\r\n    PlaceType = @PlaceType,\r\n    GeographicLocation = @GeographicLocation\r\nWHERE Id = @Id;\r\n";
                    connection.Query(sql, new { editModel.dataModel.Title, editModel.dataModel.Address, editModel.dataModel.PlaceType, editModel.dataModel.GeographicLocation, editModel.Id });
                    return new { edited = true, message = "send seccessfully" };
                }

            }
            catch (Exception ex)
            {
                return new { edited = false, message = ex.ToString() };
            }
        }

        public dynamic DeletePlace(DeletePlaceModel deletePlace)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    string sql = $"DELETE FROM Places WHERE Id = @Id;\r\n";
                    connection.Query(sql, deletePlace);
                    return new { deleted = true, message = "send seccessfully" };
                }

            }
            catch (Exception ex)
            {
                return new { deleted = false, message = ex.ToString() };
            }
        }
    }
}
