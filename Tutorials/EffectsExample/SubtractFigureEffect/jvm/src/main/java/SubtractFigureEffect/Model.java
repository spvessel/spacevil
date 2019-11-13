package SubtractFigureEffect;

import java.util.HashMap;
import java.util.Map;

import com.spvessel.spacevil.Core.InterfaceBaseItem;
import com.spvessel.spacevil.Core.InterfaceEffect;
import com.spvessel.spacevil.Decorations.Effects;

public final class Model {

    private Model() {
    }


    private static Map<InterfaceBaseItem, InterfaceEffect> circleEffectsLayout = new HashMap<>();
    private static Map<InterfaceBaseItem, InterfaceEffect> circleCenterEffectsLayout = new HashMap<>();

    public static void initEffects(InterfaceBaseItem cRed, InterfaceBaseItem cGreen, InterfaceBaseItem cBlue) {
        circleEffectsLayout = new HashMap<>();
        circleEffectsLayout.put(cRed, ItemsFactory.getCircleEffect(cRed, cBlue));
        circleEffectsLayout.put(cGreen, ItemsFactory.getCircleEffect(cGreen, cRed));
        circleEffectsLayout.put(cBlue, ItemsFactory.getCircleEffect(cBlue, cGreen));

        circleCenterEffectsLayout = new HashMap<>();
        circleCenterEffectsLayout.put(cRed, ItemsFactory.getCircleCenterEffect(cRed));
        circleCenterEffectsLayout.put(cGreen, ItemsFactory.getCircleCenterEffect(cGreen));
        circleCenterEffectsLayout.put(cBlue, ItemsFactory.getCircleCenterEffect(cBlue));
    }

    public static void addEffects() {
        for (Map.Entry<InterfaceBaseItem, InterfaceEffect> effect : circleEffectsLayout.entrySet()) {
            Effects.addEffect(effect.getKey(), effect.getValue());
        }

        for (Map.Entry<InterfaceBaseItem, InterfaceEffect> effect : circleCenterEffectsLayout.entrySet()) {
            Effects.addEffect(effect.getKey(), effect.getValue());
        }
    }

    public static void removeEffects() {
        for (Map.Entry<InterfaceBaseItem, InterfaceEffect> effect : circleEffectsLayout.entrySet()) {
            Effects.removeEffect(effect.getKey(), effect.getValue());
        }

        for (Map.Entry<InterfaceBaseItem, InterfaceEffect> effect : circleCenterEffectsLayout.entrySet()) {
            Effects.removeEffect(effect.getKey(), effect.getValue());
        }
    }
}
