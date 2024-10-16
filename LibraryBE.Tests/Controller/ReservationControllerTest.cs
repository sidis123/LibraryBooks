using AutoMapper;
using FakeItEasy;
using FluentAssertions;
using LibraryBE.Controllers;
using LibraryBE.DTO;
using LibraryBE.Interfaces;
using LibraryBE.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryBE.Tests.Controller
{
    public class ReservationControllerTest
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public ReservationControllerTest()
        {
            _reservationRepository = A.Fake<IReservationRepository>();
            _mapper = A.Fake<IMapper>();
            _bookRepository = A.Fake<IBookRepository>();
        }
        [Fact]
        public void ReservationController_GetReservations_ReturnOK()
        {
            //Arrange 
            var reservations = A.Fake<ICollection<ReservationDto>>();
            var reservationList = A.Fake<List<ReservationDto>>();
            A.CallTo(() => _mapper.Map<List<ReservationDto>>(reservations)).Returns(reservationList);
            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);
            //Act
            var result = controller.GetReservations();

            //Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }
        [Fact]
        public void ReservationController_GetReservation_ReturnsOk()
        {
            // Arrange
            var reservationId = 1;
            var reservation = A.Fake<Reservation>();
            var reservationDto = A.Fake<ReservationDto>();

            A.CallTo(() => _reservationRepository.ReservationExists(reservationId)).Returns(true);
            A.CallTo(() => _reservationRepository.GetReservation(reservationId)).Returns(reservation);
            A.CallTo(() => _mapper.Map<ReservationDto>(reservation)).Returns(reservationDto);

            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            // Act
            var result = controller.GetReservation(reservationId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [Fact]
        public void ReservationController_GetReservation_ReturnsNotFound()
        {
            // Arrange
            var reservationId = 1;
            A.CallTo(() => _reservationRepository.ReservationExists(reservationId)).Returns(false);

            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            // Act
            var result = controller.GetReservation(reservationId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.NotFoundResult));
        }
        [Fact]
        public void ReservationController_GetReservationsByUser_ReturnsOk()
        {
            // Arrange
            var userId = 1;
            var reservations = A.Fake<ICollection<Reservation>>();
            var reservationList = A.Fake<List<ReservationDto>>();

            A.CallTo(() => _reservationRepository.GetAllReservationsByUserId(userId)).Returns(reservations);
            A.CallTo(() => _mapper.Map<List<ReservationDto>>(reservations)).Returns(reservationList);

            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            // Act
            var result = controller.GetReservations(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.OkObjectResult));
        }

        [Fact]
        public void ReservationController_GetReservationsByUser_ReturnsBadRequest()
        {
            // Arrange
            var userId = 1;
            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            controller.ModelState.AddModelError("Error", "Model state is invalid");

            // Act
            var result = controller.GetReservations(userId);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }
        [Fact]
        public void ReservationController_CreateReservation_ReturnsCreated()
        {
            // Arrange
            var reservationDto = new ReservationDto { id_Reservation = 1, id_Book = 1, Days = 5, QuickPickup = true, id_User = 1 };
            var book = A.Fake<Book>();
            book.Type = "Book"; // Set the book type to "Book"
            A.CallTo(() => _bookRepository.GetBook(reservationDto.id_Book)).Returns(book);
            A.CallTo(() => _reservationRepository.GetAllReservations()).Returns(new List<Reservation>());
            A.CallTo(() => _reservationRepository.CreateReservation(A<Reservation>.Ignored)).Returns(true);

            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            // Act
            var result = controller.CreateReservation(reservationDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.ObjectResult)); // 201 Created
            (result as ObjectResult).StatusCode.Should().Be(201);
        }


        [Fact]
        public void ReservationController_CreateReservation_InvalidModelState_ReturnsBadRequest()
        {
            // Arrange
            var reservationDto = new ReservationDto();
            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            controller.ModelState.AddModelError("Error", "Model state is invalid");

            // Act
            var result = controller.CreateReservation(reservationDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.BadRequestObjectResult));
        }

        [Fact]
        public void ReservationController_CreateReservation_SaveError_ReturnsServerError()
        {
            // Arrange
            var reservationDto = new ReservationDto { id_Reservation = 1, id_Book = 1, Days = 5, QuickPickup = true, id_User = 1 };
            var book = A.Fake<Book>();
            book.Type = "Book";
            A.CallTo(() => _bookRepository.GetBook(reservationDto.id_Book)).Returns(book);
            A.CallTo(() => _reservationRepository.GetAllReservations()).Returns(new List<Reservation>());
            A.CallTo(() => _reservationRepository.CreateReservation(A<Reservation>.Ignored)).Returns(false); // Save fails

            var controller = new ReservationController(_reservationRepository, _mapper, _bookRepository);

            // Act
            var result = controller.CreateReservation(reservationDto);

            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType(typeof(Microsoft.AspNetCore.Mvc.ObjectResult)); // 500 Internal Server Error
            (result as ObjectResult).StatusCode.Should().Be(500);
        }


    }
}
