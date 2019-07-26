using System;
using SpaceVIL.Common;

namespace RadialMenu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CommonService.GetSpaceVILInfo());
            if (!CommonService.InitSpaceVILComponents())
                return;

            Model model = new Model();
            Controller controller = new Controller(model);
            controller.Start();
        }
    }
}
