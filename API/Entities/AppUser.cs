namespace API.Entities;
#nullable disable
public class AppUser
{
    public int Id { get; set; }
    public string Username { get; set; }

    public required byte[] PasswordHash { get; set; }
    public required byte[] PasswordSalt { get; set; }
}

