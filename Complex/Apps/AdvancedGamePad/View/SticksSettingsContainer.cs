using SpaceVIL;

using AdvancedGamePad.Factory;
using AdvancedGamePad.Core;
using AdvancedGamePad.Model;

namespace AdvancedGamePad.View
{
    public class SticksSettingsContainer : HorizontalStack, IUpdateUI
    {
        private VerticalStack _leftStickLayout = null;
        private VerticalStack _rightStickLayout = null;
        private Label _lLabel = null;
        private Label _rLabel = null;
        private SpeedSlider _lSpeedSlider = null;
        private SpeedSlider _rSpeedSlider = null;
        private NamedComboBox _lStickAssignment = null;
        private NamedComboBox _rStickAssignment = null;
        private ImagedCheckBox _lDynamicSensitivity = null;
        private ImagedCheckBox _rDynamicSensitivity = null;

        private MenuItem _lMouseMove = null;
        private MenuItem _rMouseMove = null;
        private MenuItem _lArrows = null;
        private MenuItem _rArrows = null;

        public SticksSettingsContainer()
        {
            SetSpacing(30, 0);
            SetPadding(10, 10, 10, 10);
            SetMargin(10, 10, 10, 10);

            _leftStickLayout = new VerticalStack();
            _rightStickLayout = new VerticalStack();
            _lLabel = Items.GetHeaderLabel(Controller.GetLanguage()["LeftStickSettings"]);
            _lLabel.SetMargin(0, 0, 0, 20);
            _rLabel = Items.GetHeaderLabel(Controller.GetLanguage()["RightStickSettings"]);
            _rLabel.SetMargin(0, 0, 0, 20);
            _lSpeedSlider = new SpeedSlider(Controller.GetLanguage()["LeftStickSpeed"]);
            _rSpeedSlider = new SpeedSlider(Controller.GetLanguage()["RightStickSpeed"]);
            _lStickAssignment = new NamedComboBox(Controller.GetLanguage()["AssignStick"]);
            _rStickAssignment = new NamedComboBox(Controller.GetLanguage()["AssignStick"]);
            _lDynamicSensitivity = Items.GetSwitcher(Controller.GetLanguage()["StickDynamicSens"], 
                    Controller.GetProfile().LeftThumbStickAcceleration);
            _rDynamicSensitivity = Items.GetSwitcher(Controller.GetLanguage()["StickDynamicSens"], 
                    Controller.GetProfile().RightThumbStickAcceleration);
            _lArrows = Items.GetListItem(Controller.GetLanguage()["StickActionArrows"]);
            _rArrows = Items.GetListItem(Controller.GetLanguage()["StickActionArrows"]);
            _lMouseMove = Items.GetListItem(Controller.GetLanguage()["StickActionMouseMove"]);
            _rMouseMove = Items.GetListItem(Controller.GetLanguage()["StickActionMouseMove"]);
        }

        public override void InitElements()
        {
            _leftStickLayout.SetSpacing(0, 10);
            _rightStickLayout.SetSpacing(0, 10);

            _lStickAssignment.SetPadding(0, 0, 40, 0);
            _rStickAssignment.SetPadding(0, 0, 40, 0);

            AddItems(
                _leftStickLayout,
                _rightStickLayout
            );

            _leftStickLayout.AddItems(
                _lLabel,
                _lDynamicSensitivity,
                _lSpeedSlider,
                _lStickAssignment
            );

            _rightStickLayout.AddItems(
                _rLabel,
                _rDynamicSensitivity,
                _rSpeedSlider,
                _rStickAssignment
            );

            _lStickAssignment.AddItems(
                _lMouseMove,
                _lArrows
            );

            _rStickAssignment.AddItems(
                _rMouseMove,
                _rArrows
            );

            ApplyProfile(Controller.GetProfile());

            _lSpeedSlider.EventValueChanged += () =>
            {
                Controller.GetProfile().LeftThumbStickSpeed = (uint)_lSpeedSlider.GetSpeed();
            };
            _rSpeedSlider.EventValueChanged += () =>
            {
                Controller.GetProfile().RightThumbStickSpeed = (uint)_rSpeedSlider.GetSpeed();
            };

            _lStickAssignment.SelectionChanged += () =>
            {
                Controller.GetProfile().LeftStickAction = (StickActionType) _lStickAssignment.GetCurrentIndex();
            };
            _rStickAssignment.SelectionChanged += () =>
            {
                Controller.GetProfile().RightStickAction = (StickActionType) _rStickAssignment.GetCurrentIndex();
            };

            _lDynamicSensitivity.EventToggle += (value) =>
            {
                Controller.GetProfile().LeftThumbStickAcceleration = value;
            };
            _rDynamicSensitivity.EventToggle += (value) =>
            {
                Controller.GetProfile().RightThumbStickAcceleration = value;
            };
        }

        private void ApplyProfile(Profile profile)
        {
            _lSpeedSlider.SetSpeed((int)profile.LeftThumbStickSpeed);
            _rSpeedSlider.SetSpeed((int)profile.RightThumbStickSpeed);

            _lStickAssignment.SetCurrentIndex(Parse.StickActionTypes(profile.LeftStickAction));
            _rStickAssignment.SetCurrentIndex(Parse.StickActionTypes(profile.RightStickAction));
        }

        public void UpdateUI()
        {
            _lLabel.SetText(Controller.GetLanguage()["LeftStickSettings"]);
            _rLabel.SetText(Controller.GetLanguage()["RightStickSettings"]);
            _lSpeedSlider.SetText(Controller.GetLanguage()["LeftStickSpeed"]);
            _rSpeedSlider.SetText(Controller.GetLanguage()["RightStickSpeed"]);
            _lStickAssignment.SetText(Controller.GetLanguage()["AssignStick"]);
            _rStickAssignment.SetText(Controller.GetLanguage()["AssignStick"]);
            _lDynamicSensitivity.SetText(Controller.GetLanguage()["StickDynamicSens"]);
            _lDynamicSensitivity.SetChecked(Controller.GetProfile().LeftThumbStickAcceleration);
            _rDynamicSensitivity.SetText(Controller.GetLanguage()["StickDynamicSens"]);
            _rDynamicSensitivity.SetChecked(Controller.GetProfile().RightThumbStickAcceleration);
            _lMouseMove.SetText(Controller.GetLanguage()["StickActionMouseMove"]);
            _lArrows.SetText(Controller.GetLanguage()["StickActionArrows"]);
            _rMouseMove.SetText(Controller.GetLanguage()["StickActionMouseMove"]);
            _rArrows.SetText(Controller.GetLanguage()["StickActionArrows"]);

            ApplyProfile(Controller.GetProfile());
        }
    }
}