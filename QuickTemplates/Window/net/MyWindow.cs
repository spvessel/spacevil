using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;

namespace MyWindowTemplate
{
    public class MyWindow : ActiveWindow
    {
        // implement ActiveWindow class
        public override void InitWindow()
        {
            // window parameters
            SetParameters("MyWindow", "MyWindow", 800, 600);

            // optional: basic parameters
            SetBackground(Color.Gray); // backgroung color
            SetMaxSize(1600, 1200); // max size of the window
            SetMinSize(400, 300); // min size of the window
            SetAntiAliasingQuality(MSAA.MSAA8x); // antialiasing quality, default: msaa 4x
            SetPadding(2, 2, 2, 2); // indents for items
            SetPosition(200, 200); // position on the screen where the window appears
            SetAspectRatio(4, 3); // enable aspect ratio, default: disable

            // optional: this parameters should be set before calling the show() function
            IsBorderHidden = true; // window decoration parameter - create window with/without border and native title bar, default: false
            IsCentered = true; // window will appear in the center of the screen, default: false
            IsResizable = false; // enable/disable window realizability, default: true
            IsAlwaysOnTop = true; // window will be always on top of others windows, default: false
            IsTransparent = true; // enable/disable window transparency, default: false
        }
    }
}