namespace apbd_cw2_oop_s34233.Domain.Users;

public class Student : User
{
    public override int MaxActiveRentals => 2;
    public Student (string firstName, string lastName) : base(firstName, lastName)
    {}
}