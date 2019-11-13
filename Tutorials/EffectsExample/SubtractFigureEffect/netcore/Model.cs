using System;
using System.Collections.Generic;
using SpaceVIL.Core;
using SpaceVIL.Decorations;

namespace SubtractFigureEffect
{
    public static class Model
    {
        private static Dictionary<IBaseItem, IEffect> circleEffectsLayout = new Dictionary<IBaseItem, IEffect>();
        private static Dictionary<IBaseItem, IEffect> circleCenterEffectsLayout = new Dictionary<IBaseItem, IEffect>();

        public static void InitEffects(IBaseItem cRed, IBaseItem cGreen, IBaseItem cBlue)
        {
            circleEffectsLayout = new Dictionary<IBaseItem, IEffect>();
            circleEffectsLayout.Add(cRed, ItemsFactory.GetCircleEffect(cRed, cBlue));
            circleEffectsLayout.Add(cGreen, ItemsFactory.GetCircleEffect(cGreen, cRed));
            circleEffectsLayout.Add(cBlue, ItemsFactory.GetCircleEffect(cBlue, cGreen));

            circleCenterEffectsLayout = new Dictionary<IBaseItem, IEffect>();
            circleCenterEffectsLayout.Add(cRed, ItemsFactory.GetCircleCenterEffect(cRed));
            circleCenterEffectsLayout.Add(cGreen, ItemsFactory.GetCircleCenterEffect(cGreen));
            circleCenterEffectsLayout.Add(cBlue, ItemsFactory.GetCircleCenterEffect(cBlue));
        }

        public static void AddEffects()
        {
            foreach (var effect in circleEffectsLayout)
            {
                Effects.AddEffect(effect.Key, effect.Value);
            }

            foreach (var effect in circleCenterEffectsLayout)
            {
                Effects.AddEffect(effect.Key, effect.Value);
            }
        }

        public static void RemoveEffects()
        {
            foreach (var effect in circleEffectsLayout)
            {
                Effects.RemoveEffect(effect.Key, effect.Value);
            }

            foreach (var effect in circleCenterEffectsLayout)
            {
                Effects.RemoveEffect(effect.Key, effect.Value);
            }
        }
    }
}