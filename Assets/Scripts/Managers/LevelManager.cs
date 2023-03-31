using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public string[] levelNames;
    [SerializeField] private int currentLevelIndex = 0;

    private void OnEnable()
    {
        EventManager.OnLevelComplete += OnLevelComplete;
    }
    

    private void OnDisable()
    {
        EventManager.OnLevelComplete -= OnLevelComplete;
    }

    void LoadLevel(int levelIndex)
    {
        SceneManager.LoadScene(levelNames[levelIndex]);
    }

    void LoadNextLevel()
    {
        currentLevelIndex++;
        if (currentLevelIndex < levelNames.Length)
        {
            LoadLevel(currentLevelIndex);
        }
        else
        {
            currentLevelIndex = 0;
            LoadLevel(currentLevelIndex);
        }
    }
    public void OnLevelComplete()
    {
        LoadNextLevel();
    }
}
