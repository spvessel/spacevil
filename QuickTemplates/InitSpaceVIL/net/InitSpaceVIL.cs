using System;
using SpaceVIL.Common;

namespace InitTemplate
{
    class Program
    {
        static void Main(string[] args)
        {
            // show SpaceVIL info: version, platform, os
            Console.WriteLine(CommonService.GetSpaceVILInfo());
            // initialize SpaceVIL components
            CommonService.InitSpaceVILComponents();

            // optional: create your window and show it
            MyWindow mw = new MyWindow();
            mw.Show();
        }
    }
}
