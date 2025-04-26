using Core.Customized.CustomizePoints.DocumentCentrePoints.Model;
using Refit;
namespace Core.Customized.CustomizePoints.DocumentCentrePoints.Interface
{
    public interface IAuthorizationInterfaceClient
    {
        [Get("/getnameproject")]
        Task<UserResponse> GetUserAsync();
    }
}
