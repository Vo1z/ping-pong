using System;
using UnityEngine;

namespace TestTask
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }
        public event Action OnLevelUp;
        public int CurrentLevel { get; private set; } = 0;

        private void Awake()
        {
            if (Instance != this && Instance != null)
                Destroy(gameObject);
            else
                Instance = this;
        }

        public void LevelUp()
        {
            CurrentLevel++;

            if (OnLevelUp != null)
                OnLevelUp.Invoke();
        }
    }
}