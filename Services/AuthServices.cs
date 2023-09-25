using hotel_training.DataBase;
using hotel_training.Model.AuthModels;
using hotel_training.Model.ResponseModels;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace hotel_training.Services
{
    public class AuthServices
    {

        private readonly IConfiguration _configuration;
        private readonly UsersDB _usersDB;

        public AuthServices(IConfiguration configuration, UsersDB usersDB)
        {
            _configuration = configuration;
            _usersDB = usersDB;
        }

        public string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JsonWebTokenConfig:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["JsonWebTokenConfig:issuer"],
                audience: _configuration["JsonWebTokenConfig:audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public BaseResponseModel login(string username, string password)
        {
            try
            {
                UserModel result = _usersDB.Login(username, password);
                if (result == null)
                {
                    return new BaseResponseModel(true, false, 404, new { });
                }
                var tokenStr = GenerateToken(result.Username);
                return new BaseResponseModel(false, true, 200, new { user = result, token = tokenStr });
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }

        public BaseResponseModel signUp(LoginAndSignUpModel model)
        {
            try
            {
                bool result = _usersDB.SignUp(model);
                if (result)
                {
                    return new BaseResponseModel(false, true, 200, new { message = "SignUp seccessfuly" });
                }
                return new BaseResponseModel(true, false, 400, new { message = "bad request" });
            }
            catch (Exception ex)
            {
                return new BaseResponseModel(true, false, 500, new { message = ex.ToString() });
            }

        }
    }
}
