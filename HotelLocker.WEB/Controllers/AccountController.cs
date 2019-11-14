using System.Threading.Tasks;
using HotelLocker.BLL.Extensions;
using HotelLocker.BLL.Interfaces;
using HotelLocker.Common.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelLocker.WEB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountController(IAccountService service)
        {
            accountService = service;
        }

        [HttpPost]
        [Route("register")]
        public async Task RegisterUser([FromBody]RegistrationDTO registerDTO)
        {
            await accountService.RegisterAsync(registerDTO);
        }

        [Authorize(Roles = "hotel-admin")]
        [HttpPost]
        [Route("register-staff")]
        public async Task RegisterStaff([FromBody]RegisterStaffDTO registerDTO)
        {
            await accountService.RegisterStaffAsync(registerDTO, GetUserId());
        }

        [HttpPost]
        [Route("confirm")]
        public async Task ConfirmEmail([FromBody]ConfirmEmailDTO confirmEmailDTO)
        {
            await accountService.ConfirmEmail(confirmEmailDTO);
        }

        [HttpPost]
        [Route("login")]
        public async Task<TokenResponse> Login([FromBody]LoginDTO loginDTO)
        {
            return await accountService.LoginAsync(loginDTO);
        }

        private int GetUserId()
        {
            return User.ConvertToUserData().Id;
        }
    }
}