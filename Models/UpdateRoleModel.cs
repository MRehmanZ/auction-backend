namespace AuctionBackend.Models;
public class UpdateRoleModel
{
    public enum Role
    {
        ADMIN,
        USER
    }

    public string RoleId { get; set; }
    public string NewRoleName { get; set; }
}