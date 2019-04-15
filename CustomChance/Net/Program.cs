using System;
using System.Drawing;
using CustomChance;
using SpaceVIL;
using SpaceVIL.Common;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonService.InitSpaceVILComponents();
            MainWindow app = new MainWindow();
            app.Show();
        }
    }
}
