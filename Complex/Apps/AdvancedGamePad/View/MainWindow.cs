using System.Linq;
using System.Collections.Generic;

using SpaceVIL;

using AdvancedGamePad.Core;
using AdvancedGamePad.Model;
using AdvancedGamePad.Factory;

namespace AdvancedGamePad.View
{
    public class MainWindow : ActiveWindow, IUpdateUI
    {
        public TitleBar Title { get; private set; }
        public Button BtnStatus { get; private set; }
        public Button BtnSettings { get; private set; }
        public Button BtnStart { get; private set; }
        public ImageItem ImgStatus { get; private set; }
        public ComboBox ProfileList { get; private set; }

        public override void InitWindow()
        {
            // Window's attr
            SetParameters(Controller.GetLanguage()["MainTitle"], Controller.GetLanguage()["MainTitle"], 300, 360, false);
            SetMinSize(300, 360);
            SetBackground(Palette.CommonDark);
            IsCentered = true;
            SetIcon(Resources.Icon, Resources.Icon);

            // Content
            Title = Items.GetTitleBar(Controller.GetLanguage()["MainTitle"]);

            VerticalStack layout = Items.GetLayout();
            layout.SetMargin(0, Title.GetHeight(), 0, 0);

            BtnStatus = Items.GetButton(Controller.GetLanguage()["BatteryButton"]);
            BtnSettings = Items.GetButton(Controller.GetLanguage()["SettingsButton"]);
            BtnStart = Items.GetButton(Controller.GetLanguage()["StartButton"]);

            ImgStatus = Items.GetBatteryItem();

            ProfileList = Items.GetProfileList();
            foreach (var profile in Controller.GetAllProfiles())
            {
                ProfileList.AddItem(Items.GetListItem(profile.Name));
            }

            // Adding content
            AddItems(Title, layout);
            layout.AddItems(
                ProfileList,
                ImgStatus,
                BtnStatus,
                BtnSettings,
                new Frame(),
                BtnStart
                );

            // Post settings
            ProfileList.SetCurrentIndex(
                    Controller.GetAllProfiles().Select(p => p.Name).ToList().IndexOf(Controller.GetProfile().Name));

            ProfileList.SelectionChanged += () =>
            {
                Controller.InvokeEventProfileChanged(ProfileList.GetText());
            };
        }

        public void UpdateUI()
        {
            List<Profile> currentProfiles = Controller.GetAllProfiles();
            if (ProfileList.GetListContent().Count != currentProfiles.Count)
            {
                var profiles = ProfileList.GetListContent();
                foreach (var p in profiles)
                {
                    ProfileList.RemoveItem(p);
                }
                foreach (var profile in currentProfiles)
                {
                    ProfileList.AddItem(Items.GetListItem(profile.Name));
                }
            }

            ProfileList.SetCurrentIndex(
                    Controller.GetAllProfiles().Select(p => p.Name).ToList().IndexOf(Controller.GetProfile().Name));

            Title.SetText(Controller.GetLanguage()["MainTitle"]);
            BtnStatus.SetText(Controller.GetLanguage()["BatteryButton"]);
            BtnSettings.SetText(Controller.GetLanguage()["SettingsButton"]);

            UpdateUIStartButton();
        }

        public void UpdateUIStartButton(bool isStart = true)
        {
            if (isStart)
                BtnStart.SetText(Controller.GetLanguage()["StartButton"]);
            else
                BtnStart.SetText(Controller.GetLanguage()["StopButton"]);
        }

        public void UpdateBatteryLevel(GamePadBatteryLevel level)
        {
            switch (level)
            {
                case GamePadBatteryLevel.Empty:
                    ImgStatus.SetImage(Resources.BatteryEmptyIcon);
                    break;
                case GamePadBatteryLevel.Low:
                    ImgStatus.SetImage(Resources.BatteryLowIcon);
                    break;
                case GamePadBatteryLevel.Medium:
                    ImgStatus.SetImage(Resources.BatteryMediumIcon);
                    break;
                case GamePadBatteryLevel.Full:
                    ImgStatus.SetImage(Resources.BatteryFullIcon);
                    break;

                default:
                    break;
            }
        }
    }
}