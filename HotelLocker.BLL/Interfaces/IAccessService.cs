using HotelLocker.Common.DTOs;
using HotelLocker.Common.DTOs.RoomDTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Interfaces
{
    public interface IAccessService
    {
        Task GetAccessByHotelAdmin(int userId, AccessByWorkerDTO workerDTO);
        Task GetAccessByHotelStaff(int userId, AccessByWorkerDTO workerDTO);
        Task GetAccessByUser(int userId, int roomId);
        List<RoomAccessDTO> GetRoomAccessUserDTO(int reservationId, int userId);
        List<RoomAccessDTO> GetRoomAccessDTO(int roomId, int userId);
    }
}
