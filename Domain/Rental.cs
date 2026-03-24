using EquipmentModel = apbd_cw2_oop_s34233.Domain.Equipment.Equipment;
using UserModel = apbd_cw2_oop_s34233.Domain.Users.User;
namespace apbd_cw2_oop_s34233.Domain;


public class Rental
{
    public Guid Id { get; } = Guid.NewGuid();
    public UserModel User { get; }
    public EquipmentModel Equipment { get; }
    public DateTime RentedOn { get; }
    public DateTime DueDate { get; }
    public DateTime? ReturnDate { get; private set; }
    public decimal PenaltyFee { get; private set; }
    
    public bool IsActive => ReturnDate == null;
    public bool IsOverdue => IsActive && DateTime.Now > DueDate;
    
    public Rental(UserModel user, EquipmentModel equipment, int rentalDays)
        {
        User = user;
        Equipment = equipment;
        RentedOn = DateTime.Now;
        DueDate = RentedOn.AddDays(rentalDays);
        }
    internal Rental(UserModel user, EquipmentModel equipment, int rentalDays, DateTime dueDate)
    {
        User = user;
        Equipment = equipment;
        RentedOn = DateTime.Now;
        DueDate = dueDate;
    }

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
        string status = IsActive ? "ACTIVE" : "RETURNED";

        return $"Rental [{shortId}] - {Equipment.Name} rented by {renterName} | Due: {DueDate:dd MMM yyyy} | {status}";
    }
    

}