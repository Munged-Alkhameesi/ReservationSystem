using DatabaseReservation.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseReservation.Service
{
    public class ReservationService: IReservationService
    {
        private readonly ReservationDbContext _db;
        private readonly IReservationService? IReservation;
        public ReservationService(ReservationDbContext Db)
        {
            _db = Db;
        }
        /// <summary>
        /// a service method to get all the reservations in the table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Reservation> GetAllReservations()
        {
            return _db.Reservations.Include(r => r.Guest).Include(r => r.Sitting);
        }

    }

}
