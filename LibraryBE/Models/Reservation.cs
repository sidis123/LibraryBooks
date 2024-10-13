using System.ComponentModel.DataAnnotations;

namespace LibraryBE.Models
{
    public class Reservation
    {
        [Key]
        public int id_Reservation { get; set; }
        public DateTime CreationDate { get; set; }
        public bool QuickPickup { get; set; }
        public int Days { get; set; }
        public double TotalCost { get; set; }
        public int id_User { get; set; }
        public int id_Book { get; set; }
        public User User { get; set; }
        public Book Book { get; set; }

    }
}
