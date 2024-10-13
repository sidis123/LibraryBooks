using LibraryBE.Models;

namespace LibraryBE.Interfaces
{
    public interface IReservationRepository
    {
        ICollection<Reservation> GetAllReservations();
        Reservation GetReservation(int id);
        bool ReservationExists(int id);
        bool Save();
    }
}
