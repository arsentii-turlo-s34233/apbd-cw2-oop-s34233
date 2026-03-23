namespace apbd_cw2_oop_s34233.Domain.Equipment;

public abstract class Equipment
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; set; }
    public bool IsAvailable { get; private set; } = true;
    protected  Equipment(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentNullException("Equipment name can't be empty");
        Name = name;
    }
    public void SetAvailable()
    {
        IsAvailable = true;
    }

    public void SetNotAvailable()
    {
        IsAvailable = false;
    }
    public abstract string GetDescription();

    public override string ToString()
    {
        string shortId = Id.ToString().Substring(0, 8);
        string status = IsAvailable ? "Available" : "Unavailable";
        return $"[{shortId}] {Name} — {status}";
    }

}