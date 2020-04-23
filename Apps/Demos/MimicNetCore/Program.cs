using System;
using SpaceVIL;
using SpaceVIL.Common;

namespace MimicSpace
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CommonService.GetSpaceVILInfo());
            CommonService.InitSpaceVILComponents();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
