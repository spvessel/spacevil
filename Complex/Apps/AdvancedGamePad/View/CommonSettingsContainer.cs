using System;
using System.Linq;
using System.Collections.Generic;

using SpaceVIL;
using SpaceVIL.Core;

using AdvancedGamePad.Factory;
using AdvancedGamePad.Core;
using AdvancedGamePad.Model;

namespace AdvancedGamePad.View
{
    public class CommonSettingsContainer : VerticalStack, IUpdateUI
    {
        private ImagedButton _copyProfile = null;
        private ImagedButton _addProfile = null;
        private ImagedButton _removeProfile = null;

        private ImagedCheckBox _autoLaunchAppCheckBox = null;
        private ImagedCheckBox _startMinimizedCheckBox = null;
        private ImagedCheckBox _autoStartCheckBox = null;

        private NamedComboBox _profileList = null;
        private NamedComboBox _inputModeList = null;
        private NamedComboBox _languageList = null;

        private Label _headerTitle = null;
        private Label _headerProfile = null;
        private Label _headerLocale = null;
        private MenuItem _miMode0 = null;
        private MenuItem _miMode1 = null;
        private MenuItem _miMode2 = null;

        public CommonSettingsContainer()
        {
            SetSpacing(0, 10);
            SetPadding(10, 10, 10, 0);
            SetMargin(10, 10, 10, 10);

            _addProfile = Items.GetImagedButton(Resources.PlusIcon);
            _copyProfile = Items.GetImagedButton(Resources.CopyIcon);
            _removeProfile = Items.GetImagedButton(Resources.ClearIcon);

            _autoLaunchAppCheckBox = Items.GetSwitcher(Controller.GetLanguage()["SetsAutoLaunch"],
                    Controller.GetSettings().AutoLaunchOnSystemStartUp);
            _startMinimizedCheckBox = Items.GetSwitcher(Controller.GetLanguage()["SetsStartMinimized"],
                    Controller.GetSettings().StartAppMinimized);
            _autoStartCheckBox = Items.GetSwitcher(Controller.GetLanguage()["SetsAutoStart"],
                    Controller.GetSettings().AutoStart);

            _languageList = new NamedComboBox(Controller.GetLanguage()["SetsLanguage"]);
            foreach (var loc in Controller.GetLocalizations())
            {
                _languageList.AddItem(Items.GetListItem(loc.Key));
            }

            _profileList = new NamedComboBox(Controller.GetLanguage()["SetsProfile"]);
            foreach (var profile in Controller.GetAllProfiles())
            {
                _profileList.AddItem(Items.GetListItem(profile.Name));
            }

            _miMode0 = Items.GetListItem(Controller.GetLanguage()["SetsIMode0"]);
            _miMode1 = Items.GetListItem(Controller.GetLanguage()["SetsIMode1"]);
            _miMode2 = Items.GetListItem(Controller.GetLanguage()["SetsIMode2"]);

            _inputModeList = new NamedComboBox(Controller.GetLanguage()["SetsInput"]);
            _inputModeList.AddItem(_miMode0);
            _inputModeList.AddItem(_miMode1);
            _inputModeList.AddItem(_miMode2);

            _headerTitle = Items.GetHeaderLabel(Controller.GetLanguage()["SetsSectionsCommon"]);
            _headerProfile = Items.GetHeaderLabel(Controller.GetLanguage()["SetsSectionsProfile"]);
            _headerLocale = Items.GetHeaderLabel(Controller.GetLanguage()["SetsSectionsLocalization"]);
        }

