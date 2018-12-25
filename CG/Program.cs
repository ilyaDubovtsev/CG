using System.Reflection;
using CG.Painter;
using OpenTK;

namespace CG
{
    class Program
    {
        static void Main()
        {
            GameWindow window = new GameWindow(800, 500);
            
            Game game = new Game(window);
            
            window.Run();
            
        }
    }
}