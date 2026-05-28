using UnityEngine;
using UnityEngine.SceneManagement;

namespace Menu
{
    public class MainMenu : MonoBehaviour
    {

        public void PlayGame()
        {
            SceneManager.LoadScene("GameScene");
        }
    
        public void QuitGame()
        {
            Application.Quit();
        }
    
        public void Settings()
        {
        
        }
    }
}