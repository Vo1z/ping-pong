using System;
using UnityEngine;

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
        //todo debug
        print($"Current level is {CurrentLevel}");
        
        CurrentLevel++;
        
        if (OnLevelUp != null) 
            OnLevelUp.Invoke();
    }
}
