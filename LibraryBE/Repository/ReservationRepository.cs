using LibraryBE.Data;
using LibraryBE.Interfaces;
using LibraryBE.Models;

namespace LibraryBE.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DataContext _context;

        public ReservationRepository(DataContext context)
        {
            _context = context;
        }

        public ICollection<Reservation> GetAllReservations()
        {
            return _context.Reservations.OrderBy(r => r.id_Reservation).ToList();
        }

        public ICollection<Reservation> GetAllReservationsByUserId(int userId)
        {
            return _context.Reservations.Where(r => r.id_User == userId).OrderBy(r=>r.id_Reservation).ToList();
        }

        public Reservation GetReservation(int id)
        {
            return _context.Reservations.Where(r => r.id_Reservation == id).FirstOrDefault();
        }

        public bool ReservationExists(int id)
        {
            return _context.Reservations.Any(r => r.id_Reservation == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
