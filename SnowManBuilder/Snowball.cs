using System.Numerics;
using Raylib_cs;

public class Snowball
{
    static Vector3 offset = new(0, 0, 20);
    public int totalCollected = 0;
    public int Size
    {
        get
        {
            if (totalCollected < 40) return 1;
            else return (int)Math.Ceiling((decimal)(totalCollected / 40));
        }
    }
    bool show = true;

    Vector3 position;

    public Snowball(Vector3? pos)
    {
        if (pos == null) position = new(0, 0, 0);
        else position = (Vector3)pos;
    }

    public void Draw(Camera3D camera)
    {
        position = camera.position - offset;
        if (show)
        {
            Raylib.DrawCircle3D(position, Size, new(0, 0, 0), 0f, Color.BLUE);
        }
    }

    public void Draw()
    {
        Raylib.DrawCircle3D(position, Size, new(0, 0, 0), 0f, Color.BLUE);
    }
}
