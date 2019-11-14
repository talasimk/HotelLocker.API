using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelLocker.BLL.Extensions;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs.RoomDTOs;
using HotelLocker.Common.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelLocker.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService roomService;

        public RoomsController(IRoomService roomService)
        {
            this.roomService = roomService;
        }

        [Authorize(Roles = "hotel-admin")]
        [HttpPost]
        public async Task<RoomDTO> CreateRoom([FromBody] RoomCreateDTO roomCreateDTO)
        {
            return await roomService.CreateRoom(roomCreateDTO, User.ConvertToUserData().Id);
        }

        [Authorize(Roles = "hotel-admin, hotel-staff")]
        [HttpPut("available")]
        public async Task<RoomDTO> EditAvailableRoom(int roomId, bool isAvailable)
        {
            return await roomService.EditRoomAvailable(roomId, User.ConvertToUserData().Id, isAvailable);
        }

        [Authorize(Roles = "hotel-admin, hotel-staff")]
        [HttpPut]
        public async Task<RoomDTO> EditRoom([FromBody]RoomEditDTO roomDTO)
        {
            return await roomService.EditRoom(roomDTO, User.ConvertToUserData().Id);
        }

        
        [HttpGet]
        public List<RoomDTO> GetAllRooms([FromQuery(Name = "minPrice")] int minPrice,
                                         [FromQuery(Name = "maxPrice")] int maxPrice,
                                         [FromQuery(Name = "beds")] int beds,
                                         [FromQuery(Name = "sort")] string sort,
                                         [FromQuery(Name = "sortDir")] bool sortDir,
                                         [FromQuery(Name = "roomCategory")] RoomCategory roomCategory,
                                         [FromQuery(Name = "from")] DateTime from,
                                         [FromQuery(Name = "hotelId")] int hotelId,
                                         [FromQuery(Name = "to")] DateTime to)
        {
            return roomService.GetAllRooms(minPrice, maxPrice, beds, sort, sortDir, roomCategory, from, hotelId, to);
        }
    }
}