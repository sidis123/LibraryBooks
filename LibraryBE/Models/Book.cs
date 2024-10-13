using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryBE.Models
{
    public class Book
    {
        [Key]
        public int id_Book {  get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Picture { get; set; }
        public string Type { get; set; }
        public ICollection<Reservation> Reservations { get; set; }


    }
}
