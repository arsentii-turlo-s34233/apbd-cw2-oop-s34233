using apbd_cw2_oop_s34233.Domain;
using apbd_cw2_oop_s34233.Domain.Equipment;
using apbd_cw2_oop_s34233.Domain.Users;
using apbd_cw2_oop_s34233.Services;
using apbd_cw2_oop_s34233.UI;

namespace apbd_cw2_oop_s34233;

public class Program
{
    public static void Main(string[] args)
    {
        // Wire up all services
        var equipmentService = new EquipmentService();
        var rentalService = new RentalService(equipmentService);
        var reportService = new ReportService(equipmentService, rentalService);
        var ui = new ConsoleUI(equipmentService, rentalService, reportService);

        Console.WriteLine("University Equipment Rental System \n");

        // Add equipment
        Console.WriteLine("Adding equipment...");
        var laptop1 = new Laptop("Dell XPS 15", "Intel i7", 16);
        var laptop2 = new Laptop("MacBook Pro", "Apple M3", 32);
        var projector = new Projector("Epson EB-S41", 3300, "SVGA");
        var camera = new Camera("Canon EOS 250D", 24, true);

        equipmentService.AddEquipment(laptop1);
        equipmentService.AddEquipment(laptop2);
        equipmentService.AddEquipment(projector);
        equipmentService.AddEquipment(camera);

        ui.PrintAllEquipment();

        // Add users
        Console.WriteLine("\n Adding users...");
        var student1 = new Student("Anna", "Nowak");
        var student2 = new Student("Piotr", "Kowalski");
        var employee1 = new Employee("Dr", "Wisniewska");

        rentalService.AddUser(student1);
        rentalService.AddUser(student2);
        rentalService.AddUser(employee1);

        Console.WriteLine($"Added: {student1}");
        Console.WriteLine($"Added: {student2}");
        Console.WriteLine($"Added: {employee1}");

        // Valid rentals
        Console.WriteLine("\n Correct rentals:");
        var r1 = ui.TryRent(student1, laptop1, 7);
        var r2 = ui.TryRent(student1, projector, 3);
        var r3 = ui.TryRent(employee1, camera, 14);

        ui.PrintAvailableEquipment();

        // Invalid operations — should all be blocked
        Console.WriteLine("\n Invalid operations (should be blocked):");

        Console.WriteLine("Attempt to rent already-rented laptop:");
        ui.TryRent(student2, laptop1, 5);

        Console.WriteLine("Attempt to exceed student rental limit (max 2):");
        ui.TryRent(student1, laptop2, 2);

        Console.WriteLine("Mark camera as damaged, then try to rent it:");
        equipmentService.MarkUnavailable(camera.Id);
        ui.TryRent(student2, camera, 1);

        // On-time return
        Console.WriteLine("\n On-time return:");
        if (r2 != null) ui.TryReturn(r2);

        // Late return with penalty
        Console.WriteLine("\n[SCENARIO] Late return:");
        camera.SetAvailable();
        var lateRental = new Rental(student2, camera, 1, DateTime.Now.AddDays(-3));
        rentalService.AddRental(lateRental);

        ui.TryReturn(lateRental);
        ui.PrintActiveRentalsForUser(student1);
        ui.PrintOverdueRentals();
        ui.PrintReport();
    }
}