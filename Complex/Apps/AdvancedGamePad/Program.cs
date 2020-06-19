using System;

using SpaceVIL.Common;

namespace AdvancedGamePad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(CommonService.GetSpaceVILInfo());
            CommonService.InitSpaceVILComponents();

            Factory.Styles.ReplaceBasicStyles();
            
            Controller controller = new Controller();
            controller.Start();
        }
    }
}
