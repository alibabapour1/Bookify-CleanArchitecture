namespace Bookify.Domain.Users;

public sealed class Permissions
{
    public static readonly Permissions UserRead = new(1, "users:read");
    public Permissions(int id , string name)
    {
        Id = id;
        Name = name;
    }
    public int Id { get; init; }

    public string Name { get; init; } 
}