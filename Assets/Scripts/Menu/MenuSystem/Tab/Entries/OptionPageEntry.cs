using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu.MenuSystem
{
    public class OptionPageEntry : PageEntry<bool>
    {
        public OptionPageEntry(string label, string description, UnityAction<bool> onValueChanged) 
            : base("EntryOptions", label, description, onValueChanged)
        {
        }
        
        public override void InstantiatePrefab(Transform parent)
        {
            base.InstantiatePrefab(parent);
            var switchElement = parent.Find("Switch").GetComponent<CycleController>();
            
            switchElement.on.AddListener(OnValueChanged);
        }
    }
}