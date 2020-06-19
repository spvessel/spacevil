using System.Drawing;

namespace AdvancedGamePad.Factory
{
    public static class Palette
    {
        public static readonly Color Background = Color.FromArgb(62, 62, 62);
        public static readonly Color LayoutBackground = Color.FromArgb(52, 52, 52);
        public static readonly Color ExclusiveDark = Color.FromArgb(32, 32, 32);
        public static readonly Color CommonDark = Color.FromArgb(40, 40, 40);
        public static readonly Color Dark = Color.FromArgb(48, 48, 48);
        public static readonly Color ButtonBase = Color.FromArgb(0xFF, 0xB5, 0x6F);
        public static readonly Color ButtonSettings = Color.FromArgb(200, 200, 200);
        public static readonly Color Underline = Color.White;
        public static readonly Color Divider = Color.FromArgb(40, 255, 255, 255);
        public static readonly Color ProfileSelection = Color.FromArgb(0xff, 0xc6, 0x8e);
        public static readonly Color Shadow = Color.Black;
        public static readonly Color Hover = Color.FromArgb(60, 255, 255, 255);
        public static readonly Color Gray = Color.FromArgb(180, 180, 180);
        public static readonly Color LightGray = Color.FromArgb(210, 210, 210);
        public static readonly Color KeyBindFocused = Color.FromArgb(138, 220, 255);
    }
}