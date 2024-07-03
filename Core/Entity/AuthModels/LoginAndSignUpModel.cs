namespace hotel_training.Model.AuthModels
{
    public class LoginAndSignUpModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public LoginAndSignUpModel(string password, string UserName,string firstName,string lastName)
        {
            this.Password = password;
            this.Username = UserName;
            this.FirstName = firstName;
            this.LastName = lastName;
        }
    }
}
