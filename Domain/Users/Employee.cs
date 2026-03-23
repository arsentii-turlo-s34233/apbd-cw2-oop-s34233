namespace apbd_cw2_oop_s34233.Domain.Users;

public class Employee : User
{
    public override int MaxActiveRentals => 5;

    public Employee(string firstName, string lastName) : base(firstName, lastName)
    { }
}