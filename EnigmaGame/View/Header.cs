using System;
using System.Drawing;
using SpaceVIL;
using SpaceVIL.Core;

namespace EnigmaGame.View
{
    internal class Header : Prototype
    {
        int level = 0;
        Label enigma_codes = new Label();
        public Header()
        {
            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetHeight(30);
            SetBackground(Color.Transparent);
            IsFocusable = false;
        }

        public override void InitElements()
        {
            //decryption
            Label decryption = new Label("DECRYPTION");
            decryption.SetFont(Common.MoireFont);
            decryption.SetFontSize(22);
            decryption.SetForeground(Common.Text);
            decryption.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.Left);
            decryption.SetMargin(20, 0, 0, 0);

            //enigma codes
            enigma_codes.SetText("ENIGMA CODES LEVEL   |   0");
            enigma_codes.SetFont(Common.MoireFont);
            enigma_codes.SetFontSize(22);
            enigma_codes.SetForeground(166, 66, 42);
            enigma_codes.SetTextAlignment(ItemAlignment.VCenter | ItemAlignment.Right);
            enigma_codes.SetMargin(0, 0, 10, 0);

            AddItems(
                decryption,
                enigma_codes
            );
        }

        internal int GetLevel()
        {
            return level;
        }
        internal void LevelUp()
        {
            level++;
            enigma_codes.SetText("ENIGMA CODES LEVEL   |   " + level);
        }
        internal void LevelDown()
        {
            level--;
            enigma_codes.SetText("ENIGMA CODES LEVEL   |   " + level);
        }
        internal void ResetLevel()
        {
            level = 0;
            enigma_codes.SetText("ENIGMA CODES LEVEL   |   " + level);
        }
    }
}