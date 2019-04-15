using System;
using SpaceVIL;
using System.Drawing;
using SpaceVIL.Core;
using SpaceVIL.Decorations;
using SpaceVIL.Common;

namespace CharacterEditor
{
    internal class CharacterCard : Prototype
    {
        private static Int32 _count = 0;
        private Label _name;

        private CharacterInfo _characterInfo = null;

        internal CharacterCard(CharacterInfo info)
        {
            SetItemName("ListItem_" + _count);

            SetSizePolicy(SizePolicy.Expand, SizePolicy.Fixed);
            SetHeight(30);
            SetBackground(60, 60, 60);
            SetPadding(10, 0, 5, 0);
            SetMargin(2, 1, 2, 1);
            AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(30, 255, 255, 255)));
            _characterInfo = info;
            _name = new Label(info.Name + " the " + info.Race);
        }

        public override void InitElements()
        {
            ImageItem _race = new ImageItem(DefaultsService.GetDefaultImage(
                EmbeddedImage.User, 
                EmbeddedImageSize.Size32x32), false);
            _race.KeepAspectRatio(true);
            _race.SetWidthPolicy(SizePolicy.Fixed);
            _race.SetWidth(20);
            _race.SetAlignment(ItemAlignment.Left, ItemAlignment.VCenter);

            switch (_characterInfo.Race)
            {
                case CharacterRace.Human:
                    _race.SetColorOverlay(Color.FromArgb(0, 162, 232));
                    break;
                case CharacterRace.Elf:
                    _race.SetColorOverlay(Color.FromArgb(35, 201, 109));
                    break;
                case CharacterRace.Dwarf:
                    _race.SetColorOverlay(Color.FromArgb(255, 127, 39));
                    break;
            }

            _name.SetMargin(30, 0, 30, 0);

            ButtonCore infoBtn = new ButtonCore("?");
            infoBtn.SetBackground(Color.FromArgb(255, 40, 40, 40));
            infoBtn.SetWidth(20);
            infoBtn.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Expand);
            infoBtn.SetFontStyle(FontStyle.Bold);
            infoBtn.SetForeground(210, 210, 210);
            infoBtn.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Right);
            infoBtn.SetMargin(0, 0, 20, 0);
            infoBtn.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(0, 140, 210)));

            infoBtn.SetPassEvents(false);

            infoBtn.EventMouseHover += (sender, args) =>
            {
                SetMouseHover(true);
            };

            infoBtn.EventMouseClick += (sender, args) =>
            {
                ImageItem race = new ImageItem(DefaultsService.GetDefaultImage(EmbeddedImage.User, EmbeddedImageSize.Size32x32), false);
                race.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
                race.SetSize(32, 32);
                race.SetAlignment(ItemAlignment.Left, ItemAlignment.Top);
                race.SetColorOverlay(_race.GetColorOverlay());

                PopUpMessage popUpInfo = new PopUpMessage(
                    _characterInfo.Name + "\n" +
                    "Age: " + _characterInfo.Age + "\n" +
                    "Sex: " + _characterInfo.Sex + "\n" +
                    "Race: " + _characterInfo.Race + "\n" +
                    "Class: " + _characterInfo.Class
                    );
                popUpInfo.SetTimeOut(3000);
                popUpInfo.SetHeight(200);
                popUpInfo.Show(GetHandler());
                popUpInfo.AddItem(race);
            };

            //close
            ButtonCore removeBtn = new ButtonCore();
            removeBtn.SetBackground(Color.FromArgb(255, 40, 40, 40));
            removeBtn.SetSizePolicy(SizePolicy.Fixed, SizePolicy.Fixed);
            removeBtn.SetSize(10, 10);
            removeBtn.SetAlignment(ItemAlignment.VCenter, ItemAlignment.Right);
            removeBtn.SetCustomFigure(new CustomFigure(false, GraphicsMathService.GetCross(10, 10, 2, 45)));
            removeBtn.AddItemState(ItemStateType.Hovered, new ItemState(Color.FromArgb(200, 95, 97)));

            //close event
            removeBtn.EventMouseClick += (sender, args) =>
            {
                RemoveSelf();
            };

            //adding
            AddItems(_race, _name, infoBtn, removeBtn);
        }

        internal void RemoveSelf()
        {
            GetParent().RemoveItem(this);
        }

        public override String ToString()
        {
            return _characterInfo.ToString();
        }
    }
}
