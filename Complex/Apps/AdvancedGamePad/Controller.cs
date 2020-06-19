using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Windows.Forms;

using SpaceVIL;

using AdvancedGamePad.View;
using AdvancedGamePad.Model;
using AdvancedGamePad.Core;

namespace AdvancedGamePad
{
    public class Controller
    {
        #region static
        private static Mutex mutex = new Mutex(true, Marshal.GetTypeLibGuidForAssembly(Assembly.GetExecutingAssembly()).ToString());

        private static ConfigManager _cfgManager = null;

        internal static CommonSettings GetSettings()
        {
            return _cfgManager.CommonSettings;
        }

        internal static Profile GetProfile()
        {
            return _cfgManager.CurrentProfile;
        }

        internal static List<Profile> GetAllProfiles()
        {
            List<Profile> list = _cfgManager.ProfileList.Values.ToList();
            return list.OrderBy(p => p.Name).ToList();
        }

        internal static Dictionary<string, string> GetLanguage()
        {
            return _cfgManager.CurrentLanguage;
        }

        internal static Dictionary<string, Dictionary<string, string>> GetLocalizations()
        {
            return _cfgManager.Languages.GetLocalizations();
        }

        internal static void InvokeEventProfileChanged(string name)
        {
            if (name.Equals(GetProfile().Name))
                return;
            _cfgManager.EventOnProfileChange?.Invoke(name);
        }

        internal static void InvokeEventLanguageChanged(string name)
        {
            _cfgManager.EventOnLanguageChange?.Invoke(name);
        }

        internal static void InvokeEventSettingsChanged()
        {
            _cfgManager.EventOnSettingsChange?.Invoke();
        }

        internal static void InvokeEventAddProfile(string name, bool isCopy = false)
        {
            if (isCopy)
            {
                _cfgManager.CopyProfile(name);
            }
            else
            {
                _cfgManager.AddProfile(name);
            }
            InvokeEventProfileChanged(name);
        }

        internal static void InvokeEventDeleteProfile()
        {
            if (_cfgManager.DeleteProfile(GetProfile()))
            {
                _cfgManager.ApplyProfile(GetAllProfiles().ElementAt(0));
                _cfgManager.EventOnProfileChange?.Invoke(GetProfile().Name);
            }
        }
        #endregion

        private IGamePad _gamepad = null;
        private IGamePadHandler _gamepadHandler = null;

        private MainWindow _mainWindow = null;
        private SettingsWindow _settingsWindow = null;
        private NotifyIcon _baloonTooltip = null;
        private System.Windows.Forms.MenuItem _showMenuItem = null;
        private System.Windows.Forms.MenuItem _exitMenuItem = null;

        public Controller()
        {
            _cfgManager = new ConfigManager();

            CheckAppInstance();

            _gamepad = new XInputGamePad();
            _gamepadHandler = new XInputHandler(_gamepad);

            _mainWindow = new MainWindow();
            _settingsWindow = new SettingsWindow();

            _baloonTooltip = Factory.Items.GetBallonToolTip(Controller.GetLanguage()["BalloonText"]);
            _baloonTooltip.Click += new EventHandler((sender, args) =>
            {
                MouseEventArgs margs = args as MouseEventArgs;
                if (margs != null)
                {
                    if (margs.Button == MouseButtons.Left)
                    {
                        _mainWindow.SetHidden(false);
                        _mainWindow.Minimize();
                    }
                }
            });

            _showMenuItem = Factory.Items.GetContextMenuItem(Controller.GetLanguage()["TrayShow"], 1);
            _showMenuItem.Click += new EventHandler((sender, args) =>
            {
                if (_mainWindow.IsMinimized())
                    _mainWindow.Minimize();
            });

            _exitMenuItem = Factory.Items.GetContextMenuItem(Controller.GetLanguage()["TrayExit"], 2);
            _exitMenuItem.Click += new EventHandler((sender, args) =>
            {
                _mainWindow.EventClose?.Invoke();
            });

            _baloonTooltip.ContextMenu = Factory.Items.GetTrayContextMenu(_showMenuItem, _exitMenuItem);
        }

