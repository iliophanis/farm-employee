using System.Security.Claims;

namespace server.Modules.Common.Services
{
    public class CurrentUserService
    {

        public string UserIP { get; }
        public string UserName { get; }
        public string UserId { get; }
        public bool IsAuthenticated { get; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            UserIP = httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
            UserId = httpContextAccessor.HttpContext!.User.FindFirstValue("Id");
            IsAuthenticated = UserId != null;
            UserName = httpContextAccessor.HttpContext!.User.FindFirstValue("UserName");
        }
    }


}