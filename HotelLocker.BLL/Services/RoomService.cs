using HotelLocker.BLL.Interfaces;
using HotelLocker.BLL.Mappers;
using HotelLocker.Common.DTOs.RoomDTOs;
using HotelLocker.Common.Enums;
using HotelLocker.Common.Exceptions;
using HotelLocker.Common.Validators;
using HotelLocker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork context;

        public RoomService(IUnitOfWork context)
        {
            this.context = context;
        }

        public async Task<RoomDTO> CreateRoom(RoomCreateDTO roomCreateDTO, int userID)
        {
            ValidationResults result = ModelValidator.IsValid(roomCreateDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var hotel = context.Hotels.Get(roomCreateDTO.HotelId);
            if (hotel == null)
                throw new NotFoundException("No such hotel");

            if (hotel.HotelAdminId != userID)
                throw new PermissionException();

            var isExistsWithNumber = hotel.Rooms.Where(r => r.NumberInHotel == roomCreateDTO.NumberInHotel).Count() != 0;

            if (isExistsWithNumber)
                throw new ValidationException("Hotel room with such number already exists");

            var room = roomCreateDTO.ToRoom();
            context.Rooms.Create(room);
            await context.SaveAsync();
            return room.ToRoomDTO();
        }

        public async Task<RoomDTO> EditRoomAvailable(int roomId, int userId, bool isAvailable)
        {
            var room = context.Rooms.Get(roomId);
            if (room == null)
                throw new NotFoundException("No such room");

            
            if (room.Hotel.HotelAdminId != userId)
                throw new PermissionException();

            room.IsAvailable = isAvailable;
            context.Rooms.Update(room);
            await context.SaveAsync();
            return room.ToRoomDTO();
        }


        public async Task<RoomDTO> EditRoom(RoomEditDTO roomDTO, int userId)
        {
            ValidationResults result = ModelValidator.IsValid(roomDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var room = context.Rooms.Get(roomDTO.Id);
            if (room == null)
                throw new NotFoundException("No such room");


            if (room.Hotel.HotelAdminId != userId)
                throw new PermissionException();

            if (roomDTO.NumberInHotel != 0) room.NumberInHotel = roomDTO.NumberInHotel;
            if (roomDTO.Category != 0) room.Category = roomDTO.Category;
            if (roomDTO.Beds != 0) room.Beds = roomDTO.Beds;
            if (roomDTO.Price != 0) room.Price = roomDTO.Price;
            context.Rooms.Update(room);
            await context.SaveAsync();
            return room.ToRoomDTO();
        }

        public List<RoomDTO> GetAllRooms(decimal minPrice,
                                                     decimal maxPrice,
                                                     int beds,
                                                     string sort,
                                                     bool sortDir,
                                                     RoomCategory roomCategory,
                                                     DateTime from,
                                                     int hotelId,
                                                     DateTime to)
        {
            var rooms = context.Rooms.GetAll();
            if (hotelId != 0) rooms = rooms.Where(r => r.HotelId == hotelId);
            if (minPrice != 0) rooms = rooms.Where(r => r.Price >= minPrice);
            if (maxPrice != 0 && maxPrice > minPrice) rooms = rooms.Where(r => r.Price <= maxPrice);
            if (beds!= 0) rooms = rooms.Where(r => r.Beds == beds);
            if (roomCategory != 0) rooms = rooms.Where(r => r.Category == roomCategory);
            if(from != null)
            {
                rooms = rooms.Where(r => r.Reservations.Where(res => res.EndDate > from || res.StartDate <= from).Count() == 0);
            }
            if (to != null && to > from)
            {
                rooms = rooms.Where(r => r.Reservations.Where(res => res.EndDate > from || res.StartDate > from).Count() == 0);
            }
            if(sort != null)
            {
                if (sort == "price" && sortDir) rooms = rooms.OrderBy(r => r.Price);
                if (sort == "price" && !sortDir) rooms = rooms.OrderByDescending(r => r.Price);
            }
            return rooms.Select(r => r.ToRoomDTO()).ToList();
        }



    }
}
