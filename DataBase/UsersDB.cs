using Dapper;
using hotel_training.Model.AuthModels;
using System.Data.SqlClient;

namespace hotel_training.DataBase
{
    public class UsersDB
    {
        private readonly Connection _connection;

        public UsersDB(Connection connection)
        {
            _connection = connection;
        }
        public UserModel Login(string username, string password)
        {
            string sql = "SELECT * FROM Users WHERE Username = @Username AND Password = @Password";
            UserModel result = _connection.GetConnection().QuerySingleOrDefault<UserModel>(sql, new { Username = username, Password = password });
            return result;
        }

        public bool SignUp(LoginAndSignUpModel user)
        {
            try
            {
                string sql = @"
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
BEGIN
    INSERT INTO Users (Username, Password, FirstName, LastName , IsActive)
    VALUES (@Username, @Password, @FirstName, @LastName, 0)
END
";
                _connection.GetConnection().Execute(sql, user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }
    }
}