        public override void InitElements()
        {
            AssingSize(_languageList, _profileList, _inputModeList);

            HorizontalStack _profileToolbar = new HorizontalStack();
            _profileToolbar.SetHeightPolicy(SizePolicy.Fixed);
            _profileToolbar.SetHeight(30);
            _profileToolbar.SetSpacing(10);

            AddItems(
                _headerTitle,
                _autoLaunchAppCheckBox,
                _startMinimizedCheckBox,
                _autoStartCheckBox,
                Items.GetHorizontalDivider(),
                _headerProfile,
                _profileToolbar,
                _inputModeList,
                Items.GetHorizontalDivider(),
                _headerLocale,
                _languageList
            );

            _profileToolbar.AddItems(_profileList, _addProfile, _copyProfile, _removeProfile);

            // apply settings
            _languageList.SetCurrentIndex(
                    Controller.GetLocalizations().Keys.ToList().IndexOf(Controller.GetLanguage()["Locale"]));

            _profileList.SetCurrentIndex(
                    Controller.GetAllProfiles().Select(p => p.Name).ToList().IndexOf(Controller.GetProfile().Name));

            _inputModeList.SetCurrentIndex(
                    Parse.RepeatTypes(Controller.GetProfile().RepeatMode));

            // events
            _languageList.SelectionChanged += () =>
            {
                Controller.InvokeEventLanguageChanged(_languageList.GetTextSelection());
            };

            _profileList.SelectionChanged += () =>
            {
                Controller.InvokeEventProfileChanged(_profileList.GetTextSelection());
            };

            _inputModeList.SelectionChanged += () =>
            {
                Controller.GetProfile().RepeatMode = (RepeatType)_inputModeList.GetCurrentIndex();
            };

            _autoLaunchAppCheckBox.EventToggle += (value) =>
            {
                Controller.GetSettings().AutoLaunchOnSystemStartUp = value;
            };

            _startMinimizedCheckBox.EventToggle += (value) =>
            {
                Controller.GetSettings().StartAppMinimized = value;
            };

            _autoStartCheckBox.EventToggle += (value) =>
            {
                Controller.GetSettings().AutoStart = value;
            };

            _addProfile.EventMouseClick += (sender, args) =>
            {
                InputDialog input = new InputDialog(Controller.GetLanguage()["InputTitle"], Controller.GetLanguage()["AddButton"]);
                input.SetCancelVisible(false);
                input.OnCloseDialog += () =>
                {
                    if (input.GetResult().Length > 0)
                        Controller.InvokeEventAddProfile(input.GetResult());
                };
                input.Show(GetHandler());
            };

            _copyProfile.EventMouseClick += (sender, args) =>
            {
                InputDialog input = new InputDialog(Controller.GetLanguage()["InputTitle"], Controller.GetLanguage()["AddButton"]);
                input.SetCancelVisible(false);
                input.OnCloseDialog += () =>
                {
                    if (input.GetResult().Length > 0)
                        Controller.InvokeEventAddProfile(input.GetResult(), true);
                };
                input.Show(GetHandler());
            };

            _removeProfile.EventMouseClick += (sender, args) =>
            {
                MessageItem msg = new MessageItem(
                        Controller.GetLanguage()["MsgDeleteProfile"] 
                        + " " + Controller.GetProfile().Name
                        + "\n" + Controller.GetLanguage()["MsgConfirm"],
                        Controller.GetLanguage()["DeleteProfileTitle"]);
                msg.GetOkButton().SetText(Controller.GetLanguage()["DeleteButton"]);
                msg.GetCancelButton().SetText(Controller.GetLanguage()["CancelButton"]);
                msg.OnCloseDialog += () =>
                {
                    if (msg.GetResult())
                        Controller.InvokeEventDeleteProfile();
                };
                msg.Show(GetHandler());
            };
        }

        private int IndexOfListContent(List<String> list, string name)
        {
            return list.IndexOf(name);
        }

        public void UpdateUI()
        {
            List<Profile> currentProfiles = Controller.GetAllProfiles();
            if (_profileList.ComboBoxItem.GetListContent().Count != currentProfiles.Count)
            {
                var profiles = _profileList.ComboBoxItem.GetListContent();
                foreach (var p in profiles)
                {
                    _profileList.ComboBoxItem.RemoveItem(p);
                }
                foreach (var profile in currentProfiles)
                {
                    _profileList.ComboBoxItem.AddItem(Items.GetListItem(profile.Name));
                }
            }

            _profileList.SetCurrentIndex(
                    Controller.GetAllProfiles().Select(p => p.Name).ToList().IndexOf(Controller.GetProfile().Name));

            _autoLaunchAppCheckBox.SetText(Controller.GetLanguage()["SetsAutoLaunch"]);
            _startMinimizedCheckBox.SetText(Controller.GetLanguage()["SetsStartMinimized"]);
            _autoStartCheckBox.SetText(Controller.GetLanguage()["SetsAutoStart"]);

            _languageList.SetText(Controller.GetLanguage()["SetsLanguage"]);
            _profileList.SetText(Controller.GetLanguage()["SetsProfile"]);

            _miMode0.SetText(Controller.GetLanguage()["SetsIMode0"]);
            _miMode1.SetText(Controller.GetLanguage()["SetsIMode1"]);
            _miMode2.SetText(Controller.GetLanguage()["SetsIMode2"]);
            _inputModeList.SetText(Controller.GetLanguage()["SetsInput"]);
            _inputModeList.SetCurrentIndex(_inputModeList.GetCurrentIndex());

            _headerTitle.SetText(Controller.GetLanguage()["SetsSectionsCommon"]);
            _headerProfile.SetText(Controller.GetLanguage()["SetsSectionsProfile"]);
            _headerLocale.SetText(Controller.GetLanguage()["SetsSectionsLocalization"]);
        }

        private void AssingSize(params NamedComboBox[] list)
        {
            foreach (var item in list)
            {
                item.SetMaxWidth(400);
                item.Name.SetMaxWidth(100);
            }
        }
    }
}