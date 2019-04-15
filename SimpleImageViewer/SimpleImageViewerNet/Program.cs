using System;
using SpaceVIL.Common;

namespace SimpleImageViewer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CommonService.InitSpaceVILComponents());
            CustomStyles.InitWparGridStyle();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
