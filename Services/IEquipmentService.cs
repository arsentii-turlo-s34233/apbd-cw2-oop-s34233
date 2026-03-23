namespace apbd_cw2_oop_s34233.Services;
using apbd_cw2_oop_s34233.Domain.Equipment;

public interface IEquipmentService
{
    void AddEquipment(Equipment equipment);
    Equipment? GetById(Guid id);
    IReadOnlyList<Equipment> GetAll();
    IReadOnlyList<Equipment> GetAvailable();
    void MarkUnavailable(Guid equipmentId);
}