using HotelLocker.Common.DTOs.HotelsDTO;
using HotelLocker.DAL.Entities;

namespace HotelLocker.BLL.Mappers
{
    public static class HotelMapperExtensions
    {
        public static Hotel ToHotel(this HotelCreateDTO dto)
        {
            return new Hotel()
            {
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                Stars = dto.Stars
            };
        }

        public static Hotel ToHotel(this HotelDTO dto)
        {
            return new Hotel()
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                City = dto.City,
                Country = dto.Country,
                Stars = dto.Stars
            };
        }

        public static HotelAdmin GetHotelAdminInfo(this HotelCreateDTO dto)
        {
            return new HotelAdmin()
            {
                Email = dto.AdminEmail,
                UserName = dto.AdminEmail
            };
        }

        public static HotelDTO ToHotelDTO(this Hotel hotel)
        {
            return new HotelDTO()
            {
                Id = hotel.Id,
                Name = hotel.Name,
                Address = hotel.Address,
                City = hotel.City,
                Country = hotel.Country,
                Stars = hotel.Stars
            };
        }
    }
}
