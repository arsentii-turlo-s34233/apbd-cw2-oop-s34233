namespace apbd_cw2_oop_s34233.Domain.Equipment;

// Projector-specific fields on top of base Equipment
public class Projector : Equipment
{
    public int LumensOutput { get; set; }
    public string Resolution { get; set; }

    public Projector(string name, int lumens, string resolution) : base(name)
    {
        LumensOutput = lumens;
        Resolution = resolution;
    }

    public override string GetDescription()
    {
        return $"Projector: {Name} | Lumens: {LumensOutput} lm | Resolution: {Resolution}";
    }
}