namespace apbd_cw2_oop_s34233.Services;
using apbd_cw2_oop_s34233.Domain.Equipment;

// Manages the list of all equipment items
public class EquipmentService : IEquipmentService
{
    private readonly List<Equipment> _items = new();

    public void AddEquipment(Equipment equipment)
    {
        if (equipment == null)
            throw new ArgumentNullException(nameof(equipment));
        _items.Add(equipment);
    }

    public Equipment? GetById(Guid id) => _items.FirstOrDefault(e => e.Id == id);
    public IReadOnlyList<Equipment> GetAll() => _items.AsReadOnly();

    // Returns only items that are currently available to rent
    public IReadOnlyList<Equipment> GetAvailable() => _items.Where(e => e.IsAvailable).ToList().AsReadOnly();

    // Marks item as damaged - blocks it from being rented
    public void MarkUnavailable(Guid equipmentId)
    {
        var item = GetById(equipmentId)
                   ?? throw new KeyNotFoundException($"Equipment with id {equipmentId} does not exist");
        item.SetDamages();
    }
}