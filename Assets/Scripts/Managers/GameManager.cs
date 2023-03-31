using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject startTextGO;
    [SerializeField] private TextMeshProUGUI startText;
    
    void Start()
    {
        //DOTween.SetTweensCapacity(5000,5000);
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartGame();
            if (PlayerScript.PlayerScriptInstance.NumberOfStickmans == 0)
            {
                RestartLevel();
            }

            if (TowerScript.towerScriptInstance.IsTowerCreated)
            {
                EventManager.SendMoneyToPlayer(TowerScript.towerScriptInstance.towerCount);
                EventManager.LevelComplete();
            }
        }

        if (PlayerScript.PlayerScriptInstance.NumberOfStickmans == 0)
        {
            LooseGame();
        }
        if(TowerScript.towerScriptInstance.IsTowerCreated)
        {
            WinGame();
        }
    }

    public void StartGame()
    {
        startTextGO.SetActive(false);
        PlayerScript.PlayerScriptInstance.gameState = true;
    }
    public void LooseGame()
    {
        startTextGO.SetActive(true);
        startText.text = "You Loose";
        PlayerScript.PlayerScriptInstance.gameState = false;
    }
    public void WinGame()
    {
        startTextGO.SetActive(true);
        startText.text = "You Win";
        PlayerScript.PlayerScriptInstance.gameState = false;
        
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
