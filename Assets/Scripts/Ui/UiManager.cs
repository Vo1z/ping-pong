using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TestTask.UI
{
    public class UiManager : MonoBehaviour
    {
        [Header("Obligatory options")]
        [SerializeField] private Animator transition;

        [Header("Options for ingame UI")]
        [SerializeField] private GameObject hud;
        [SerializeField] private GameObject pauseMenu;
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

        public void StartNewGame()
        {
            StartCoroutine(TransitToTheScene(1, 1f));
        }

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

        private IEnumerator TransitToTheScene(int sceneIndex, float pauseBetweenTransitionInSeconds)
        {
            transition.SetTrigger("InvokeEndFade");
            yield return new WaitForSeconds(pauseBetweenTransitionInSeconds);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}