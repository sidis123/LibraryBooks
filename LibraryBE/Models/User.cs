using System.ComponentModel.DataAnnotations;

namespace LibraryBE.Models
{
    public class User
    {
        [Key]
        public int id_User { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public ICollection<Reservation> Reservations { get; set; }
    }
}
