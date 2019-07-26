package RadialMenu;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.TitleBar;
import com.spvessel.spacevil.Flags.MSAA;

public class MainWindow extends ActiveWindow {

    ContactMenu menu;
    ButtonCore showContactsBtn;
    RadialMenuItem radialMenu;

    @Override
    public void initWindow() {
        setParameters("Mainwindow", "RadialMenu Example Java", 800, 600, false);
        setMinSize(300, 300);
        setBackground(StyleFactory.commonBackground);
        setPadding(0, 0, 0, 0);
        setAntiAliasingQuality(MSAA.MSAA_8X);
        isCentered = true;

        // title
        TitleBar title = new TitleBar(getWindowTitle());

        // radial menu
        radialMenu = new RadialMenuItem(this);

        // contact menu
        menu = new ContactMenu(this, radialMenu);
        
        // show contacts button
        showContactsBtn = new ButtonCore("Show");
        showContactsBtn.setStyle(StyleFactory.getRoundedButtonStyle());

        // adding
        addItems(title, showContactsBtn);
    }
}