        public void Start()
        {
            InitController();
            _mainWindow.Show();
        }

        private void InitController()
        {
            if (GetSettings().StartAppMinimized)
            {
                _mainWindow.Minimize();
            }

            if (GetSettings().AutoStart)
            {
                _mainWindow.EventOnStart += () =>
                {
                    _mainWindow.BtnStart.EventMouseClick?.Invoke(_mainWindow.BtnStart, null);
                };
            }

            _mainWindow.EventClose += () =>
            {
                _baloonTooltip.Visible = false;
                _baloonTooltip.Dispose();
                WindowManager.AppExit();
            };

            _mainWindow.BtnSettings.EventMouseClick += (sender, args) =>
            {
                _gamepadHandler.Stop();
                _settingsWindow.Show();
                _mainWindow.UpdateUIStartButton(!_gamepadHandler.IsRunning());
            };

            _mainWindow.Title.GetMinimizeButton().EventMouseClick += (sender, args) =>
            {
                _mainWindow.SetHidden(true);
                _baloonTooltip.ShowBalloonTip(1000);
            };

            _mainWindow.BtnStart.EventMouseClick += (sender, args) =>
            {
                if (_gamepadHandler.IsRunning())
                {
                    _gamepadHandler.Stop();
                }
                else
                {
                    _gamepadHandler.Run();
                }
                _mainWindow.UpdateUIStartButton(!_gamepadHandler.IsRunning());
            };

            _mainWindow.BtnStatus.EventMouseClick += (sender, args) =>
            {
                if (!_gamepad.IsConnected())
                {
                    SpaceVIL.MessageBox msgBox = new SpaceVIL.MessageBox(GetLanguage()["MsgIsConnection"],
                            GetLanguage()["OutputTitle"]);

                    msgBox.GetCancelButton().SetVisible(false);
                    msgBox.Show();
                }
                else
                {
                    _mainWindow.UpdateBatteryLevel(_gamepad.GetBatteryInfo());
                }
            };

            _settingsWindow.SaveBtn.EventMouseClick += (sender, args) =>
            {
                _cfgManager.SaveSettings();
                _cfgManager.SaveProfile();
            };

            _cfgManager.EventOnIOProfileFail += (message, title) =>
            {
                SpaceVIL.MessageBox msgException = new SpaceVIL.MessageBox(message, title);
                msgException.GetCancelButton().SetVisible(false);
                msgException.Show();
            };

            _cfgManager.EventOnIOSettingsFail += _cfgManager.EventOnIOProfileFail;

            _cfgManager.EventOnLanguageChange += (name) =>
            {
                _baloonTooltip.BalloonTipText = Controller.GetLanguage()["BalloonText"];
                _showMenuItem.Text = Controller.GetLanguage()["TrayShow"];
                _exitMenuItem.Text = Controller.GetLanguage()["TrayExit"];
                _settingsWindow.UpdateUI();
                _mainWindow.UpdateUI();
            };

            _cfgManager.EventOnProfileChange += (name) =>
            {
                _cfgManager.CurrentProfile = _cfgManager.ProfileList[name];
                _cfgManager.CommonSettings.DefaultProfile = _cfgManager.CurrentProfile.Name;
                _settingsWindow.UpdateUI();
                _mainWindow.UpdateUI();
            };

            (_gamepadHandler as XInputHandler).OnBatteryLevelChanged = (level) =>
            {
                _mainWindow.UpdateBatteryLevel(level);
            };
        }

        private void CheckAppInstance()
        {
            if (!mutex.WaitOne(TimeSpan.Zero, true))
            {
                SpaceVIL.MessageBox msg = new SpaceVIL.MessageBox(Controller.GetLanguage()["IsRunning"],
                        Controller.GetLanguage()["OutputTitle"]);
                msg.GetCancelButton().SetVisible(false);
                msg.Show();
                msg.OnCloseDialog += () =>
                {
                    Environment.Exit(0);
                };
            }
        }
    }
}