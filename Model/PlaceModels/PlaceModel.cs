namespace hotel_training.Model.HotelModels
{
    public class PlaceModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Address { get; set; }
        public string PlaceType { get; set; }
        public string GeographicLocation { get; set; }
        public string Registrant { get; set; }
        public DateTime RegistrationDate { get; set; }

    }
}
