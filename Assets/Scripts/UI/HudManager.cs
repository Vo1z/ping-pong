using System;
using TMPro;
using UnityEngine;

namespace TestTask.UI
{
    public class HudManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI levelLabel;
        
        private void Start()
        {
            GameManager.Instance.OnLevelUp += UpdateLevelLabel;
            UpdateLevelLabel();
        }

        public void InvokeGameMenu() => throw new NotImplementedException();
        
        private void UpdateLevelLabel() => levelLabel.text = $"{GameManager.Instance.CurrentLevel}";
    }
}