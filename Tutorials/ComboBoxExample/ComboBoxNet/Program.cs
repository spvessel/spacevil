using System;
using SpaceVIL;

namespace ComboBoxExample
{
    class Program
    {
        static void Main(string[] args)
        {
            SpaceVIL.Common.CommonService.InitSpaceVILComponents();
            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}
