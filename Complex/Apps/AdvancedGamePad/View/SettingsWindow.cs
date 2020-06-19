using SpaceVIL;
using SpaceVIL.Core;

using AdvancedGamePad.Factory;
using AdvancedGamePad.Core;

namespace AdvancedGamePad.View
{
    public class SettingsWindow : ActiveWindow, IUpdateUI
    {
        private TitleBar _titleBar = null;
        private TabView _tabArea = null;
        private SettingsTab _common = null;
        private CommonSettingsContainer _commonContent = null;
        private SettingsTab _binds = null;
        private IUpdateUI _bindsContent = null;
        private SettingsTab _sticks = null;
        private IUpdateUI _sticksContent = null;
        public Button SaveBtn
        {
            get;
            private set;
        }

        public ButtonCore CloseBtn
        {
            get;
            private set;
        }

        public override void InitWindow()
        {
            // Window's attr
            SetParameters(Controller.GetLanguage()["SetsTitle"], Controller.GetLanguage()["SetsTitle"], 900, 600, false);
            SetMinSize(900, 600);
            SetBackground(Factory.Palette.CommonDark);
            IsCentered = true;
            SetIcon(Factory.Resources.Icon, Factory.Resources.Icon);

            // Content
            _titleBar = Factory.Items.GetTitleBar(Controller.GetLanguage()["SetsTitle"]);
            CloseBtn = _titleBar.GetCloseButton();

            VerticalStack layout = Factory.Items.GetLayout();
            layout.SetMargin(0, _titleBar.GetHeight(), 0, 0);
            layout.SetPadding(0, 0, 0, 15);

            _tabArea = new TabView();
            _tabArea.SetTabPolicy(SizePolicy.Expand);

            _common = new SettingsTab(Controller.GetLanguage()["SetsTabCommon"]);
            _commonContent = new CommonSettingsContainer();

            _binds = new SettingsTab(Controller.GetLanguage()["SetsTabBinds"]);
            _bindsContent = new BindsSettingsContainer();

            _sticks = new SettingsTab(Controller.GetLanguage()["SetsTabSticks"]);
            _sticksContent = new SticksSettingsContainer();

            SaveBtn = Items.GetControlButton(Controller.GetLanguage()["SaveButton"]);

            // Adding content
            AddItems(_titleBar, layout);
            layout.AddItems(
                _tabArea,
                SaveBtn
            );

            _tabArea.AddTab(_common);
            _tabArea.AddTab(_sticks);
            _tabArea.AddTab(_binds);

            _tabArea.AddItemToTab(_common, _commonContent as IBaseItem);
            _tabArea.AddItemToTab(_sticks, _sticksContent as IBaseItem);
            _tabArea.AddItemToTab(_binds, _bindsContent as IBaseItem);
        }

        public void UpdateUI()
        {
            _titleBar.SetText(Controller.GetLanguage()["SetsTitle"]);
            _common.SetText(Controller.GetLanguage()["SetsTabCommon"]);
            _binds.SetText(Controller.GetLanguage()["SetsTabBinds"]);
            _sticks.SetText(Controller.GetLanguage()["SetsTabSticks"]);
            SaveBtn.SetText(Controller.GetLanguage()["SaveButton"]);

            _commonContent.UpdateUI();
            _sticksContent.UpdateUI();
            _bindsContent.UpdateUI();
        }
    }
}