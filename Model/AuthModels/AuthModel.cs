namespace hotel_training.Model.AuthModels
{
    public class AuthModel
    {
        public string token { get; set; }
        public string userName { get; set; }

        public AuthModel(string token, string UserName) {
            this.token = token;
            this.userName = UserName;
        }
    }
}
