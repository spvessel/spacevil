using System;
using System.Collections.Generic;
using System.Text;

namespace CharacterEditor
{
    internal class CharacterInfo
    {
        internal String Name = String.Empty;
        internal CharacterRace Race = CharacterRace.Human;
        internal CharacterSex Sex = CharacterSex.Female;
        internal Int32 Age = 18;
        internal CharacterClass Class = CharacterClass.Assassin;
        internal String Сharacteristics = String.Empty;
        internal String PersonalSkills = String.Empty;
        internal String Biography = String.Empty;

        internal CharacterInfo() { }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("Name: " + Name + "\n");
            sb.Append("Race: " + Race + "\n");
            sb.Append("Sex: " + Sex + "\n");
            sb.Append("Age: " + Age + "\n\n");
            sb.Append("Class: " + Class + "\n\n");
            sb.Append("Сharacteristics: " + Сharacteristics + "\n");
            sb.Append("PersonalSkills:\n" + PersonalSkills + "\n");
            sb.Append("Biography:\n" + Biography + "\n");
            return sb.ToString();
        }
    }
}
