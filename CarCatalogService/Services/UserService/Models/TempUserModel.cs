namespace CarCatalogService.Services.UserService.Models;

public class TempUserModel
{
    public long Id { get; set; }
    public required string Login { get; set; }
    public required IEnumerable<string> Roles { get; set;}
}
