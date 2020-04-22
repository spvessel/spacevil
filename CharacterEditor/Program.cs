using System;

namespace CharacterEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(SpaceVIL.Common.CommonService.GetSpaceVILInfo());
            if (!SpaceVIL.Common.CommonService.InitSpaceVILComponents())
                return;

            Model model = new Model();
            Controller controller = new Controller(model);
            controller.Start();
        }
    }
}
