using AutoMapper;
using LibraryBE.DTO;
using LibraryBE.Interfaces;
using LibraryBE.Models;
using LibraryBE.Repository;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper, IBookRepository bookRepository)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
        public IActionResult GetReservations()
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetAllReservations());
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }

        [HttpGet("{Resid}")]
        [ProducesResponseType(200, Type = typeof(Reservation))]
        [ProducesResponseType(400)]
        public IActionResult GetReservation(int Resid)
        {
            if (!_reservationRepository.ReservationExists(Resid))
            {
                return NotFound();
            }
            var reservation = _mapper.Map<ReservationDto>(_reservationRepository.GetReservation(Resid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservation);
        }

        [HttpGet("user/{Userid}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<Reservation>))]
        public IActionResult GetReservations(int Userid)
        {
            var reservations = _mapper.Map<List<ReservationDto>>(_reservationRepository.GetAllReservationsByUserId(Userid));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(reservations);
        }

        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult CreateReservation([FromBody] ReservationDto reservationCreate)//reikia FromBody , nes jis paims data is jsono
        {
            if (reservationCreate == null)
            {
                return BadRequest(ModelState);
            }
            var reservation = _reservationRepository.GetAllReservations().Where(r => r.id_Reservation == reservationCreate.id_Reservation).FirstOrDefault();

            if (reservation != null)
            {
                ModelState.AddModelError("", "Reservation allready exists");
                return StatusCode(422, ModelState);
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = _bookRepository.GetBook(reservationCreate.id_Book);
            var costPerDayPerType = 0;
            if(book.Type == "Book")
            {
                costPerDayPerType = 2;
            }
            if(book.Type == "Audiobook")
            {
                costPerDayPerType = 3;
            }
            double costBeforeDiscount = costPerDayPerType * reservationCreate.Days;
            if(reservationCreate.Days > 3)
            {
                costBeforeDiscount = costBeforeDiscount * 0.9;
            }
            if (reservationCreate.Days > 10)
            {
                costBeforeDiscount = costBeforeDiscount * 0.8;
            }
            double costAfterDiscount = costBeforeDiscount + 3;
            if(reservationCreate.QuickPickup == true)
            {
                costAfterDiscount = costAfterDiscount + 5;
            }
            //var reservationMap = _mapper.Map<Reservation>(reservationCreate);
            var reservationDto = new Reservation
            {
                id_Reservation = reservationCreate.id_Reservation,
                CreationDate = DateTime.Now,
                QuickPickup = reservationCreate.QuickPickup,
                Days = reservationCreate.Days,
                TotalCost = costAfterDiscount,
                id_User = reservationCreate.id_User,
                id_Book = reservationCreate.id_Book
            };
            if (!_reservationRepository.CreateReservation(reservationDto))
            {
                ModelState.AddModelError("", "Something went wrong while saving");
                return StatusCode(500, ModelState);
            }
            return StatusCode(201, "Successfully created");
        }

    }
}
