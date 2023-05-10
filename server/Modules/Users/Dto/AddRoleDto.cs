namespace server.Modules.Users.Dto
{

    public record ContactInfoDto(string Address,
                                 string City,
                                 string Tk,
                                 string PhoneNo,
                                 string MobilePhoneNo);
    public record AddRoleDto(int RoleId, string UserName, ContactInfoDto ContactInfo);

    public record AddRoleResponseDto();
}