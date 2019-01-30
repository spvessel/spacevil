﻿using System;
using SpaceVIL;
using SpaceVIL.Common;

namespace EnigmaGame
{
    class Program
    {
        static void Main(string[] args)
        {
            CommonService.InitSpaceVILComponents();
            View.MainWindow mw = new View.MainWindow();
            WindowLayoutBox.TryShow(mw.GetWindowGuid());
            Console.WriteLine("Ready...");
        }
    }
}
