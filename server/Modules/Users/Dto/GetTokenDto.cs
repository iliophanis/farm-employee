namespace server.Modules.Users.Dto
{
    public record GetTokenResponseDto(string Token, DateTime Expiration, string DisplayName, int UserId, string Role);
}