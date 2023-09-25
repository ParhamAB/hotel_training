namespace hotel_training.Model.ReservationModel
{
    public class ReservationModel
    {
        public Guid Id { get; set; }
        public string RegistrationDate { get; set; }
        public string ReservationDate { get; set; }
        public Guid UserId { get; set; }
        public Guid PlaceId { get; set; }

    }
}
