using HotelLocker.BLL.Interfaces;
using HotelLocker.BLL.Mappers;
using HotelLocker.Common.DataObjects;
using HotelLocker.Common.DTOs;
using HotelLocker.Common.Exceptions;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Services
{
    public class AccessService : IAccessService
    {
        private readonly IUnitOfWork context;

        public AccessService(IUnitOfWork unitOfWork)
        {
            context = unitOfWork;
        }

        public async Task GetAccessByHotelAdmin(int userId, AccessByWorkerDTO workerDTO)
        {
            var room = context.Rooms.Get(workerDTO.RoomId);
            if (room == null)
                throw new NotFoundException("No such room");

            bool hasPermission = false;
            HotelAdmin hotelAdmin = context.HotelAdmins.Get(userId);
            hasPermission = room.HotelId == hotelAdmin.Hotel.Id;

            if (!hasPermission)
                throw new PermissionException();

            var lastAccess = room.RoomAccesses.Where(r => r.UserId == userId).OrderBy(x => x.DateTime).LastOrDefault();
            bool isOpened = (lastAccess == null) ? true : !lastAccess.IsOpen;
            var access = new RoomAccess()
            {
                DateTime = DateTime.Now,
                IsOpen = isOpened,
                RoomId = workerDTO.RoomId,
                UserId = userId,
                ReasonOfVisit = workerDTO.Reason

            };
            context.RoomAccesses.Create(access);
            await context.SaveAsync();
        }

        public async Task GetAccessByHotelStaff(int userId, AccessByWorkerDTO workerDTO)
        {
            var room = context.Rooms.Get(workerDTO.RoomId);
            if (room == null)
                throw new NotFoundException("No such room");

            bool hasPermission = false;
            HotelStaff staff = context.HotelStaffs.Get(userId);
            room = context.Rooms.Get(workerDTO.RoomId);
            hasPermission = room.HotelId == staff.Hotel.Id;

            if (!hasPermission)
                throw new PermissionException();

            var lastAccess = room.RoomAccesses.Where(r => r.UserId == userId).OrderBy(x => x.DateTime).LastOrDefault();
            bool isOpened = (lastAccess == null) ? true : !lastAccess.IsOpen;
            var access = new RoomAccess()
            {
                DateTime = DateTime.Now,
                IsOpen = isOpened,
                RoomId = workerDTO.RoomId,
                UserId = userId,
                ReasonOfVisit = workerDTO.Reason

            };
            context.RoomAccesses.Create(access);
            await context.SaveAsync();
        }

        public async Task GetAccessByUser(int userId, int roomId)
        {
            var room = context.Rooms.Get(roomId);
            if (room == null)
                throw new NotFoundException("No such room");

            bool hasPermission = false;
            Guest user = context.Guests.Get(userId);
            hasPermission = DoesUserHasAccess(userId, roomId);
           
            if (!hasPermission)
                throw new PermissionException();
            var lastAccess = room.RoomAccesses.Where(r => r.UserId == userId).OrderBy(x => x.DateTime).LastOrDefault();
            bool isOpened = (lastAccess == null) ? true : !lastAccess.IsOpen;
            var access = new RoomAccess()
            {
                DateTime = DateTime.Now,
                IsOpen = isOpened,
                RoomId = roomId,
                UserId = userId,
                ReasonOfVisit = ""

            };
            context.RoomAccesses.Create(access);
            await context.SaveAsync();
        }





        public List<RoomAccessDTO> GetRoomAccessUserDTO(int reservationId, int userId)
        {
            var reservation = context.Reservations.Get(reservationId);
            if (reservation == null)
                throw new NotFoundException("No such reservation");
            var roomAccesses = reservation.Room.RoomAccesses.Where(r => r.DateTime >= reservation.StartDate && r.DateTime <= reservation.EndDate);
            return roomAccesses.Select(r => r.ToRoomAccessDTO()).ToList();
        }

        public List<RoomAccessDTO> GetRoomAccessDTO(int roomId, int userId)
        {
            var room = context.Rooms.Get(roomId);
            if (room == null)
                throw new NotFoundException("No such room");
            if (room.Hotel.HotelAdminId != userId || room.Hotel.HotelStaffs.Where(s => s.Id == userId).Count() == 0)
                throw new PermissionException();
            return room.RoomAccesses.Select(r => r.ToRoomAccessDTO()).ToList();
        }

        private bool DoesUserHasAccess(int userId, int roomId)
        {
            DateTime currentTime = DateTime.Now;
            Guest user = context.Guests.Get(userId);
            bool condition1 = user.Reservations
                .Where(r => r.RoomId == roomId && r.StartDate <= currentTime && r.EndDate >= currentTime)
                .Count() != 0;
            var lastAccess = user.RoomAccesses.Where(r => r.RoomId == roomId && r.IsOpen).OrderBy(r => r.DateTime).LastOrDefault();
            var lastAccessAgo = (lastAccess != null) ? currentTime - lastAccess.DateTime : TimeSpan.FromHours(4);
            bool condition2 = lastAccess != null && lastAccessAgo.TotalHours < 3;
            return condition1 || condition2;

        }
    }
}
