using System;

namespace CharacterEditor
{
    class Program
    {
        static void Main(string[] args)
        {
            if (!SpaceVIL.Common.CommonService.InitSpaceVILComponents())
                return;

            Model model = new Model();
            Controller controller = new Controller(model);
            controller.Start();
        }
    }
}
