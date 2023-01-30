using System.Numerics;
using Raylib_cs;

public class SnowLayer
{
    static int size = 1;
    static int gridSize = 100;
    List<List<(Vector2 position, bool available)>> snowTiles = new();

    public SnowLayer()
    {
        for (int i = 0; i < gridSize; i++)
        {
            snowTiles.Add(new());
            for (int j = 0; j < gridSize; j++)
            {
                snowTiles[i].Add(new(new(i - gridSize / 2, j - gridSize / 2), true));
            }
        }
    }

    public void Draw()
    {
        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                Color c = Color.WHITE;

                if (!snowTiles[i][j].available) c = Color.GRAY;

                Vector2 pos = snowTiles[i][j].position;

                Raylib.DrawCube(new(pos.X, 0, pos.Y), size, size, size, c);
                Raylib.DrawCubeWires(new(pos.X, 0, pos.Y), size, size, size, Color.BLACK);
            }
        }
    }

    public int Update(Camera3D camera)
    {
        Vector3 relativePos = camera.position - new Vector3(-gridSize / 2, 0, -gridSize / 2);

        if (relativePos.Y < 0 || relativePos.Y > 10) return 0;

        if (relativePos.X >= 0 / 2 && relativePos.X < gridSize)
        {
            if (relativePos.Z >= 0 / 2 && relativePos.Z < gridSize)
            {
                (Vector2, bool) newTile = snowTiles[(int)relativePos.X][(int)relativePos.Z];

                if (!newTile.Item2) return 0;
                newTile.Item2 = false;
                snowTiles[(int)relativePos.X][(int)relativePos.Z] = newTile;

                return 1;
            }
        }

        return 0;
    }
}
