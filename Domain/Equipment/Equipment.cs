namespace apbd_cw2_oop_s34233.Domain.Equipment;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public EquipmentStatus Status { get; private set; } = EquipmentStatus.Available;
    public bool IsAvailable => Status == EquipmentStatus.Available;
    protected  Equipment(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Equipment name can't be empty");
        Name = name;
    }
    public void SetAvailable()
    {
        Status = EquipmentStatus.Available;
    }

    public void SetNotAvailable()
    {
        Status = EquipmentStatus.Rented;
    }

    public void SetDamages()
    {
        Status = EquipmentStatus.Damaged;
    }
    public abstract string GetDescription();

    public override string ToString()
    {
        string shortId = Id.ToString().Substring(0, 8);
        string status = Status.ToString();
        return $"[{shortId}] {Name} — {status}";
    }

}