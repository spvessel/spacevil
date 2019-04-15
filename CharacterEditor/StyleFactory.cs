using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Decorations;

namespace CharacterEditor
{
    internal static class StyleFactory
    {
        internal static Style GetTextAreaStyle()
        {
            Style style = Style.GetTextAreaStyle();
            style.Background = Color.Transparent;

            Style textedit = style.GetInnerStyle("textedit");
            textedit.Foreground = Color.LightGray;

            Style cursor = textedit.GetInnerStyle("cursor");
            cursor.Background = Color.FromArgb(0, 162, 232);

            return style;
        }
    }
}
