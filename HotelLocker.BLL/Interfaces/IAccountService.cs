using HotelLocker.Common.DTOs;
using System.Threading.Tasks;

namespace HotelLocker.BLL.Interfaces
{
    public interface IAccountService
    {
        Task RegisterAsync(RegistrationDTO registrationDTO);
        Task RegisterStaffAsync(RegisterStaffDTO registrationDTO, int userId);
        Task ConfirmEmail(ConfirmEmailDTO confirmEmailDTO);
        Task<TokenResponse> LoginAsync(LoginDTO loginDTO);
    }
}
