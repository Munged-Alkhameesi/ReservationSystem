using DatabaseReservation.Data;
using DatabaseReservation.Models;
using NuGet.Common;

namespace DatabaseReservation.Service
{
    public interface IUserService
    {
        Task<Status> RegisterAsync(Register model);

        Task<Status> LoginAsync(Login model);
        Task<Status> LogoutAsync();

    }
}
