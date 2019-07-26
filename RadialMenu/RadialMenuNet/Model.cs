using System;

namespace RadialMenu
{
    public class Model
    {
        private Random _randomizer;
        public Model()
        {
            _randomizer = new Random();
        }

        public int GetRandomNumber(int firstBound, int secondBound)
        {
            return _randomizer.Next(firstBound, secondBound);
        }

        public String getRandomName()
        {
            bool isFemale = Convert.ToBoolean(_randomizer.Next(2));

            return (isFemale)
                ? NameStorage.FemaleNames[_randomizer.Next(NameStorage.FemaleNames.Count)]
                : NameStorage.MaleNames[_randomizer.Next(NameStorage.MaleNames.Count)];
        }
    }
}