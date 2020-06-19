using System.Drawing;
using System.Collections.Generic;
using System.Reflection;

using SpaceVIL;

namespace AdvancedGamePad.Factory
{
    public static class Resources
    {
        // Images
        public static readonly Bitmap Icon = GetResourceImage("Images.icon.png");
        public static readonly Bitmap TitleBarIcon = GetResourceImage("Images.simple_icon.png");
        public static readonly Bitmap BatteryEmptyIcon = GetResourceImage("Images.battery_empty.png");
        public static readonly Bitmap BatteryFullIcon = GetResourceImage("Images.battery_full.png");
        public static readonly Bitmap BatteryLowIcon = GetResourceImage("Images.battery_low.png");
        public static readonly Bitmap BatteryMediumIcon = GetResourceImage("Images.battery_medium.png");
        public static readonly Bitmap SwitcherOn = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.check_box_icon_on.png"), 56, 30);
        public static readonly Bitmap SwitcherOnHovered = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.check_box_icon_on_hover.png"), 56, 30);
        public static readonly Bitmap SwitcherOff = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.check_box_icon_off.png"), 56, 30);
        public static readonly Bitmap SwitcherOffHovered = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.check_box_icon_off_hover.png"), 56, 30);

        public static readonly Bitmap CopyIcon = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.copy.png"), 30, 30);
        public static readonly Bitmap PasteIcon = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.paste.png"), 30, 30);
        public static readonly Bitmap ClearIcon = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.clear.png"), 30, 30);
        public static readonly Bitmap PlusIcon = GraphicsMathService.ScaleBitmap(GetResourceImage("Images.plus.png"), 30, 30);

        private static Bitmap GetResourceImage(string resource)
        {
            return new Bitmap(Assembly
                .GetExecutingAssembly()
                .GetManifestResourceStream(resource));
        }

        // Default Lacale (English)
        internal static readonly Dictionary<string, string> EnglishLocale = new Dictionary<string, string>()
        {
            //SETTINGS
            //Common settings
            {"SetsTitle", "AdvancedGamePad: Settings"},
            {"SetsTabCommon", "Common"},
            {"SetsAutoLaunch", "Autolaunch on system startup"},
            {"SetsStartMinimized", "Start minimized"},
            {"SetsAutoStart", "Start working after app launch"},
            {"SetsLanguage", "Language:"},
            {"Locale", "English"},
            //Sections
            {"SetsSectionsCommon", "Common settings:"},
            {"SetsSectionsProfile", "Profile settings:"},
            {"SetsSectionsLocalization", "Localization:"},
            //Profile settings
            {"SetsProfile", "Profile:"},
            {"SetsInput", "Input mode:"},
            //Input modes
            {"SetsIMode0", "Without repeating"},
            {"SetsIMode1", "Display's refresh rate"},
            {"SetsIMode2", "Display's half refresh rate"},

            //Binds
            {"SetsTabBinds", "Binds"},
            {"A", "A button:"},
            {"X", "X button:"},
            {"Y", "Y button:"},
            {"B", "B button:"},
            {"Up", "Up button:"},
            {"Down", "Down button:"},
            {"Left", "Left button:"},
            {"Right", "Right button:"},
            {"Start", "Start button:"},
            {"Back", "Back button:"},
            {"LB", "Left shoulder:"},
            {"RB", "Right shoulder:"},
            {"LT", "Left trigger:"},
            {"RT", "Right trigger:"},
            {"LS", "Left thumb:"},
            {"RS", "Right thumb:"},
            //left stick settings
            {"SetsTabSticks", "Stick settings"},
            {"LeftStickSettings", "Left stick settings:"},
            {"AssignStick", "Assign to:"},
            {"LeftStickSpeed", "Left stick speed:"},
            //right stick settings
            {"RightStickSettings", "Right stick settings:"},
            {"StickDynamicSens", "On/Off dynamic sensitivity"},
            {"RightStickSpeed", "Right stick speed:"},
            //Save button
            {"SaveButton", "Save settings"},
            {"StickActionMouseMove", "Mouse movement"},
            {"StickActionArrows", "Keyboard arrows"},

            //MAINWINDOW
            {"MainTitle", "AdvancedGamePad"},
            {"BatteryButton", "Battery Status"},
            {"InfoCharge", "-"},
            {"SettingsButton", "Settings"},
            {"StartButton", "Start"},
            {"StopButton", "Stop"},
            {"Empty", "Empty"},
            {"Low", "Low"},
            {"Medium", "Medium"},
            {"Full", "Full"},
            {"BalloonText", "The app has been minimized. Click the tray icon to show."},
            {"TrayShow", "Show Advanced GamePad"},
            {"TrayExit", "Close application"},

            //INPUT
            {"InputTitle", "Create new profile:"},
            {"AddButton", "Add"},
            {"ApplyButton", "Apply"},

            //OUTPUT
            {"OutputTitle", "Message"},
            {"MsgSaveSuccess", "Save success!"},
            {"MsgSaveFail", "Save fail!"},
            {"MsgIsConnection", "GamePad is not connected."},
            {"DeleteProfileTitle", "Deleting the profile"},
            {"MsgConfirm", "Are you sure?"},
            {"MsgDeleteProfile", "The profile will be deleted:"},
            {"DeleteButton", "Delete"},
            {"CancelButton", "Cancel"},
            {"IsRunning" , "Application already running!"},
            {"OkButton", "Ok"},
            {"NoButton", "No"},
            {"YesButton", "Yes"},

            //MENU
            {"CopyMenuItem", "Copy"},
            {"PasteMenuItem", "Paste"},
            {"ClearMenuItem", "Clear"},
            {"ManualMenuItem", "Go to manual mode"},
        };
    }
}