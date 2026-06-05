using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu.MenuSystem
{
    public class TogglePageEntry : PageEntry<bool>
    {
        public TogglePageEntry(string label, string description, UnityAction<bool> onValueChanged) 
            : base("ToggleEntry", label, description, onValueChanged)
        {
        }
        
        public override void InstantiatePrefab(Transform parent)
        {
            base.InstantiatePrefab(parent);
            var toggle = parent.GetComponentInChildren<Toggle>();
            
            toggle.onValueChanged.AddListener(OnValueChanged);
        }
    }
}