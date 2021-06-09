using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestTask.UI
{
    public class UiManager : MonoBehaviour
    {
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject pauseMenu;
        [Space]
        [SerializeField] private TextMeshProUGUI levelLabel;
        
        private void Start()
        {
            if(GameManager.Instance != null)
                GameManager.Instance.OnLevelUp += UpdateLevelLabel;
            UpdateLevelLabel();
            
            if(hud != null)
                hud.SetActive(true);
            if(pauseMenu != null)
                pauseMenu.SetActive(false);
        }

        public void StartNewGame() => SceneManager.LoadScene(1);
        
        public void InvokeGameMenu()
        {
            if(hud != null)
                hud.SetActive(false);
            if(pauseMenu != null)
                pauseMenu.SetActive(true);
            
            Time.timeScale = 0;
        }

        public void InvokeHUD()
        {
            if(pauseMenu != null)
                pauseMenu.SetActive(false);
            if(hud != null)
                hud.SetActive(true);

            Time.timeScale = 1;
        }

        public void ExitGame() => Application.Quit();

        private void UpdateLevelLabel()
        {
            if(levelLabel != null)
                levelLabel.text = $"{GameManager.Instance.CurrentLevel}";
        }
    }
}