namespace apbd_cw2_oop_s34233.Domain.Users;

// Base class for all user types - holds identity and rental limit rule
public abstract class User
{
    // Each user gets a unique ID on creation
    public Guid Id { get; } = Guid.NewGuid();
    public virtual string FirstName { get; set; }
    public virtual string LastName { get; set; }

    // Each user type sets their own rental limit
    public abstract int MaxActiveRentals { get; }

    protected User(string firstName, string lastName)
    {
        // First and last name can't be empty
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