using System.Collections.Generic;
using UnityEngine;

namespace Menu.MenuSystem
{
    public class Page : UIPrefab
    { 
        private List<PageEntry> _entries = new List<PageEntry>();

        public Page() : base("Tabbed/Page")
        {
        }

        public List<PageEntry> Entries => _entries;
        
        public void AddEntry(PageEntry entry)
        {
            _entries.Add(entry);
        }
        
        public void Show()
        {
            Instance.SetActive(true);
        }
        
        public void Hide()
        {
            Instance.SetActive(false);
        }
    }
}