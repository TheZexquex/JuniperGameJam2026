namespace Menu.MenuSystem
{
    public class TabPage
    {
        public Tab tab { get; }
        public Page page { get; }

        public TabPage(Tab Tab, Page Page)
        {
            tab = Tab;
            page = Page;
        }
    }
}