using HotelLocker.BLL.Interfaces;
using HotelLocker.BLL.Mappers;
using HotelLocker.Common.DTOs.HotelsDTO;
using HotelLocker.Common.Exceptions;
using HotelLocker.Common.Validators;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Linq;

namespace HotelLocker.BLL.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork context;
        private readonly UserManager<User> _userManager;

        public HotelService(UserManager<User> userManager, IUnitOfWork db)
        {
            context = db;
            _userManager = userManager;
        }

        public async Task<HotelDTO> CreateHotel(HotelCreateDTO hotelCreateDTO)
        {
            ValidationResults result = ModelValidator.IsValid(hotelCreateDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var hotelAdmin = hotelCreateDTO.GetHotelAdminInfo();

            string password = GeneratePassword();
            await CheckIfThePasswordIsValid(password);
            await CheckIfTheUserDoesNotExist(hotelAdmin);

            hotelAdmin.EmailConfirmed = true;

            var isCreated = await _userManager.CreateAsync(hotelAdmin, password);
            if (!isCreated.Succeeded)
            {
                throw new DBException("Cann't create new hotel admin");
            }
            var isAddedToRole = await _userManager.AddToRoleAsync(hotelAdmin, "hotel-admin");
            if (!isAddedToRole.Succeeded)
            {
                throw new DBException("Cann't add new user to hotel admin's role");
            }

            var newHotelAdmin = await _userManager.FindByEmailAsync(hotelAdmin.Email);
            var hotel = hotelCreateDTO.ToHotel();
            hotel.HotelAdminId = newHotelAdmin.Id;
            context.Hotels.Create(hotel);
            await context.SaveAsync();
            string message = "Ваш пароль у системi: " + password;
            await EmailService.SendEmailAsync(newHotelAdmin.Email, "HotelLocker", message);
            return hotel.ToHotelDTO();
        }

        public async Task<HotelDTO> EditHotel(HotelDTO hotelDTO, int userId)
        {
            ValidationResults result = ModelValidator.IsValid(hotelDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            var hotel = context.Hotels.Get(hotelDTO.Id);
            if (hotel == null)
                throw new NotFoundException("No such hotel");

            if (hotel.HotelAdminId != userId)
                throw new PermissionException();

           
            if(hotelDTO.Name != null) hotel.Name = hotelDTO.Name;
            if (hotelDTO.Stars != 0) hotel.Stars = hotelDTO.Stars;
            if (hotelDTO.Country != null)  hotel.Country = hotelDTO.Country;
            if (hotelDTO.City != null) hotel.City = hotelDTO.City;
            if (hotelDTO.Address != null) hotel.Address = hotelDTO.Address;
            context.Hotels.Update(hotel);
            await context.SaveAsync();
            return hotel.ToHotelDTO();
        }

        public List<HotelDTO> GetAllHotels(string name,
                                                       string sort,
                                                       bool sortDir,
                                                       int minStars,
                                                       int maxStars,
                                                       string country,
                                                       string city,
                                                       string address)
        {
            var hotels = context.Hotels.GetAll();
            if (name != null)
                hotels = hotels.Where(h => h.Name.Contains(name));
            if (country != null)
                hotels = hotels.Where(h => h.Country.Contains(country));
            if (city != null)
                hotels = hotels.Where(h => h.City.Contains(city));
            if(address != null)
                hotels = hotels.Where(h => h.Address.Contains(address));
            if (minStars != 0)
                hotels = hotels.Where(h => h.Stars >= minStars);
            if (maxStars != 0 && maxStars > minStars)
                hotels = hotels.Where(h => h.Stars <= maxStars);
            if (sort != null)
            {
                if (sort == "name" && sortDir) hotels = hotels.OrderBy(h => h.Name);
                if (sort == "name" && !sortDir) hotels = hotels.OrderByDescending(h => h.Name);

                if (sort == "stars" && sortDir) hotels = hotels.OrderBy(h => h.Stars);
                if (sort == "stars" && !sortDir) hotels = hotels.OrderByDescending(h => h.Stars);
            }
            return hotels.Select(h => h.ToHotelDTO()).ToList();
        }

        private string GeneratePassword()
        {
            return PasswordService.GeneratePassword(8);
        }

        private async Task CheckIfThePasswordIsValid(string password)
        {
            var passwordValidator = new PasswordValidator<User>();
            var isValid = (await passwordValidator.ValidateAsync(_userManager, null, password)).Succeeded;
            if (!isValid) throw new ValidationException("Invalid password");

        }

        private async Task CheckIfTheUserDoesNotExist(User user)
        {
            User foundUser = await _userManager.FindByEmailAsync(user.Email);
            if (foundUser != null) throw new ValidationException("User with this email already exists");

        }
    }
}
