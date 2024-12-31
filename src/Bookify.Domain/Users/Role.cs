namespace Bookify.Domain.Users;

public class Role
{
    public static Role Registered = new Role(1,"Registered");
    public Role(int id , string name)
    {
        Id = id;
        Name = name;
    }

    public int Id { get; init; }
    public string Name { get; init; } 

    public ICollection<User> Users { get; init; } = new List<User>();
    public ICollection<Permissions> Permissions { get; init; } = new List<Permissions>();
}