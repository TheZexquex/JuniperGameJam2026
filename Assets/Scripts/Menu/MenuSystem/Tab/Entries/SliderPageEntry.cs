using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu.MenuSystem
{
    public class SliderPageEntry : PageEntry<float>
    {
        public SliderPageEntry(string label, string description, UnityAction<float> onValueChanged) 
            : base("EntrySlider", label, description, onValueChanged)
        {
        }

        public override void InstantiatePrefab(Transform parent)
        {
            base.InstantiatePrefab(parent);
            
            var slider = parent.GetComponentInChildren<Slider>();
            slider.onValueChanged.AddListener(OnValueChanged);
        }
    }
}