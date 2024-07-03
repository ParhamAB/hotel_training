namespace hotel_training.Model.ReservationModel
{
    public class AddReservation
    {
        public DateTime ReservationDate { get; set; }
        public Guid UserId { get; set; }
        public Guid PlaceId { get; set; }
    }
}
