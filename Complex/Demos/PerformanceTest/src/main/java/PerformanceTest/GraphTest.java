package PerformanceTest;

import com.spvessel.spacevil.ActiveWindow;
import com.spvessel.spacevil.ButtonCore;
import com.spvessel.spacevil.FreeArea;
import com.spvessel.spacevil.Graph;
import com.spvessel.spacevil.HorizontalStack;
import com.spvessel.spacevil.Label;
import com.spvessel.spacevil.ResizableItem;
import com.spvessel.spacevil.VerticalStack;

public class GraphTest extends ActiveWindow {
    Label _statusLineOutput;
    int pointCount = 0;

    @Override
    public void initWindow() {
        setParameters("GraphTest", "GraphTest JVM : SpaceVIL v0.3.4.1", 870, 1000, true);

        _statusLineOutput = ItemsFactory.getStatusLine("0 graphs : 0 points");
        addItem(_statusLineOutput);

        VerticalStack layout = new VerticalStack();
        layout.setMargin(0, 30, 0, 25);
        addItem(layout);

        HorizontalStack toolbar = ItemsFactory.getToolbar();
        layout.addItem(toolbar);

        FreeArea flowArea = ItemsFactory.getFreeArea();
        layout.addItem(flowArea);

        ButtonCore addOneGraph = ItemsFactory.getControlButton("One");
        ButtonCore addManyGraphs = ItemsFactory.getControlButton("Many");
        ButtonCore clearGraphs = ItemsFactory.getControlButton("Clear");
        toolbar.addItems(addOneGraph, addManyGraphs, clearGraphs);

        addOneGraph.eventMouseClick.add((sender, args) -> {
            ResizableItem shell = ItemsFactory.getGraphShell();
            flowArea.addItem(shell);
            Graph graph = ItemsFactory.getGraph();
            shell.addItem(graph);
            pointCount += graph.getPointsCoord().size();
            _statusLineOutput.setText(flowArea.getItems().size() + " graphs : " + pointCount + " points");
        });
        
        addManyGraphs.eventMouseClick.add((sender, args) -> {
            for (int i = 0; i < 8; i++) {
                for (int j = 0; j < 8; j++) {
                    ResizableItem shell = ItemsFactory.getGraphShell();
                    shell.setPosition(10 + i * 103, 10 + j * 103);
                    flowArea.addItem(shell);
                    Graph graph = ItemsFactory.getGraph();
                    shell.addItem(graph);
                    pointCount += graph.getPointsCoord().size();
                    _statusLineOutput.setText(flowArea.getItems().size() + " graphs : " + pointCount + " points");
                }
            }
        });
        
        clearGraphs.eventMouseClick.add((sender, args) -> {
            flowArea.clear();
            pointCount = 0;
            _statusLineOutput.setText(flowArea.getItems().size() + " graphs : " + pointCount + " points");
        });
    }
}