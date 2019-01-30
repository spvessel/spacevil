using System;
using SpaceVIL;
using SpaceVIL.Common;

namespace MimicSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonService.InitSpaceVILComponents();
            MainWindow mw = new MainWindow();
            WindowLayoutBox.TryShow(nameof(MainWindow));
            Console.WriteLine("Ready.");
        }
    }
}
