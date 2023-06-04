using System.Security.Claims;

namespace server.Modules.Common.Services
{
    public class CurrentUserService
    {

        public string UserIP { get; }
        public string UserName { get; }
        public string UserId { get; }
        public bool IsAuthenticated { get; }
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IEnumerable<Claim> UserClaims { get; set; }

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            UserIP = httpContextAccessor.HttpContext!.Connection.RemoteIpAddress!.ToString();
            UserId = httpContextAccessor.HttpContext!.User.FindFirstValue("Id");
            IsAuthenticated = UserId != null;
            UserName = httpContextAccessor.HttpContext!.User.FindFirstValue("UserName");
            UserClaims = httpContextAccessor.HttpContext?.User.Claims;
        }


        public IEnumerable<string> GetRoles()
        {
            var result = UserClaims.Where(c => c.Type == ClaimTypes.Role).Select(s => s.Value);
            return result;
        }

        public int? GetEmployeeId()
        {
            if (!this.IsEmployee()) return null;
            var employeeId = _httpContextAccessor.HttpContext!.User.FindFirstValue("EmployeeId");
            return Convert.ToInt32(employeeId);
        }

        public int? GetFarmerId()
        {
            if (!this.IsFarmer()) return null;
            var farmerId = _httpContextAccessor.HttpContext!.User.FindFirstValue("FarmerId");
            return Convert.ToInt32(farmerId);
        }

        public bool IsAdministrator()
        {
            var result = UserClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            return result;
        }

        public bool IsEmployee()
        {
            var result = UserClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Employee");
            return result;
        }
        public bool IsFarmer()
        {
            var result = UserClaims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Farmer");
            return result;
        }



    }


}