using Resources.Prefabs.UI.Common;
using UnityEngine;
using UnityEngine.Events;

namespace Menu.MenuSystem
{
    public class OptionPageEntry : PageEntry<string>
    {
        public OptionPageEntry(string label, string description, UnityAction<string> onValueChanged) 
            : base("EntryOptions", label, description, onValueChanged)
        {
        }
        
        public override void InstantiatePrefab(Transform parent)
        {
            base.InstantiatePrefab(parent);
            Debug.Log(parent);
            var switchElement = Instance.transform.Find("Switch").GetComponent<CycleController>();
            
            switchElement.onOptionChange.AddListener(OnValueChanged);
        }
    }
}