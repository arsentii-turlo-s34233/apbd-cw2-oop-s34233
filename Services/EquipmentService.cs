namespace apbd_cw2_oop_s34233.Services;
using apbd_cw2_oop_s34233.Domain.Equipment;

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
    public IReadOnlyList<Equipment> GetAvailable() => _items.Where(e => e.IsAvailable).ToList().AsReadOnly();

    public void MarkUnavailable(Guid equipmentId)
    {
        var item = GetById(equipmentId)
            ?? throw new KeyNotFoundException($"Equipment with id {equipmentId} does not exist");
        item.SetNotAvailable();
    }
}