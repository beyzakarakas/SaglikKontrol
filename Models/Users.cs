using DiabetWebSite.Models;

public class User
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string PasswordHash { get; set; }
    public string Email { get; set; }
    public DateTime CreatedAt { get; set; }

    // İlişki tanımlaması
    public ICollection<BodyMassIndex> BodyMassIndexes { get; set; }
}
