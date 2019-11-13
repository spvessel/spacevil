package SubtractFigureEffect;

import java.awt.Color;
import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.ButtonToggle;
import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.MSAA;

public class MainWindow extends ActiveWindow {

    @Override
    public void initWindow() {
        // window attr
        setParameters("MainWindow", "SubtractEffectExample", 600, 500);
        setAntiAliasingQuality(MSAA.MSAA_8X);
        isCentered = true;

        // create items
        int diameter = 200;

        InterfaceBaseItem cRed = ItemsFactory.getCircle(diameter, new Color(255, 94, 94));
        InterfaceBaseItem cGreen = ItemsFactory.getCircle(diameter, new Color(16, 180, 111));
        InterfaceBaseItem cBlue = ItemsFactory.getCircle(diameter, new Color(10, 162, 232));

        ItemsFactory.setCircleAlignment(cRed, ItemAlignment.TOP);
        ItemsFactory.setCircleAlignment(cGreen, ItemAlignment.LEFT, ItemAlignment.BOTTOM);
        ItemsFactory.setCircleAlignment(cBlue, ItemAlignment.RIGHT, ItemAlignment.BOTTOM);

        // add items to window
        addItems(cRed, cGreen, cBlue, ItemsFactory.getLabel("Vector Subtraction Effect"));

        // init effects
        Model.initEffects(cRed, cGreen, cBlue);

        // switch effects button
        ButtonToggle switchEffectBtn = ItemsFactory.getSwitchButton();
        switchEffectBtn.eventToggle.add((sender, args) -> {
            if (switchEffectBtn.isToggled()) {
                Model.addEffects();
            } else {
                Model.removeEffects();
            }
        });
        addItem(switchEffectBtn);
    }
}