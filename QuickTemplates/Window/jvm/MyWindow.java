package MyWindowTemplate;

import java.awt.Color;
import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.Flags.MSAA;

public class MyWindow extends ActiveWindow {
    // implement ActiveWindow class
    @Override
    public void initWindow() {
        // window parameters
        setParameters("MyWindow", "MyWindow", 800, 600);

        // optional: basic parameters
        setBackground(Color.gray); // backgroung color
        setMaxSize(1600, 1200); // max size of the window
        setMinSize(400, 300); // min size of the window
        setAntiAliasingQuality(MSAA.MSAA_8X); // antialiasing quality, default: msaa 4x
        setPadding(2, 2, 2, 2); // indents for items
        setPosition(200, 200); // position on the screen where the window appears
        setAspectRatio(4, 3); // enable aspect ratio, default: disable

        // optional: this parameters should be set before calling the show() function
        isBorderHidden = true; // window decoration parameter - create window with/without border and native title bar, default: false
        isCentered = true; // window will appear in the center of the screen, default: false
        isResizable = false; // enable/disable window realizability, default: true
        isAlwaysOnTop = true; // window will be always on top of others windows, default: false
        isTransparent = true; // enable/disable window transparency, default: false
    }
}