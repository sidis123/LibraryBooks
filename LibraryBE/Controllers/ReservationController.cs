using AutoMapper;
using LibraryBE.DTO;
using LibraryBE.Interfaces;
using LibraryBE.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibraryBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;

        public ReservationController(IReservationRepository reservationRepository, IMapper mapper)
        {
            _reservationRepository = reservationRepository;
            _mapper = mapper;
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
    }
}
