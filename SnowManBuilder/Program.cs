using Raylib_cs;

Console.WriteLine("Hello, World!");

//This is a simple 3D test

Engine e = new();

Setup();
Draw();

void Setup()
{
    Raylib.InitWindow(1000, 800, "Snow Builder");
    Raylib.SetTargetFPS(60);
}

void Draw()
{
    while (!Raylib.WindowShouldClose())
    {
        e.Run();
    }
}