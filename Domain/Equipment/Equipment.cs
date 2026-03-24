namespace apbd_cw2_oop_s34233.Domain.Equipment;

// Base class for all equipment types - holds shared data and status logic
public abstract class Equipment
{
    // Each item gets a unique ID on creation
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }

    // Current state of the item - available, rented, or damaged
    public EquipmentStatus Status { get; private set; } = EquipmentStatus.Available;

    // Shortcut check used by rental logic
    public bool IsAvailable => Status == EquipmentStatus.Available;

    protected Equipment(string name)
    {
        // Name can't be empty - fails fast if someone passes null or whitespace
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Equipment name can't be empty");
        Name = name;
    }

    // Mark as back in stock after return
    public void SetAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    // Mark as taken when rented out
    public void SetNotAvailable()
    {
        Status = EquipmentStatus.Rented;
    }

    // Mark as broken - can't be rented until fixed
    public void SetDamages()
    {
        Status = EquipmentStatus.Damaged;
    }

    // Each subclass must describe itself
    public abstract string GetDescription();

    public override string ToString()
    {
        string shortId = Id.ToString().Substring(0, 8);
        string status = Status.ToString();
        return $"[{shortId}] {Name} — {status}";
    }
}