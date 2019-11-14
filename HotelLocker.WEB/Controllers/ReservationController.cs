using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelLocker.BLL.Extensions;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs.ReservationDTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelLocker.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService reservationService;

        public ReservationController(IReservationService reservationService)
        {
            this.reservationService = reservationService;
        }

        [Authorize(Roles = "user")]
        [HttpPost]
        public async Task<ReservationDTO> CreateReservation([FromBody]ReservationCreateDTO reservationCreateDTO)
        {
            return await this.reservationService.CreateReservation(reservationCreateDTO, User.ConvertToUserData().Id);
        }

        [Authorize(Roles = "user")]
        [HttpPut]
        public async Task<ReservationDTO> EditReservation([FromBody]ReservationEditDTO reservationEditDTO)
        {
            return await this.reservationService.EditReservation(reservationEditDTO, User.ConvertToUserData().Id);
        }

        [Authorize(Roles = "user")]
        [HttpDelete("{id}")]
        public async Task DeleteReservation(int id)
        {
            await this.reservationService.DeleteReservation(id, User.ConvertToUserData().Id);
        }

        [Authorize(Roles = "hotel-admin")]
        [HttpGet("hotel")]
        public List<ReservationDTO> GetHotelReservations([FromQuery(Name = "from")] DateTime from,
                                                         [FromQuery(Name = "to")]DateTime to,
                                                         [FromQuery(Name = "room")]int roomId,
                                                         [FromQuery(Name = "guest")]int guestId)
        {
            return reservationService.GetHotelReservations(User.ConvertToUserData().Id, from, to , roomId, guestId);
        }

        [Authorize(Roles = "hotel-admin")]
        [HttpPut("reservation/{id}")]
        public async Task ConfirmPayment(int id)
        {
            await reservationService.ConfirmPayment(id, User.ConvertToUserData().Id);
        }

        [Authorize(Roles = "user")]
        [HttpGet("user")]
        public List<ReservationDTO> GetUserReservations([FromQuery(Name = "from")] DateTime from,
                                                        [FromQuery(Name = "to")]DateTime to,
                                                        [FromQuery(Name = "hotel")]int hotelId)
        {
            return reservationService.GetUserReservationsHistory(User.ConvertToUserData().Id, from, to, hotelId);
        }
    }
}