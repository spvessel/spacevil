using SpaceVIL;

using AdvancedGamePad.Wrappers;
using AdvancedGamePad.Model;
using AdvancedGamePad.Core;
using System.Drawing;
using System;
using SpaceVIL.Common;

namespace AdvancedGamePad.View
{
    public class BindsSettingsContainer : HorizontalStack, IUpdateUI
    {
        private ContextMenu _menu = null;
        private MenuItem _copy = null;
        private MenuItem _paste = null;
        private MenuItem _clear = null;
        private MenuItem _manualMode = null;

        private VerticalStack _leftBinds = null;
        private VerticalStack _rightBinds = null;

        public BindsSettingsContainer()
        {
            SetSpacing(60, 0);
            SetPadding(10, 40, 10, 0);
            SetMargin(10, 10, 10, 10);

            _leftBinds = new VerticalStack();
            _rightBinds = new VerticalStack();

            // context menu
            _copy = new MenuItem(Controller.GetLanguage()["CopyMenuItem"]);
            _paste = new MenuItem(Controller.GetLanguage()["PasteMenuItem"]);
            _clear = new MenuItem(Controller.GetLanguage()["ClearMenuItem"]);
            _manualMode = new MenuItem(Controller.GetLanguage()["ManualMenuItem"]);
        }

        public override void InitElements()
        {
            _menu = new ContextMenu(GetHandler(), _copy, _paste, _clear, _manualMode);
            _menu.SetBackground(50, 50, 50);
            _menu.SetShadow(5, 0, 0, Color.FromArgb(210, 0, 0, 0));

            Factory.Styles.GetContextMenuItemStyle().SetStyle(_copy, _paste, _clear, _manualMode);
            _copy.AddItem(Factory.Items.GetImageMenuItem(Factory.Resources.CopyIcon, Color.FromArgb(12, 180, 105)));
            _paste.AddItem(Factory.Items.GetImageMenuItem(Factory.Resources.PasteIcon, Color.FromArgb(173, 139, 186)));
            _clear.AddItem(Factory.Items.GetImageMenuItem(Factory.Resources.ClearIcon, Color.FromArgb(200, 116, 116)));
            _manualMode.AddItem(Factory.Items.GetImageMenuItem(Factory.Resources.PlusIcon, Color.FromArgb(10, 168, 232)));

            _copy.EventMouseClick += (sender, args) =>
            {
                if (_menu.ReturnFocus != null)
                {
                    TextEdit field = _menu.ReturnFocus as TextEdit;
                    if (field != null)
                    {
                        CommonService.SetClipboardString(field.GetText());
                    }
                }
            };

            _paste.EventMouseClick += (sender, args) =>
            {
                if (_menu.ReturnFocus != null)
                {
                    TextEdit field = _menu.ReturnFocus as TextEdit;
                    if (field != null)
                    {
                        field.SetText(CommonService.GetClipboardString());
                    }
                }
            };

            _clear.EventMouseClick += (sender, args) =>
            {
                if (_menu.ReturnFocus != null)
                {
                    TextEdit field = _menu.ReturnFocus as TextEdit;
                    if (field != null)
                    {
                        field.Clear();
                    }
                }
            };

            _manualMode.EventMouseClick += (sender, args) =>
            {
                if (_menu.ReturnFocus != null)
                {
                    TextEdit field = _menu.ReturnFocus as TextEdit;
                    if (field != null)
                    {
                        ManualKeyBinder mkb = new ManualKeyBinder();
                        mkb.OnCloseDialog += () =>
                        {
                            string result = mkb.Result;
                            if (!result.Equals(String.Empty))
                            {
                                field.SetText(result);
                                KeyBindItem parent = field.GetParent() as KeyBindItem;
                                parent?.InputChanged?.Invoke();
                            }
                        };
                        mkb.Show(GetHandler());
                    }
                }
            };

            _leftBinds.SetSpacing(0, 10);
            _rightBinds.SetSpacing(0, 10);

            AddItems(
                _leftBinds,
                _rightBinds
            );
            UpdateUI();
        }

        public void UpdateUI()
        {
            _leftBinds.Clear();
            _rightBinds.Clear();
            FillBinds();

            _copy.SetText(Controller.GetLanguage()["CopyMenuItem"]);
            _paste.SetText(Controller.GetLanguage()["PasteMenuItem"]);
            _clear.SetText(Controller.GetLanguage()["ClearMenuItem"]);
            _manualMode.SetText(Controller.GetLanguage()["ManualMenuItem"]);
        }

        private void FillBinds()
        {
            _leftBinds.AddItems(
                GetBindItem(GamePadButtonType.A),
                GetBindItem(GamePadButtonType.X),
                GetBindItem(GamePadButtonType.Y),
                GetBindItem(GamePadButtonType.B),
                GetBindItem(GamePadButtonType.Up),
                GetBindItem(GamePadButtonType.Down),
                GetBindItem(GamePadButtonType.Left),
                GetBindItem(GamePadButtonType.Right)
            );

            _rightBinds.AddItems(
                GetBindItem(GamePadButtonType.Start),
                GetBindItem(GamePadButtonType.Back),
                GetBindItem(GamePadButtonType.LB),
                GetBindItem(GamePadButtonType.RB),
                GetBindItem(GamePadButtonType.LT),
                GetBindItem(GamePadButtonType.RT),
                GetBindItem(GamePadButtonType.LS),
                GetBindItem(GamePadButtonType.RS)
            );
        }

        private KeyBindItem GetBindItem(GamePadButtonType type)
        {
            string line = Parse.KeysToString(Controller.GetProfile(), XInputWrapper.GamePadButtons[type]);
            KeyBindItem item = new KeyBindItem(Controller.GetLanguage()[type.ToString()], line);

            item.InputChanged = () =>
            {
                AssignKeystroke(Controller.GetProfile(), type, item.GetText());
            };

            item.SetContextMenu(_menu);

            return item;
        }

        private void AssignKeystroke(Profile profile, GamePadButtonType button, string keystroke)
        {
            switch (button)
            {
                case GamePadButtonType.A:
                    profile.ButtonA = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.X:
                    profile.ButtonX = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Y:
                    profile.ButtonY = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.B:
                    profile.ButtonB = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Up:
                    profile.ButtonDUp = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Down:
                    profile.ButtonDDown = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Left:
                    profile.ButtonDLeft = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Right:
                    profile.ButtonDRight = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Start:
                    profile.ButtonBack = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.Back:
                    profile.ButtonStart = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.LB:
                    profile.ButtonLB = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.LS:
                    profile.ButtonLS = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.RB:
                    profile.ButtonRB = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.RS:
                    profile.ButtonRS = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.RT:
                    profile.ButtonRT = Parse.KeyCodes(keystroke);
                    break;
                case GamePadButtonType.LT:
                    profile.ButtonLT = Parse.KeyCodes(keystroke);
                    break;
                default:
                    break;
            }
        }
    }
}