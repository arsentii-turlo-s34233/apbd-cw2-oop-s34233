using apbd_cw2_oop_s34233.Domain.Equipment;

namespace apbd_cw2_oop_s34233.Services;
using apbd_cw2_oop_s34233.Domain;
using apbd_cw2_oop_s34233.Domain.Users;

public interface IRentalService
{
    void AddUser (User user);
    User? GetUserById(Guid userId);
    Rental RentEquipment (Guid userId, Guid equipmentid, int days);
    Rental ReturnEquipment(Guid rentalId);
    IReadOnlyList<Rental> GetActiveRentalsForUse(Guid userId);
    IReadOnlyList<Rental> GetOverdueRentals();
    IReadOnlyList<Rental> GetAllRentals();
}