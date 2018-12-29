using System;
using System.Collections.Generic;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Common;

namespace EnigmaGame.View
{
    internal enum CardType
    {
        Top,
        Bottom
    }
    internal static class Common
    {
        //internal static readonly Font MoireFont = new Font(new FontFamily("Moire"), 22, FontStyle.Regular);
        internal static readonly Font MoireFont = DefaultsService.GetDefaultFont(22);
        internal static readonly Color Background = Color.FromArgb(255, 20, 20, 20);
        internal static readonly Color Extinguished = Color.FromArgb(255, 47, 26, 20);
        internal static readonly Color Neitral = Color.FromArgb(255, 68, 34, 24);
        internal static readonly Color Text = Color.FromArgb(255, 166, 66, 42);
        internal static readonly Color Significant = Color.FromArgb(220, 145, 35, 20);
        internal static readonly Color Selected = Color.FromArgb(255, 254, 64, 28);

        //top cards
        internal static readonly int[] Card_line_top = new int[]
        {
            1,0,0,
            0,1,0,
            0,0,1,
        };
        internal static readonly int[] Card_6_top = new int[]
        {
            1,1,0,
            1,1,0,
            1,1,0,
        };
        internal static readonly int[] Card_4_poligon_top = new int[]
        {
            0,1,0,
            1,0,1,
            0,1,0,
        };
        internal static readonly int[] Card_4_square_top = new int[]
        {
            0,0,0,
            0,1,1,
            0,1,1,
        };
        internal static readonly int[] Card_Y_top = new int[]
        {
            0,1,0,
            0,1,0,
            1,0,1,
        };
        internal static readonly int[] Card_Z_top = new int[]
        {
            1,1,0,
            0,1,0,
            0,1,1,
        };
        internal static readonly int[] Card_halfmoon_top = new int[]
        {
            0,0,1,
            0,0,1,
            1,1,0,
        };
        internal static readonly int[] Card_tetris_top = new int[]
        {
            0,0,0,
            0,1,0,
            1,1,1,
        };
        internal static readonly int[] Card_triangle_top = new int[]
        {
            1,1,1,
            0,1,1,
            0,0,1,
        };

        //bottom cards
        internal static readonly int[] Card_line_bottom = new int[]
        {
            0,0,1,
            0,1,0,
            1,0,0,
        };

        internal static readonly int[] Card_6_bottom = new int[]
        {
            1,1,0,
            1,1,0,
            1,1,0,
        };
        internal static readonly int[] Card_4_poligon_bottom = new int[]
        {
            0,1,0,
            1,0,1,
            0,1,0,
        };
        internal static readonly int[] Card_4_square_bottom = new int[]
        {
            0,1,1,
            0,1,1,
            0,0,0,
        };
        internal static readonly int[] Card_Y_bottom = new int[]
        {
            1,0,1,
            0,1,0,
            0,1,0,
        };
        internal static readonly int[] Card_Z_bottom = new int[]
        {
            0,1,1,
            0,1,0,
            1,1,0,
        };
        internal static readonly int[] Card_halfmoon_bottom = new int[]
        {
            1,1,0,
            0,0,1,
            0,0,1,
        };
        internal static readonly int[] Card_tetris_bottom = new int[]
        {
            1,1,1,
            0,1,0,
            0,0,0,
        };
        internal static readonly int[] Card_triangle_bottom = new int[]
        {
            0,0,1,
            0,1,1,
            1,1,1,
        };

        internal static List<float[]> GetArrow(float w, float h, float thickness, int alpha)
        {
            List<float[]> figure = new List<float[]>();

            float x0 = (w - thickness) / 2;
            float y0 = (h - thickness) / 2;

            //center
            figure.Add(new float[] { x0, y0, 0.0f });
            figure.Add(new float[] { x0, y0 + thickness, 0.0f });
            figure.Add(new float[] { x0 + thickness, y0 + thickness, 0.0f });

            figure.Add(new float[] { x0 + thickness, y0 + thickness, 0.0f });
            figure.Add(new float[] { x0 + thickness, y0, 0.0f });
            figure.Add(new float[] { x0, y0, 0.0f });

            //top
            figure.Add(new float[] { x0, 0, 0.0f });
            figure.Add(new float[] { x0, y0, 0.0f });
            figure.Add(new float[] { x0 + thickness, y0, 0.0f });

            figure.Add(new float[] { x0 + thickness, y0, 0.0f });
            figure.Add(new float[] { x0 + thickness, 0, 0.0f });
            figure.Add(new float[] { x0, 0, 0.0f });

            //left
            figure.Add(new float[] { 0, y0, 0.0f });
            figure.Add(new float[] { 0, y0 + thickness, 0.0f });
            figure.Add(new float[] { x0, y0 + thickness, 0.0f });

            figure.Add(new float[] { x0, y0 + thickness, 0.0f });
            figure.Add(new float[] { x0, y0, 0.0f });
            figure.Add(new float[] { 0, y0, 0.0f });

            //rotate
            x0 = w / 2;
            y0 = h / 2;
            foreach (var crd in figure)
            {
                float x_crd = crd[0];
                float y_crd = crd[1];
                crd[0] = x0 + (x_crd - x0) * (float)Math.Cos(alpha * Math.PI / 180.0f) - (y_crd - y0) * (float)Math.Sin(alpha * Math.PI / 180.0f);
                crd[1] = y0 + (y_crd - y0) * (float)Math.Cos(alpha * Math.PI / 180.0f) + (x_crd - x0) * (float)Math.Sin(alpha * Math.PI / 180.0f);
            }
            return figure;
        }

        internal static Label GetNumber(int number)
        {
            Label card_number = new Label();
            card_number.SetFont(Common.MoireFont);
            card_number.SetFontSize(22);
            card_number.SetForeground(Common.Neitral);
            card_number.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);
            card_number.SetHeight(30);
            card_number.SetHeightPolicy(SizePolicy.Fixed);

            return card_number;
        }

        internal static Ellipse GetPoint()
        {
            Ellipse point = new Ellipse(20);
            point.SetBackground(Common.Extinguished);
            point.SetMinSize(17, 17);
            point.SetMaxSize(100, 100);
            point.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            point.SetAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);
            point.SetMargin(5, 5, 5, 5);

            return point;
        }

        internal static CustomShape GetArrow(int angle)
        {
            CustomShape arrow = new CustomShape(Common.GetArrow(20, 20, 1, angle));
            arrow.SetBackground(Common.Neitral);
            arrow.SetMinSize(8, 15);
            arrow.SetMaxSize(8, 15);
            arrow.SetSizePolicy(SizePolicy.Expand, SizePolicy.Expand);
            arrow.SetAlignment(ItemAlignment.VCenter | ItemAlignment.HCenter);
            return arrow;
        }

        internal static bool CompareMatrices(int[] first, int[] second)
        {
            if (first.Length != second.Length)
                return false;
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                    return false;
            }
            return true;
        }
    }
}