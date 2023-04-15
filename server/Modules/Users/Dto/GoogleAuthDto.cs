namespace server.Modules.Users.Dto
{
    public record GoogleAuthDto(string UserName, string FirstName, string LastName);

    public record GoogleAuthResponseDto(string Token, DateTime Expiration, string DisplayName, int UserId, List<string> Roles); //List<string>
                                                                                            //why list shouldn't it return farmer or employee? 
}