using HotelLocker.BLL.Configurations;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs;
using HotelLocker.Common.Exceptions;
using HotelLocker.Common.SecurityEncriptor;
using HotelLocker.Common.Validators;
using HotelLocker.DAL.Entities;
using HotelLocker.DAL.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork context;
        private readonly UserManager<User> _userManager;
        private readonly AuthConfiguration _authConfiguration;

        public AccountService(UserManager<User> userManager, AuthConfiguration authConfiguration, IUnitOfWork db)
        {
            context = db;
            _userManager = userManager;
            _authConfiguration = authConfiguration;
        }

        public async Task RegisterAsync(RegistrationDTO registrationDTO)
        {
            ValidationResults result = ModelValidator.IsValid(registrationDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            User newUser = new Guest()
            {
                FirstName = registrationDTO.FirstName,
                LastName = registrationDTO.LastName,
                Email = registrationDTO.Email,
                UserName = registrationDTO.Email,
                Passport = registrationDTO.Passport
            };

            string password = registrationDTO.Password;
            await CheckIfThePasswordIsValid(password);
            await CheckIfTheUserDoesNotExist(newUser);

            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                throw new DBException("Cann't create new user");
            }
            var isAddedToRole = await _userManager.AddToRoleAsync(newUser, "user");
            if (!isAddedToRole.Succeeded)
            {
                throw new DBException("Cann't add new user to user's role");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string message = "Ваш код пiдтвердження: " + code;
            await EmailService.SendEmailAsync(newUser.Email, "HotelLocker пiдтвердження паролю", message);
        }

        public async Task RegisterStaffAsync(RegisterStaffDTO registrationDTO, int userId)
        {
            var hotelAdmin = context.HotelAdmins.Get(userId);
            if (hotelAdmin == null)
                throw new NotFoundException("No such hotel admin");
            int hotelId = hotelAdmin.Hotel.Id;
            ValidationResults result = ModelValidator.IsValid(registrationDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);

            User newUser = new HotelStaff()
            {
                FirstName = registrationDTO.FirstName,
                LastName = registrationDTO.LastName,
                Email = registrationDTO.Email,
                UserName = registrationDTO.Email,
                Category = registrationDTO.Category
            };

            string password = registrationDTO.Password;
            await CheckIfThePasswordIsValid(password);
            await CheckIfTheUserDoesNotExist(newUser);

            var isCreated = await _userManager.CreateAsync(newUser, password);
            if (!isCreated.Succeeded)
            {
                throw new DBException("Cann't create new user");
            }
            var isAddedToRole = await _userManager.AddToRoleAsync(newUser, "hotel-staff");
            if (!isAddedToRole.Succeeded)
            {
                throw new DBException("Cann't add new user to hotel-staff's role");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
            string message = "Ваш код пiдтвердження: " + code;
            await EmailService.SendEmailAsync(newUser.Email, "HotelLocker пiдтвердження паролю", message);
        }

        public async Task ConfirmEmail(ConfirmEmailDTO confirmEmailDTO)
        {
            User user = await _userManager.FindByEmailAsync(confirmEmailDTO.Email);
            if (user == null)
                throw new NotFoundException("There is no user with such email");

            var result = await _userManager.ConfirmEmailAsync(user, confirmEmailDTO.Code);
            if (!result.Succeeded)
                throw new ValidationException("Invalid confirmation code");
        }

        public async Task<TokenResponse> LoginAsync(LoginDTO loginDTO)
        {
            ValidationResults result = ModelValidator.IsValid(loginDTO);
            if (!result.Successed)
                throw ValidationExceptionBuilder.BuildValidationException(result);
            User foundUser = await FindUserByEmail(loginDTO.Email);
            await CheckIfThePasswordIsCorrect(foundUser, loginDTO.Password);
            if (!foundUser.EmailConfirmed)
                throw new ValidationException("User is unconfimed");
            var userRole = await _userManager.GetRolesAsync(foundUser);
            return GenerateJwtToken(foundUser, userRole);

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

        private TokenResponse GenerateJwtToken(User user, IList<string> roles)
        {
            string stringOfRoles = String.Join(" ", roles.ToArray());
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, stringOfRoles),
            };

            var token = new JwtSecurityToken(
                issuer: _authConfiguration.Issuer,
                audience: _authConfiguration.Issuer,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_authConfiguration.ExpireMinutes),
                signingCredentials: new SigningCredentials(
                        _authConfiguration.Key,
                        SecurityAlgorithms.HmacSha256)
            );
            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return new TokenResponse() { AccessToken = jwtToken };
        }

        private async Task<User> FindUserByEmail(string email)
        {
            User foundUser = await _userManager.FindByEmailAsync(email);
            if (foundUser == null) throw new ValidationException("There is no user with such email");
            return foundUser;
        }

        private async Task CheckIfThePasswordIsCorrect(User user, string password)
        {
            bool IsPasswordCorrect = await _userManager.CheckPasswordAsync(user, password);
            if (!IsPasswordCorrect)
                throw new ValidationException("Wrong password");
        }

        private string EncrypteData(string data)
        {
            string key = "123456";
            return SecurityEncriptor.EncryptString(key, data);
        }
    }
}
