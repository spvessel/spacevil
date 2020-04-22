package PerformanceTest;

import java.awt.Color;
import java.awt.Font;
import java.awt.Point;
import java.util.LinkedList;
import java.util.List;

import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.FreeArea;
import com.spvessel.spacevil.Graph;
import com.spvessel.spacevil.GraphicsMathService;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.ResizableItem;
import com.spvessel.spacevil.TextArea;
import com.spvessel.spacevil.VerticalStack;
import com.spvessel.spacevil.WrapGrid;
import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Decorations.ItemState;
import com.spvessel.spacevil.Flags.ItemAlignment;
import com.spvessel.spacevil.Flags.ItemStateType;
import com.spvessel.spacevil.Flags.Orientation;
import com.spvessel.spacevil.Flags.SizePolicy;

final class ItemsFactory {
    static private List<float[]> koh = new LinkedList<>();

    private ItemsFactory() {
    }

    static public Label getStatusLine(String text) {
        Label label = new Label(text);
        label.setHeight(25);
        label.setHeightPolicy(SizePolicy.FIXED);
        label.setBackground(80, 80, 80);
        label.setForeground(210, 210, 210);
        label.setFontSize(18);
        label.setFontStyle(Font.BOLD);
        label.setAlignment(ItemAlignment.BOTTOM);
        label.setTextAlignment(ItemAlignment.HCENTER, ItemAlignment.BOTTOM);
        label.setPadding(0, 0, 0, 3);
        return label;
    }

    static public InterfaceBaseItem getSimpleButton(int index, Label statusLine) {
        ButtonCore btn = new ButtonCore();
        btn.setItemName("Button_" + index);
        btn.setBackground(Color.WHITE);
        btn.setSize(8, 8);
        btn.addItemState(ItemStateType.HOVERED, new ItemState(Color.RED));
        btn.setMargin(1, 1, 1, 1);
        btn.eventMouseClick.add((sender, args) -> {
            statusLine.setText(btn.getItemName() + " has been pressed");
        });
        return btn;
    }

    static public ButtonCore getControlButton(String name) {
        ButtonCore btn = new ButtonCore(name);
        btn.setSize(50, 30);
        return btn;
    }

    static public WrapGrid getWrapGrid() {
        WrapGrid wrapGrid = new WrapGrid(10, 10, Orientation.HORIZONTAL);
        wrapGrid.getArea().setSpacing(0, 0);
        wrapGrid.setBackground(new Color(0, 0, 0, 0));
        wrapGrid.setMargin(0, 0, 0, 25);
        return wrapGrid;
    }

    static public VerticalStack getVerticalStack() {
        VerticalStack v = new VerticalStack();
        v.setSpacing(0, 0);
        return v;
    }

    static public HorizontalStack getHorizontalStack() {
        HorizontalStack h = new HorizontalStack();
        h.setHeightPolicy(SizePolicy.FIXED);
        h.setHeight(22);
        h.setSpacing(6, 0);
        h.setContentAlignment(ItemAlignment.HCENTER);
        return h;
    }

    static public HorizontalStack getToolbar() {
        HorizontalStack toolbar = new HorizontalStack();
        toolbar.setBackground(51, 51, 51);
        toolbar.setHeight(40);
        toolbar.setPadding(10, 0, 10, 0);
        toolbar.setHeightPolicy(SizePolicy.FIXED);
        toolbar.setSpacing(5, 0);
        return toolbar;
    }

    static public FreeArea getFreeArea() {
        FreeArea area = new FreeArea();
        area.setBackground(70, 70, 70);
        return area;
    }

    static public TextArea getTextArea() {
        TextArea area = new TextArea();
        area.setMargin(0, 0, 0, 25);
        return area;
    }

    static public Graph getGraph() {
        if (koh.isEmpty())
            initKoh();

        Graph graph = new Graph();
        graph.setBackground(32, 32, 32);
        graph.setLineColor(new Color(100, 100, 100, 255));
        graph.setPointColor(new Color(10, 162, 232));
        graph.setPointThickness(20.0f);
        graph.setAlignment(ItemAlignment.HCENTER, ItemAlignment.VCENTER);
        graph.setSizePolicy(SizePolicy.EXPAND, SizePolicy.EXPAND);
        graph.setPadding(5, 5, 5, 5);
        graph.setPointsCoord(koh);
        graph.setPointShape(
                GraphicsMathService.getCross(graph.getPointThickness(), graph.getPointThickness(), 1, 45));

        return graph;
    }

    static public ResizableItem getGraphShell() {
        ResizableItem shell = new ResizableItem();
        shell.setPadding(5, 5, 5, 5);
        shell.setBackground(100, 100, 100);
        shell.setSize(100, 100);
        shell.setPosition(10, 10);
        return shell;
    }

    static private void initKoh() {
        Point point1 = new Point(200, 200);
        Point point2 = new Point(500, 200);
        Point point3 = new Point(350, 400);
        fractalKoh(koh, point1, point2, point3, 4);
        fractalKoh(koh, point2, point3, point1, 4);
        fractalKoh(koh, point3, point1, point2, 4);

        point1 = new Point(100, 100);
        point2 = new Point(250, 100);
        point3 = new Point(175, 200);
        List<float[]> koh2 = new LinkedList<>();
        fractalKoh(koh2, point1, point2, point3, 4);
        fractalKoh(koh2, point2, point3, point1, 4);
        fractalKoh(koh2, point3, point1, point2, 4);
        koh2 = GraphicsMathService.moveShape(koh2, 175, 135);

        point1 = new Point(50, 50);
        point2 = new Point(125, 50);
        point3 = new Point(87, 100);
        List<float[]> koh3 = new LinkedList<>();
        fractalKoh(koh3, point1, point2, point3, 3);
        fractalKoh(koh3, point2, point3, point1, 3);
        fractalKoh(koh3, point3, point1, point2, 3);
        koh3 = GraphicsMathService.moveShape(koh3, 262, 200);

        koh.addAll(koh2);
        koh.addAll(koh3);
    }

    static private void fractalKoh(List<float[]> pointsList, Point p1, Point p2, Point p3, int iterations) {

        if (iterations > 0) {
            Point p4 = new Point((p2.x + 2 * p1.x) / 3, (p2.y + 2 * p1.y) / 3);
            Point p5 = new Point((2 * p2.x + p1.x) / 3, (p1.y + 2 * p2.y) / 3);
            Point ps = new Point((p2.x + p1.x) / 2, (p2.y + p1.y) / 2);
            Point pn = new Point((4 * ps.x - p3.x) / 3, (4 * ps.y - p3.y) / 3);

            pointsList.add(new float[] { p4.x, p4.y });
            pointsList.add(new float[] { pn.x, pn.y });
            pointsList.add(new float[] { p5.x, p5.y });
            pointsList.add(new float[] { pn.x, pn.y });
            pointsList.add(new float[] { p4.x, p4.y });
            pointsList.add(new float[] { p5.x, p5.y });

            fractalKoh(pointsList, p4, pn, p5, iterations - 1);
            fractalKoh(pointsList, pn, p5, p4, iterations - 1);
            fractalKoh(pointsList, p1, p4, new Point((2 * p1.x + p3.x) / 3, (2 * p1.y + p3.y) / 3), iterations - 1);
            fractalKoh(pointsList, p5, p2, new Point((2 * p2.x + p3.x) / 3, (2 * p2.y + p3.y) / 3), iterations - 1);
        }
    }
}