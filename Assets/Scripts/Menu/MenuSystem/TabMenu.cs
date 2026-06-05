using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.MenuSystem
{
    public class TabMenu
    {
        private int _selectedTabIndex;
        private List<TabPage> _tabs = new List<TabPage>();

        public void AddPage(Tab tab, Page page)
        {
            _tabs.Add(new TabPage(tab, page));
        }

        public void Draw()
        {
            var canvas = Resources.Load<Canvas>("Prefabs/UI/Tabbed/TabbedCanvas");
            var canvasInstance = Object.Instantiate(canvas);
            var menu = canvasInstance.transform.Find("Menu").gameObject;
            var tabsContainer = menu.transform.Find("Tabs").gameObject;
            var pagesContainer = menu.transform.Find("Pages").gameObject;
            
            foreach (TabPage tabPage in _tabs)
            {
                var tab = tabPage.tab;
                var page = tabPage.page;
                
                tab.InstantiatePrefab(tabsContainer.transform);
                page.InstantiatePrefab(pagesContainer.transform);

                tab.GetInstance.GetComponent<Button>().onClick.AddListener(() => SelectTab(_tabs.IndexOf(tabPage)));
                
                foreach (var entry in page.Entries)
                {
                    entry.InstantiatePrefab(page.GetInstance.GetComponent<VerticalLayoutGroup>().transform);
                }
            }
            
            SelectTab(0);
        }

        public Page GetPage(int index)
        {
            return _tabs[index].page;
        }

        public Tab GetTab(int index)
        {
            return _tabs[index].tab;
        }

        public void SelectTab(int index)
        {
            _selectedTabIndex = index;
            Page page = GetPage(index);
            
            page.Show();
            foreach (TabPage tabPage in _tabs)
            {
                if (page != tabPage.page)
                {
                    tabPage.page.Hide();  
                }            
            }
        }
    }
}