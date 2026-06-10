using UnityEngine;

namespace Menu.MenuSystem
{
    public class SettingsMenu : MonoBehaviour
    {
        private TabMenu _tabMenu;

        public void Start()
        {
            _tabMenu = new TabMenu();
            
            var audioTab = new Tab("Audio");
            var audioPage = new Page();
            audioPage.AddEntry(new SliderPageEntry("Master Volume ", "Change the Master Volume", (float value) =>
            {
                Debug.Log("Set Master Volume to " + value);
            }));
            audioPage.AddEntry(new SliderPageEntry("Music Volume ", "Change the Music Volume", (float value) =>
            {
                Debug.Log("Set Master Music to " + value);
            }));
            audioPage.AddEntry(new OptionPageEntry("Mute Music", "Mute the Music", (bool value) =>
            {
                Debug.Log("Set Music Mute to " + value);
            }));
            
            _tabMenu.AddPage(audioTab, audioPage);
            
            var graphicsTab = new Tab("Graphics");
            var graphicsPage = new Page();
            graphicsPage.AddEntry(new SliderPageEntry("Render Scaling ", "Change the Render Scaling", (float value) =>
            {
                Debug.Log("Set Render Scaling to " + value);
            }));
            
            _tabMenu.AddPage(graphicsTab, graphicsPage);
            
            var controlsTab = new Tab("Controls");
            var controlsPage = new Page();
            controlsPage.AddEntry(new OptionPageEntry("Invert Mouse", "Invert the Mouse", (bool value) =>
            {
                Debug.Log("Set Mouse Inversion to " + value);
            }));
            
            _tabMenu.AddPage(controlsTab, controlsPage);
            
            _tabMenu.Draw();
        }
    }
}