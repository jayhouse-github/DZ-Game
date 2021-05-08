using System;

namespace DZ_Game
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new DzGame())
                game.Run();
        }
    }
}
