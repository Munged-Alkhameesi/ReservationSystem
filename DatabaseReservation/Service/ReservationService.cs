using DatabaseReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Service
{
    public class ReservationService: IReservationService
    {
        private readonly ReservationDbContext _db;
        private IReservationService? IReservation;
        public ReservationService(ReservationDbContext Db)
        {
            _db = Db;
        }
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _db.Reservations.Include(r => r.Guest).Include(r => r.Sitting);
        }

    }

}
