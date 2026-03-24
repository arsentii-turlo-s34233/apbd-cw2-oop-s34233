namespace apbd_cw2_oop_s34233.UI;
using apbd_cw2_oop_s34233.Domain;
using apbd_cw2_oop_s34233.Domain.Equipment;
using apbd_cw2_oop_s34233.Domain.Users;
using apbd_cw2_oop_s34233.Services;

// Handles all console output - keeps print logic out of Program.cs
public class ConsoleUI
{
    private readonly IEquipmentService _equipment;
    private readonly IRentalService _rentals;
    private readonly ReportService _reports;

    public ConsoleUI(IEquipmentService equipment, IRentalService rentals, ReportService reports)
    {
        _equipment = equipment;
        _rentals = rentals;
        _reports = reports;
    }

    public void PrintAllEquipment()
    {
        Console.WriteLine("\n All Equipment");
        foreach (var e in _equipment.GetAll())
            Console.WriteLine($"{e} | {e.GetDescription()}");
    }

    public void PrintAvailableEquipment()
    {
        Console.WriteLine("\n Available Equipment");
        foreach (var e in _equipment.GetAvailable())
            Console.WriteLine($"{e}");
    }

    public void PrintActiveRentalsForUser(User user)
    {
        Console.WriteLine($"\n Active Rentals for {user.FirstName} {user.LastName}");
        var list = _rentals.GetActiveRentalsForUser(user.Id);
        if (!list.Any())
        {
            Console.WriteLine("(none)");
            return;
        }
        foreach (var r in list)
            Console.WriteLine($"{r}");
    }

    public void PrintOverdueRentals()
    {
        Console.WriteLine("\n Overdue Rentals");
        var list = _rentals.GetOverdueRentals();
        if (!list.Any())
        {
            Console.WriteLine("(none)");
            return;
        }
        foreach (var r in list)
            Console.WriteLine($"{r}");
    }

    public void PrintReport()
    {
        Console.WriteLine("\n" + _reports.GenerateSummary());
    }

    // Tries to rent — prints result or blocked reason
    public Rental? TryRent(User user, Equipment item, int days)
    {
        try
        {
            var rental = _rentals.RentEquipment(user.Id, item.Id, days);
            Console.WriteLine($"Rented: {item.Name} to {user.FirstName} {user.LastName} for {days} days");
            return rental;
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Rental blocked: {ex.Message}");
            return null;
        }
    }

    // Tries to return — prints penalty if late
    public void TryReturn(Rental rental)
    {
        try
        {
            var r = _rentals.ReturnEquipment(rental.Id);
            if (r.PenaltyFee > 0)
                Console.WriteLine($"Returned (Late). Penalty: {r.PenaltyFee:C}");
            else
                Console.WriteLine($"Returned: {r} on time. No penalty.");
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine($"Return failed: {ex.Message}");
        }
    }
}