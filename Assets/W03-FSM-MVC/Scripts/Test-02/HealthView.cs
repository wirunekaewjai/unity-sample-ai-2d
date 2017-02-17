using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Wirune.W03.Test02
{
    public class HealthView : View
    {
        public Slider slider;

        [CallbackAttribute]
        void OnHealthChanged(int health, int maxHealth)
        {
            slider.maxValue = maxHealth;
            slider.value = health;
        }
    }
}
