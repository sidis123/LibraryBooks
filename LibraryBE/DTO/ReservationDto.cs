﻿namespace LibraryBE.DTO
{
    public class ReservationDto
    {
        public int id_Reservation { get; set; }
        public DateTime CreationDate { get; set; }
        public bool QuickPickup { get; set; }
        public int Days { get; set; }
        public double TotalCost { get; set; }
        public int id_Book { get; set; }
    }
}
