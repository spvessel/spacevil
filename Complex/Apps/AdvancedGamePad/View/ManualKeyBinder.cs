using System;
using System.Collections.Generic;
using System.Drawing;
using AdvancedGamePad.Core;
using AdvancedGamePad.Model;
using SpaceVIL;
using SpaceVIL.Common;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace AdvancedGamePad.View
{
    public class ManualKeyBinder : DialogItem, IUpdateUI
    {
        public string Result { get; private set; }

        private TextEdit _bind = null;
        private Button _apply = null;
        private BlankItem _close = null;
        private ImagedButton _deleteLast = null;

        private Button _win = null;
        private Button _lMouse = null;
        private Button _rMouse = null;
        private Button _mMouse = null;

        public ManualKeyBinder()
        {
            Result = String.Empty;

            _bind = new TextEdit();
            _apply = Factory.Items.GetControlButton(Controller.GetLanguage()["ApplyButton"]);
            _close = new BlankItem();
            _deleteLast = Factory.Items.GetImagedButton(Factory.Resources.ClearIcon);
            _win = Factory.Items.GetControlButton("Win");
            _lMouse = Factory.Items.GetControlButton("MouseLeft");
            _rMouse = Factory.Items.GetControlButton("MouseRight");
            _mMouse = Factory.Items.GetControlButton("MouseMiddle");

            _keySequence = new List<string>();

            SetStyle(DefaultsService.GetDefaultStyle(typeof(ManualKeyBinder)));
        }

        public override void InitElements()
        {
            base.InitElements();
            Color gray = Color.FromArgb(150, 150, 150);
            Indents margin = new Indents();
            int width = 120;

            _win.SetWidth(width);
            _win.SetMargin(margin);
            _win.SetBackground(gray);

            _lMouse.SetWidth(width);
            _lMouse.SetMargin(margin);
            _lMouse.SetBackground(gray);

            _rMouse.SetWidth(width);
            _rMouse.SetMargin(margin);
            _rMouse.SetBackground(gray);

            _mMouse.SetWidth(width);
            _mMouse.SetMargin(margin);
            _mMouse.SetBackground(gray);

            _apply.SetAlignment(ItemAlignment.Bottom, ItemAlignment.Right);
            _apply.SetMargin(0, 0, 20, 20);

            _deleteLast.IsFocusable = false;
            _win.IsFocusable = false;
            _lMouse.IsFocusable = false;
            _rMouse.IsFocusable = false;
            _mMouse.IsFocusable = false;

            HorizontalStack textFieldlayout = Factory.Items.GetFixedHorizontalStack(30);
            textFieldlayout.SetMargin(100, 100, 100, 0);

            HorizontalStack toolbarlayout = Factory.Items.GetFixedHorizontalStack(30);
            toolbarlayout.SetMargin(40, 0, 40, 100);
            toolbarlayout.SetAlignment(ItemAlignment.Bottom, ItemAlignment.Left);
            toolbarlayout.SetSpacing(6, 0);

            Frame header = new Frame();
            header.SetHeightPolicy(SizePolicy.Fixed);
            header.SetHeight(40);
            header.SetBackground(Factory.Palette.ExclusiveDark);

            Window.SetMinSize(600, 300);
            Window.AddItems(header, _close, textFieldlayout, toolbarlayout, _apply);

            textFieldlayout.AddItems(_bind, _deleteLast);

            toolbarlayout.AddItems(_win, _lMouse, _rMouse, _mMouse);

            _deleteLast.EventMouseClick += (sender, args) =>
            {
                if (_keySequence.Count > 0)
                {
                    _keySequence.RemoveAt(_keySequence.Count - 1);
                    SetShortcutSequence();
                }
            };

            _win.EventMouseClick += (sender, args) =>
            {
                OnKeyDown(HardwareButtons.LeftSuper);
            };
            _lMouse.EventMouseClick += (sender, args) =>
            {
                OnKeyDown(HardwareButtons.MouseLeft);
            };
            _rMouse.EventMouseClick += (sender, args) =>
            {
                OnKeyDown(HardwareButtons.MouseRight);
            };
            _mMouse.EventMouseClick += (sender, args) =>
            {
                OnKeyDown(HardwareButtons.MouseMiddle);
            };

            _bind.SetEditable(false);
            _bind.EventKeyPress += (sender, args) =>
            {
                OnKeyDown((HardwareButtons)args.Key);
            };

            _close.EventMouseClick += (sender, args) =>
            {
                Close();
            };

            _apply.EventMouseClick += (sender, args) =>
            {
                Result = _bind.GetText();
                Close();
            };
        }

        public override void Show(CoreWindow handler)
        {
            base.Show(handler);
            _bind.SetFocus();
        }

        public override void Close()
        {
            OnCloseDialog?.Invoke();
            base.Close();
        }

        public override void SetStyle(Style style)
        {
            if (style == null)
                return;
            base.SetStyle(style);

            Style innerStyle = style.GetInnerStyle("closebutton");
            innerStyle?.SetStyle(_close);
        }

        public void UpdateUI()
        {
            _apply.SetText(Controller.GetLanguage()["ApplyButton"]);
        }


        private string _keyStroketext = string.Empty;
        private List<string> _keySequence;

        private void OnKeyDown(HardwareButtons button)
        {
            string key = Parse.KeysString(button);
            if (button == HardwareButtons.Unknown)
                return;

            if (!_keySequence.Contains(key))
            {
                _keySequence.Add(key);
            }
            SetShortcutSequence();
        }

        private void SetShortcutSequence()
        {
            _keyStroketext = "";

            foreach (var item in _keySequence)
            {
                if (_keySequence.IndexOf(item) < _keySequence.Count - 1)
                    _keyStroketext += item + "+";
                else
                    _keyStroketext += item;
            }
            _bind.SetText(_keyStroketext);
        }
    }
}