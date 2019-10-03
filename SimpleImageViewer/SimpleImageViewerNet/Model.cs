using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using SpaceVIL;
using SpaceVIL.Core;

namespace SimpleImageViewer
{
    public static class Model
    {
        public static void FillImageArea(CoreWindow handler, WrapGrid area, PreviewArea preview, String directory)
        {
            if (directory == null || directory == String.Empty)
                return;

            LoadingScreen screen = new LoadingScreen();
            screen.Show(handler);

            Task thread = new Task(() =>
            {
                area.Clear();

                DirectoryInfo d = new DirectoryInfo(directory);
                FileInfo[] files = d.GetFiles();

                List<Picture> list = new List<Picture>();
                int count = files.Length;
                int index = 0;

                Parallel.ForEach(files, (f) =>
                {
                    index++;
                    if (CheckExtensionFilter(f))
                    {
                        Bitmap img = new Bitmap(f.FullName);
                        Bitmap dBitmap = DownScaleBitmap(img, 170, 100);
                        Picture pic = new Picture(dBitmap, f.Name, f.FullName);

                        pic.EventMouseClick += (sender, args) =>
                        {
                            ReplacePreviewImage(area, preview, pic);
                        };
                        pic.EventKeyPress += (sender, args) =>
                        {
                            if (args.Key == KeyCode.Enter)
                                ReplacePreviewImage(area, preview, pic);
                        };
                        dBitmap.Dispose();
                        img.Dispose();
                        list.Add(pic);
                    }
                    float persent = ((float)index / (float)count) * 100.0f;
                    screen.SetValue((int)persent);
                });
                if (list.Count == 0)
                {
                    screen.SetToClose();
                    return;
                }

                list.Sort();
                foreach (var item in list)
                {
                    area.AddItem(item);
                }

                Random rnd = new Random();
                int fileIndex = rnd.Next(0, list.Count);

                using (Bitmap img = new Bitmap(list[fileIndex].Path))
                {
                    preview.SetImage(img);
                    preview.SetPictureInfo(new FileInfo(list[fileIndex].Path).Name, img.Width, img.Height);
                }

                screen.SetToClose();
            });
            thread.Start();
        }

        public static void ReplacePreviewImage(WrapGrid area, PreviewArea preview, Picture pic)
        {
            using (Bitmap bitmap = new Bitmap(pic.Path))
            {
                preview.SetImage(bitmap);
                preview.SetPictureInfo(pic.Name.GetText(), bitmap.Width, bitmap.Height);
            }
            area.GetArea().SetFocus();
        }

        public static Bitmap DownScaleBitmap(Bitmap img, int w, int h)
        {
            float boundW = w;
            float boundH = h;

            var ratioX = (boundW / img.Width);
            var ratioY = (boundH / img.Height);
            float ratio = ratioX < ratioY ? ratioX : ratioY;

            int resW = (int)(img.Width * ratio);
            int resH = (int)(img.Height * ratio);

            var bmp = new Bitmap(resW, resH);
            var graphic = Graphics.FromImage(bmp);
            graphic.DrawImage(img, new System.Drawing.Rectangle(0, 0, resW, resH));
            graphic.Dispose();

            return bmp;
        }

        public static bool CheckExtensionFilter(FileInfo f)
        {
            String name = f.Name.ToLower();
            String[] filter = new String[] { ".png", ".bmp", ".jpg", ".jpeg" };

            foreach (String item in filter)
                if (name.EndsWith(item))
                    return true;

            return false;
        }

        public static String GetPicturePath(WrapGrid area, String name)
        {
            List<IBaseItem> list = area.GetListContent();
            foreach (IBaseItem item in list)
            {
                Picture tmp = item as Picture;
                if (tmp != null)
                {
                    if (tmp.Name.GetText().Equals(name))
                        return tmp.Path;
                }
            }
            return String.Empty;
        }
    }
}
