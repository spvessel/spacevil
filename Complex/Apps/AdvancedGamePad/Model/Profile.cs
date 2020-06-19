using System;
using System.Text;

using AdvancedGamePad.Core;

namespace AdvancedGamePad.Model
{
    public class Profile : ICloneable
    {
        public Profile() { }

        public string Name;
        public HardwareButtons[] ButtonA;
        public HardwareButtons[] ButtonX;
        public HardwareButtons[] ButtonY;
        public HardwareButtons[] ButtonB;
        public HardwareButtons[] ButtonDUp;
        public HardwareButtons[] ButtonDDown;
        public HardwareButtons[] ButtonDLeft;
        public HardwareButtons[] ButtonDRight;
        public HardwareButtons[] ButtonHome;
        public HardwareButtons[] ButtonBack;
        public HardwareButtons[] ButtonStart;
        public HardwareButtons[] ButtonRT;
        public HardwareButtons[] ButtonRB;
        public HardwareButtons[] ButtonLT;
        public HardwareButtons[] ButtonLB;
        public HardwareButtons[] ButtonRS;
        public HardwareButtons[] ButtonLS;

        public bool LeftThumbStickAcceleration;
        public uint LeftThumbStickSpeed;
        public StickActionType LeftStickAction;

        public bool RightThumbStickAcceleration;
        public uint RightThumbStickSpeed;
        public StickActionType RightStickAction;

        public RepeatType RepeatMode;

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append("Profile path: " + Name);
            sb.Append("ButtonA=" + ButtonA);
            sb.Append("ButtonX=" + ButtonX);
            sb.Append("ButtonY=" + ButtonY);
            sb.Append("ButtonB=" + ButtonB);
            sb.Append("ButtonDUp=" + ButtonDUp);
            sb.Append("ButtonDDown=" + ButtonDDown);
            sb.Append("ButtonDLeft=" + ButtonDLeft);
            sb.Append("ButtonDRight=" + ButtonDRight);
            sb.Append("ButtonHome=" + ButtonHome);
            sb.Append("ButtonBack=" + ButtonBack);
            sb.Append("ButtonStart=" + ButtonStart);
            sb.Append("ButtonRT=" + ButtonRT);
            sb.Append("ButtonRB=" + ButtonRB);
            sb.Append("ButtonLT=" + ButtonLT);
            sb.Append("ButtonLB=" + ButtonLB);
            sb.Append("ButtonRS=" + ButtonRS);
            sb.Append("ButtonLS=" + ButtonLS);
            sb.Append("LeftThumbStickAcceleration=" + LeftThumbStickAcceleration);
            sb.Append("RightThumbStickAcceleration=" + RightThumbStickAcceleration);
            sb.Append("LeftThumbStickSpeed=" + LeftThumbStickSpeed);
            sb.Append("RightThumbStickSpeed=" + RightThumbStickSpeed);
            sb.Append("LeftStickAction=" + LeftStickAction);
            sb.Append("RightStickAction=" + RightStickAction);
            sb.Append("RepeatMode=" + RepeatMode);

            return sb.ToString();
        }

        public object Clone()
        {
            Profile clone = new Profile();
            clone.Name = this.Name;
            clone.ButtonA = this.ButtonA;
            clone.ButtonX = this.ButtonX;
            clone.ButtonY = this.ButtonY;
            clone.ButtonB = this.ButtonB;
            clone.ButtonDUp = this.ButtonDUp;
            clone.ButtonDDown = this.ButtonDDown;
            clone.ButtonDLeft = this.ButtonDLeft;
            clone.ButtonDRight = this.ButtonDRight;
            clone.ButtonHome = this.ButtonHome;
            clone.ButtonBack = this.ButtonBack;
            clone.ButtonStart = this.ButtonStart;
            clone.ButtonRT = this.ButtonRT;
            clone.ButtonRB = this.ButtonRB;
            clone.ButtonLT = this.ButtonLT;
            clone.ButtonLB = this.ButtonLB;
            clone.ButtonRS = this.ButtonRS;
            clone.ButtonLS = this.ButtonLS;

            clone.LeftThumbStickAcceleration = this.LeftThumbStickAcceleration;
            clone.LeftThumbStickSpeed = this.LeftThumbStickSpeed;
            clone.LeftStickAction = this.LeftStickAction;
            clone.RightThumbStickAcceleration = this.RightThumbStickAcceleration;
            clone.RightThumbStickSpeed = this.RightThumbStickSpeed;
            clone.RightStickAction = this.RightStickAction;
            clone.RepeatMode = this.RepeatMode;

            return clone;
        }
    }
}