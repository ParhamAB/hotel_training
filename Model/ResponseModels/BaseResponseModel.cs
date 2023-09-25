namespace hotel_training.Model.ResponseModels
{
    public class BaseResponseModel
    {
        public bool isFailed { get; set; }
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public dynamic value { get; set; }
        // public dynamic valueOrDefault { get; set; }

        public BaseResponseModel(bool isFailed, bool isSuccess, int statusCode, dynamic value)
        {
            this.isFailed = isFailed;
            this.isSuccess = isSuccess;
            this.statusCode = statusCode;
            this.value = value;
            //this.valueOrDefault = value;
        }
    }
}
