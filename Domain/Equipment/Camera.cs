namespace apbd_cw2_oop_s34233.Domain.Equipment;

public class Camera : Equipment
{
    public int Megapixels {get; set;}
    public bool HasLens {get; set;}
    
    public Camera(string name, int mp, bool hasLens) : base(name)
        {
        Megapixels = mp;
        HasLens = hasLens;
        }

    public override string GetDescription()
    {
        return $"Camera: {Name} | Megapixels: {Megapixels} | Lens included: {HasLens}";
    }
}