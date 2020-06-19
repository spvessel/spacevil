using System;

namespace AdvancedGamePad.Model
{
    public class CommonSettings
    {
        public CommonSettings() { }

        public Boolean AutoLaunchOnSystemStartUp
        {
            get;
            set;
        }
        public Boolean StartAppMinimized
        {
            get;
            set;
        }
        public Boolean AutoStart
        {
            get;
            set;
        }
        public String Language
        {
            get;
            set;
        }
        public String DefaultProfile
        {
            get;
            set;
        }

        public override string ToString()
        {
            return AutoLaunchOnSystemStartUp + "\n"
                + StartAppMinimized + "\n"
                + AutoStart + "\n"
                + Language + "\n"
                + DefaultProfile + "\n";
        }
    }
}