namespace MacroEvolution;
public class Monster
{
    // Monster attributes
    public float Vision { get; set; }
    public float Hunger { get; set; }
    public float Speed { get; set; }
    public float Size { get; set; }
    // The path to the monster's image
    public string ImagePath { get; set; }

    // You can add more attributes as you need

    // public string LegImagePath { get; set; }
    // public string EyeImagePath { get; set; }
    public Monster(float vision, float hunger, float speed, float size)//, string legImagePath, string eyeImagePath
    {
        Vision = vision;
        Hunger = hunger;
        Speed = speed;
        Size = size;

        // LegImagePath = legImagePath;
        // EyeImagePath = eyeImagePath;
    }
}