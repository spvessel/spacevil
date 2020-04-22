using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace OpenGLLayerExample
{
   public class MainWindow : ActiveWindow
   {
      public override void InitWindow()
      {
         SetParameters(this.GetType().Name, this.GetType().Name, 800, 800, false);
         IsCentered = true;

         OneCubeExample();
         // MultipleCubes();
      }

      private void OneCubeExample()
      {
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

      private void MultipleCubes()
      {
         TitleBar titleBar = new TitleBar(this.GetType().Name);

         FreeArea area = new FreeArea();
         area.SetMargin(0, titleBar.GetHeight(), 0, 0);

         AddItems(titleBar, area);

         List<IBaseItem> content = new List<IBaseItem>();

         for (int row = 0; row < 3; row++)
         {
            for (int column = 0; column < 3; column++)
            {
               ResizableItem frame = new ResizableItem();
               frame.SetBorder(new Border(Color.Gray, new CornerRadius(), 2));
               frame.SetPadding(5, 5, 5, 5);
               frame.SetBackground(100, 100, 100);
               frame.SetSize(200, 200);
               frame.SetPosition(90 + row * 210, 60 + column * 210);
               area.AddItem(frame);
               content.Add(frame);

               frame.EventMousePress += (sender, args) =>
               {
                  content.Remove(frame);
                  content.Add(frame);
                  area.SetContent(content);
               };

               OpenGLLayer ogl = new OpenGLLayer();
               ogl.SetMargin(0, 30, 0, 0);
               frame.AddItem(ogl);
            }
         }
      }
   }
}