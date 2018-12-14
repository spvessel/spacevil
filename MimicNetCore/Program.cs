using System;
using SpaceVIL;

namespace MimicSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            MainWindow mw = new MainWindow();
            WindowLayoutBox.TryShow(nameof(MainWindow));
            Console.WriteLine("Ready.");
        }
    }
}
