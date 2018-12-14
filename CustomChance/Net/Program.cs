using System;
using System.Drawing;
using SpaceVIL;
using System.Diagnostics;
using CustomChance;
using SpaceVIL.Common;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            MainWindow app = new MainWindow();
            WindowLayoutBox.TryShow(nameof(MainWindow));
        }
    }
}
