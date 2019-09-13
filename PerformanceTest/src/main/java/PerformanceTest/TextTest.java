package PerformanceTest;

import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.TextArea;
import com.spvessel.spacevil.*;

public class TextTest extends ActiveWindow {
    Label _infoOutput;

    @Override
    public void initWindow() {
        setParameters("TextTest", "TextTest JVM : SpaceVIL v0.3.4.1", 800, 800, true);
        setBackground(24, 24, 24);
        setPadding(0, 30, 0, 0);

        _infoOutput = ItemsFactory.getStatusLine("0 lines : 0 characters");
        addItem(_infoOutput);

        TextArea textArea = ItemsFactory.getTextArea();

        textArea.onTextChanged.add(() -> {
            _infoOutput.setText(
                countLines(textArea.getText()) + " lines : " + textArea.getText().length() + " characters");
        });
        addItem(textArea);
    }

    private static int countLines(String str) {
        String[] lines = str.split("\r\n|\r|\n");
        return lines.length;
    }
}