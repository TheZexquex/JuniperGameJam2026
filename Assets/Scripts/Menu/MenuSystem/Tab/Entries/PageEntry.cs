using System;
using TMPro;
using UnityEngine.Events;

namespace Menu.MenuSystem
{
    public abstract class PageEntry : UIPrefab
    {
        protected PageEntry(string prefabName, string label, TextMeshProUGUI labelText, string description, TextMeshProUGUI descriptionText, string prefabPath) : base(prefabName)
        {
            Label = label;
            LabelText = labelText;
            Description = description;
            DescriptionText = descriptionText;
            PrefabPath = prefabPath;
        }

        public string PrefabPath { get; protected set; }
        protected string Id;
        protected string Label;
        protected TextMeshProUGUI LabelText;
        protected string Description;
        protected TextMeshProUGUI DescriptionText;

        protected void Start()
        {
            LabelText.text = Label;
            DescriptionText.text = Description;
        }
    }

    public abstract class PageEntry<T> : PageEntry
    {
        protected UnityAction<T> OnValueChanged;
        protected T Value { get; set; }
        
        public PageEntry(string prefabPath, string label, string description, UnityAction<T> onValueChanged) : base(prefabPath, label, null, description, null, prefabPath)
        {
            Label = label;
            Description = description;
            PrefabPath = prefabPath;
            OnValueChanged = onValueChanged;
        }
        
        public void SetValue(T value)
        {
            Value = value;
        }
    }
}
