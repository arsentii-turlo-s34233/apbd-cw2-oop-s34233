namespace apbd_cw2_oop_s34233.Domain.Users;

public class Student : User
{
    // Students can have at most 2 active rentals
    public override int MaxActiveRentals => 2;

    public Student(string firstName, string lastName) : base(firstName, lastName)
    {}
}