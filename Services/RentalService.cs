namespace apbd_cw2_oop_s34233.Services;
using apbd_cw2_oop_s34233.Domain;
using apbd_cw2_oop_s34233.Domain.Users;
public class RentalService : IRentalService
{
    private readonly IEquipmentService _equipment;
    private readonly List<User> _users = new ();
    private readonly List<Rental> _rentals = new();
    
    public RentalService(IEquipmentService equipmentService)
    {
        _equipment = equipmentService;
    }

    public void AddUser(User user)
    {
        if (user == null)
            throw new ArgumentNullException(nameof(user));
        _users.Add(user);
    }
    
    public User? GetUserById(Guid id) => _users.FirstOrDefault(u => u.Id == id);

    public Rental RentEquipment(Guid userId, Guid equipmentId, int days)
    {
        var user = _users.FirstOrDefault(u => u.Id == userId)
                   ?? throw new KeyNotFoundException($"User {userId} not found");
        var item = _equipment.GetById(equipmentId)
            ?? throw new KeyNotFoundException($"Equipment {equipmentId} not found");
        
        if(!item.IsAvailable)
            throw new InvalidOperationException(
                $"'{item.Name}' is not available for rental");
        
        int activeCount = _rentals.Count(r => r.User.Id == userId && r.IsActive);
        if (activeCount >= user.MaxActiveRentals)
            throw new InvalidOperationException(
                $"{user.FirstName} has reached the limit of {user.MaxActiveRentals} active rentals");
        item.SetNotAvailable();
        var rental = new Rental(user, item, days);
        _rentals.Add(rental);
        return rental;
    }

    public Rental ReturnEquipment(Guid rentalId)
    {
        var rental = _rentals.FirstOrDefault(r => r.Id == rentalId)
            ?? throw new KeyNotFoundException($"Rental {rentalId} not found");
        if (!rental.IsActive)
            throw new InvalidOperationException("Rental is already closed");
        decimal penalty = PenaltyCalculator.CalculatePenalty(rental.DueDate, DateTime.Now);
        rental.CompleteReturn(penalty);
        return rental;
    }

    public IReadOnlyList<Rental> GetActiveRentalsForUser(Guid userId) => _rentals.Where (r => r.User.Id == userId && r.IsActive).ToList().AsReadOnly();
    public IReadOnlyList<Rental> GetOverdueRentals() => _rentals.Where (r => r.IsOverdue).ToList().AsReadOnly();
    public IReadOnlyList<Rental> GetAllRentals() => _rentals.AsReadOnly();
    public void AddRental(Rental rental)
    {
        _rentals.Add(rental);
        rental.Equipment.SetNotAvailable();
    }
}