using System;
using SpaceVIL;

namespace EnigmaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            View.MainWindow mw = new View.MainWindow();
            WindowLayoutBox.TryShow(mw.GetWindowGuid());
            Console.WriteLine("Ready...");
        }
    }
}
