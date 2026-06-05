using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Menu.MenuSystem
{
    public class Tab : UIPrefab
    {
        private string _name;

        public string Name => _name;

        public Tab(string name) : base("Tabbed/Tab")
        {
            _name = name;
        }

        public override void InstantiatePrefab(Transform parent)
        {
            base.InstantiatePrefab(parent);
            var text = Instance.GetComponent<TextMeshProUGUI>();
            text.text = _name;
        }
    }
}