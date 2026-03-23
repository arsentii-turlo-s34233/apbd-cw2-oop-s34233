namespace apbd_cw2_oop_s34233.Domain.Users;

public abstract class  User
{
    public Guid Id { get; } = Guid.NewGuid();
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }
    public abstract int MaxActiveRentals { get; }

    protected User(string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException($"User name cannot be empty");
        FirstName = firstName;
        LastName = lastName;
    }

    public override string ToString()
    {
        string shortId = Id.ToString().Substring(0, 8);
        string fullName = FirstName + " " + LastName;
        string typeName = GetType().Name;

        return $"[{shortId}] {fullName} ({typeName})";
    }
    
}