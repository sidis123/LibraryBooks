using LibraryBE.Models;

namespace LibraryBE.Interfaces
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetAllReservationsByUserId(int userId);
        ICollection<Reservation> GetAllReservations();
        Reservation GetReservation(int id);
        bool ReservationExists(int id);
        bool Save();
    }
}
