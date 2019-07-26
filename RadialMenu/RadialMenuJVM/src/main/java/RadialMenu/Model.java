package RadialMenu;

import java.util.Random;

public class Model {
    private static Random _randomizer;

    public Model() {
        _randomizer = new Random();
    }

    public int getRandomNumber(int firstBound, int secondBound) {
        return _randomizer.nextInt(secondBound) + firstBound;
    }

    public String getRandomName() {
        boolean isFemale = _randomizer.nextBoolean();

        return (isFemale) ? NameStorage.femaleNames.get(_randomizer.nextInt(NameStorage.femaleNames.size()))
                : NameStorage.maleNames.get(_randomizer.nextInt(NameStorage.maleNames.size()));
    }
}