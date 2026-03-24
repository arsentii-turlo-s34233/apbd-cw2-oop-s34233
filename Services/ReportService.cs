namespace apbd_cw2_oop_s34233.Services;

public class ReportService
{
    private readonly IEquipmentService _equipment;
    private readonly IRentalService _rentals;

    public ReportService(IEquipmentService equipment, IRentalService rentals)
    {
        _equipment = equipment;
        _rentals = rentals;
    }

    public string GenerateSummary()
    {
        var all = _equipment.GetAll();
        var available = _equipment.GetAvailable();
        var allRentals = _rentals.GetAllRentals();
        var active = allRentals.Where(r => r.IsActive).ToList();
        var overdue = _rentals.GetOverdueRentals();
        var returned = allRentals.Where(r => !r.IsActive).ToList();
        decimal totalPenalties = returned.Sum(r => r.PenaltyFee);
        return
            $"Rental System Summary {Environment.NewLine}" +
            $"Equipment total: {all.Count} {Environment.NewLine}" +
            $"Available now: {available.Count} {Environment.NewLine}" +
            $"Active rentals: {active.Count} {Environment.NewLine}" +
            $"Overdue rentals: {overdue.Count} {Environment.NewLine}" +
            $"Total penalties: {totalPenalties:C}";
    }
}