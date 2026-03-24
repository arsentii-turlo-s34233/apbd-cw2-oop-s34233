using EquipmentModel = apbd_cw2_oop_s34233.Domain.Equipment.Equipment;
using UserModel = apbd_cw2_oop_s34233.Domain.Users.User;
namespace apbd_cw2_oop_s34233.Domain;

// Represents one rental - who rented what, when, and current state
public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();
    public UserModel User { get; }
    public EquipmentModel Equipment { get; }
    public DateTime RentedOn { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal PenaltyFee { get; private set; }

    // Null means not returned yet
    public bool IsActive => ReturnDate == null;

    // Still active but past the due date
    public bool IsOverdue => IsActive && DateTime.Now > DueDate;

    // Computed from IsActive and IsOverdue - no manual tracking needed
    public RentalStatus Status =>
        !IsActive ? RentalStatus.Returned :
        IsOverdue  ? RentalStatus.Overdue :
                     RentalStatus.Active;

    public Rental(UserModel user, EquipmentModel equipment, int rentalDays)
    {
        User = user;
        Equipment = equipment;
        RentedOn = DateTime.Now;
        DueDate = RentedOn.AddDays(rentalDays);
    }

    // Used for testing late returns - sets a custom due date directly
    internal Rental(UserModel user, EquipmentModel equipment, int rentalDays, DateTime dueDate)
    {
        User = user;
        Equipment = equipment;
        RentedOn = DateTime.Now;
        DueDate = dueDate;
    }

    // Closes the rental, stores penalty, frees the equipment
    public void CompleteReturn(decimal penaltyAmount)
    {
        ReturnDate = DateTime.Now;
        PenaltyFee = penaltyAmount;
        Equipment.SetAvailable();
    }

    public override string ToString()
    {
        string shortId = Id.ToString().Substring(0, 8);
        string renterName = $"{User.FirstName} {User.LastName}";
        string status = Status.ToString().ToUpper();
        return $"Rental [{shortId}] - {Equipment.Name} rented by {renterName} | Due: {DueDate:dd MMM yyyy} | {status}";
    }
}