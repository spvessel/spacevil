using System;
using SpaceVIL;
using SpaceVIL.Core;

namespace OpenGLLayerExample
{
    public class MainWindow : ActiveWindow
    {
        public override void InitWindow()
        {
            SetParameters(this.GetType().Name, this.GetType().Name, 800, 800, false);
            IsCentered = true;

            TitleBar titleBar = new TitleBar(this.GetType().Name);

            OpenGLLayer ogl = new OpenGLLayer();
            ogl.SetMargin(0, titleBar.GetHeight(), 0, 0);

            HorizontalStack toolbar = Items.GetToolbarLayout();

            ImagedButton btnRotateLeft = Items.GetImagedButton(EmbeddedImage.ArrowUp, -90);
            ImagedButton btnRotateRight = Items.GetImagedButton(EmbeddedImage.ArrowUp, 90);

            HorizontalSlider zoom = Items.GetSlider();

            ImagedButton btnRestoreView = Items.GetImagedButton(EmbeddedImage.Refresh, 0);

            // adding
            AddItems(titleBar, ogl);
            ogl.AddItems(toolbar);
            toolbar.AddItems(btnRotateLeft, btnRotateRight, zoom, btnRestoreView);

            // assign events
            btnRestoreView.EventMousePress += (sender, args) =>
            {
                ogl.RestoreView();
            };

            btnRotateLeft.EventMousePress += (sender, args) =>
            {
                ogl.Rotate(KeyCode.Left);
            };

            btnRotateRight.EventMousePress += (sender, args) =>
            {
                ogl.Rotate(KeyCode.Right);
            };

            zoom.EventValueChanged += (sender) =>
            {
                ogl.SetZoom(zoom.GetCurrentValue());
            };

            // Set focus
            ogl.SetFocus();
            zoom.SetCurrentValue(3);
        }
    }
}