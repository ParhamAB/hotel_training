using Dapper;
using Dapper.FastCrud;
using hotel_training.Model.HotelModels;
using System.Data;
using System.Linq;

namespace hotel_training.DataBase
{
    public class PlacesDB
    {
        private readonly Connection _connection;
        public PlacesDB(Connection connection)
        {
            _connection = connection;
        }

        public IEnumerable<PlaceModel> GetAllPlaces(string title, string type, int pageSize, int pageNumber)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    var parameters = new { PageSize = pageSize, PageNumber = pageNumber, Title = title, PlaceType = type };
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
                    var existingPlace = connection.Find<AddPlaceModel>(statement => statement
                        .Where($"{nameof(AddPlaceModel.GeographicLocation):C} = @GeographicLocation")
                        .WithParameters(new { placeModel.GeographicLocation }));

                    if (existingPlace == null)
                    {
                        var place = new AddPlaceModel
                        {
                            Title = placeModel.Title,
                            Address = placeModel.Address,
                            PlaceType = placeModel.PlaceType,
                            GeographicLocation = placeModel.GeographicLocation
                        };
                        connection.Insert(place);
                        return new { added = true, message = "send successfully" };
                    }
                    else
                    {
                        return new { added = false, message = "Place with given geographic location already exists" };
                    }
                }
            }
            catch (Exception ex)
            {
                return new { added = false, message = ex.ToString() };
            }
        }

        public dynamic EditPlace(EditPlaceModel editModel)
        {
            try
            {
                using (var connection = _connection.GetConnection())
                {
                    var placeToUpdate = connection.Find<PlaceModel>().Where(E => E.Id == editModel.Id) as PlaceModel;

                    if (placeToUpdate != null)
                    {
                        placeToUpdate.Title = editModel.dataModel.Title;
                        placeToUpdate.Address = editModel.dataModel.Address;
                        placeToUpdate.PlaceType = editModel.dataModel.PlaceType;
                        placeToUpdate.GeographicLocation = editModel.dataModel.GeographicLocation;

                        connection.Update(placeToUpdate);

                        return new { edited = true, message = "send successfully" };
                    }
                    else
                    {
                        return new { edited = false, message = "Place not found" };
                    }
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
