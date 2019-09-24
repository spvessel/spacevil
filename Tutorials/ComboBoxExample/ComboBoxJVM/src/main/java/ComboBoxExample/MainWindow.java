package ComboBoxExample;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.ComboBox;
import com.spvessel.spacevil.InputDialog;
import com.spvessel.spacevil.MenuItem;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.Common.DefaultsService;
import com.spvessel.spacevil.Flags.EmbeddedImage;
import com.spvessel.spacevil.Flags.EmbeddedImageSize;
import com.spvessel.spacevil.Flags.ItemAlignment;

class MainWindow extends ActiveWindow {

    @Override
    public void initWindow() {
        // Window attr
        setParameters("ComboBoxExample", "ComboBoxExample", 500, 400);

        // Layout attr
        VerticalStack layout = new VerticalStack();
        layout.setBackground(60, 60, 60);
        layout.setPadding(30, 30, 30, 30);

        // Creating MenuItems
        MenuItem filterItem = ItemsFactory.getMenuItem("Open Filter Function Menu",
                DefaultsService.getDefaultImage(EmbeddedImage.FILTER, EmbeddedImageSize.SIZE_32X32));

        MenuItem recycleItem = ItemsFactory.getMenuItem("Open Recycle Bin",
                DefaultsService.getDefaultImage(EmbeddedImage.RECYCLE_BIN, EmbeddedImageSize.SIZE_32X32));

        MenuItem refreshItem = ItemsFactory.getMenuItem("Refresh UI",
                DefaultsService.getDefaultImage(EmbeddedImage.REFRESH, EmbeddedImageSize.SIZE_32X32));

        MenuItem addMenuItemItem = ItemsFactory.getMenuItem("Add New Function...",
                DefaultsService.getDefaultImage(EmbeddedImage.ADD, EmbeddedImageSize.SIZE_32X32));

        // Creating ComboBox
        ComboBox combo = new ComboBox(filterItem, recycleItem, refreshItem, addMenuItemItem);
        combo.setAlignment(ItemAlignment.VCENTER, ItemAlignment.HCENTER);
        combo.setText("Operations");
        combo.setStyle(StyleFactory.getComboBoxStyle());

        // Change event for "addMenuItemItem" - add a new MenuItem into ComboBox
	addMenuItemItem.eventMouseClick.clear();
        addMenuItemItem.eventMouseClick.add((sender, args) -> {
            InputDialog inDialog = new InputDialog("Add new function...", "Add", "NewFunction");
            inDialog.onCloseDialog.add(() -> {
                if (inDialog.getResult() != "")
                    combo.addItem(ItemsFactory.getMenuItem(inDialog.getResult(),
                            DefaultsService.getDefaultImage(EmbeddedImage.IMPORT, EmbeddedImageSize.SIZE_32X32)));
            });
            inDialog.show(addMenuItemItem.getHandler());
        });

        // Add ComboBox to Window
        addItem(layout);
        layout.addItem(combo);

        // Decorate our ComboBox with a white dot
        combo.addItem(ItemsFactory.getDot());

        //Optionally: set start index (this method should only be called if ComboBox has already been added)
        // combo.setCurrentIndex(0);
    }
}