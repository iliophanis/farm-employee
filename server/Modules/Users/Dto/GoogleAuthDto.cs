namespace server.Modules.Users.Dto
{
    public record GoogleAuthDto(string UserName, string FirstName, string LastName);

    public record GoogleAuthResponseDto(bool isNewUser); //string
}