using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using SpaceVIL;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace CharacterEditor
{
    internal class Model
    {
        private Random _grn;
        internal Model() { _grn = new Random(); }

        internal List<CharacterInfo> GetListOfNames(int count)
        {
            List<CharacterInfo> list = new List<CharacterInfo>();
            for (Int32 i = 0; i < count; i++)
                list.Add(GenerateCharacter());
            return list;
        }

        internal CharacterInfo GenerateCharacter()
        {
            CharacterInfo ch = new CharacterInfo();

            ch.Sex = GenerateCharacterSex();
            ch.Name = GenerateName(ch.Sex);
            ch.Race = GenerateRace();
            ch.Age = GenerateAge(ch.Race);
            ch.Class = GenerateClass();
            ch.Сharacteristics = GenerateСharacteristics();

            return ch;
        }

        internal String GenerateName(CharacterSex sex)
        {
            StringBuilder name = new StringBuilder();
            switch (sex)
            {
                case CharacterSex.Female:
                    name.Append(StorageEmulation.FemaleNames[_grn.Next(0, StorageEmulation.FemaleNames.Count)]);
                    break;
                case CharacterSex.Male:
                    name.Append(StorageEmulation.MaleNames[_grn.Next(0, StorageEmulation.MaleNames.Count)]);
                    break;
            }
            name.Append(" " + StorageEmulation.SecondNames[_grn.Next(0, StorageEmulation.SecondNames.Count)]);
            return name.ToString();
        }

        internal CharacterSex GenerateCharacterSex()
        {
            return (CharacterSex)_grn.Next(1, 3);
        }

        internal Int32 GenerateAge(CharacterRace race)
        {
            Int32 age = 16;

            switch (race)
            {
                case CharacterRace.Human:
                    age = _grn.Next(age, 60);
                    break;
                case CharacterRace.Elf:
                    age = _grn.Next(50, 200);
                    break;
                case CharacterRace.Dwarf:
                    age = _grn.Next(50, 120);
                    break;
            }

            return age;
        }

        internal CharacterRace GenerateRace()
        {
            return (CharacterRace)_grn.Next(1, 4);
        }

        internal CharacterClass GenerateClass()
        {
            return (CharacterClass)_grn.Next(1, 10);
        }

        internal String GenerateСharacteristics()
        {
            StringBuilder characteristics = new StringBuilder();
            Сharacteristics[] chrsList = (Сharacteristics[])Enum.GetValues(typeof(Сharacteristics));
            int value = 0;
            foreach (Сharacteristics chrs in chrsList)
            {
                switch (chrs)
                {
                    case Сharacteristics.Health:
                        value = _grn.Next(500, 1000);
                        break;
                    case Сharacteristics.Mana:
                        value = _grn.Next(50, 300);
                        break;
                    case Сharacteristics.Endurance:
                        value = _grn.Next(50, 100);
                        break;
                    case Сharacteristics.Agility:
                        value = _grn.Next(5, 15);
                        break;
                    case Сharacteristics.Strength:
                        value = _grn.Next(5, 15);
                        break;
                    case Сharacteristics.Intelligence:
                        value = _grn.Next(5, 15);
                        break;
                    case Сharacteristics.Charisma:
                        value = _grn.Next(5, 15);
                        break;
                }
                characteristics.Append(chrs + ": " + value + "\n");
            }

            return characteristics.ToString();
        }

        internal bool WriteFile(String path, String text)
        {
            using (StreamWriter sr = new StreamWriter(path))
            {
                sr.Write(text);
            }
            return true;
        }
    }
}