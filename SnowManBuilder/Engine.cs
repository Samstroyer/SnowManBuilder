using System.Numerics;
using Raylib_cs;

public class Engine
{
    Camera3D camera = new(new(0, 15, 65), new(0, 0, 50), new(0, 1, 0), 60f, CameraProjection.CAMERA_PERSPECTIVE);
    SnowLayer snow = new();

    Snowball currentSnowball = new(new(0, 10, 55));

    List<Snowball> stationarySnowballs = new();

    bool showHud = true;

    public void Run()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.BLACK);
        Raylib.UpdateCamera(ref camera);
        Raylib.BeginMode3D(camera);

        Keybinds();
        DrawMap();
        ShowSnow();

        Raylib.EndMode3D();

        HUD();

        Raylib.EndDrawing();
    }

    private void HUD()
    {
        if (!showHud)
        {
            Raylib.DrawRectangleLines(10, 10, 150, 20, Color.BLUE);
            Raylib.DrawText("H to show controls", 15, 15, 12, Color.RED);
            return;
        }

        Raylib.DrawRectangleLines(10, 10, 500, 180, Color.BLUE);
        Raylib.DrawText("WASD to move horizontal", 20, 20, 32, Color.RED);
        Raylib.DrawText("IO to move vertically", 20, 60, 32, Color.RED);
        Raylib.DrawText("U to place snowball", 20, 100, 32, Color.RED);
        Raylib.DrawText("H to toggle this rectangle", 20, 140, 32, Color.RED);
    }

    private void ShowSnow()
    {
        currentSnowball.Draw(camera);

        foreach (Snowball sb in stationarySnowballs)
        {
            sb.Draw();
        }
    }

    private void Keybinds()
    {
        KeyboardKey key = (KeyboardKey)Raylib.GetKeyPressed();

        if (Raylib.IsKeyDown(KeyboardKey.KEY_A)) { camera.position.X--; camera.target.X--; }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_D)) { camera.position.X++; camera.target.X++; }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_W)) { camera.position.Z--; camera.target.Z--; }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_S)) { camera.position.Z++; camera.target.Z++; }

        if (Raylib.IsKeyDown(KeyboardKey.KEY_I)) { camera.position.Y--; camera.target.Y--; }
        if (Raylib.IsKeyDown(KeyboardKey.KEY_O)) { camera.position.Y++; camera.target.Y++; }

        Vector2 change = Raylib.GetMouseDelta();
        camera.target.X += change.X / 20;
        camera.target.Y += -change.Y / 20;

        if (Raylib.IsMouseButtonPressed(MouseButton.MOUSE_BUTTON_LEFT)) ToggleCursor();

        if (key == KeyboardKey.KEY_U) { stationarySnowballs.Add(currentSnowball); currentSnowball = new(camera.position - new Vector3(0, 5, 10)); }
        if (key == KeyboardKey.KEY_H) showHud = !showHud;
    }

    private void ToggleCursor()
    {
        bool disabled = Raylib.IsCursorHidden();
        if (disabled) Raylib.EnableCursor();
        else Raylib.DisableCursor();
    }

    private void DrawMap()
    {
        snow.Draw();
        currentSnowball.totalCollected += snow.Update(camera);
    }
}
