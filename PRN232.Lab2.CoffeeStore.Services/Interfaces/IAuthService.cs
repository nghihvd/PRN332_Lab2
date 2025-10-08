using PRN232.Lab2.CoffeeStore.Services.Models.Requests.Auth;
using PRN232.Lab2.CoffeeStore.Services.Models.Responses;

namespace PRN232.Lab2.CoffeeStore.Services.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseModel> RegisterAsync(RegisterRequestModel request);
        Task<AuthResponseModel> LoginAsync(LoginRequestModel request);
    }
}


