using DatabaseReservation.Models;
namespace DatabaseReservation.Service
{
    public interface IReservationService
    {
        public IEnumerable<Reservation> GetAllReservations();

    }
}
