namespace apbd_cw2_oop_s34233.Domain.Equipment;

public class Laptop : Equipment
{
    public string Processor { get; set; }
    public int RamGb { get; set; }
    
    public Laptop(string name, string processor, int ramGb) : base(name)
    {
        Processor = processor;
        RamGb = ramGb;
    }

    public override string GetDescription()
    {
        return $"Laptop: {Name} | Processor: {Processor} | RAM: {RamGb} GB"; 
    }
}