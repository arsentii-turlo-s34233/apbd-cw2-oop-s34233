namespace apbd_cw2_oop_s34233.Domain.Users;

public class Employee : User
{
    // Employees can have at most 5 active rentals
    public override int MaxActiveRentals => 5;

    public Employee(string firstName, string lastName) : base(firstName, lastName)
    { }
}