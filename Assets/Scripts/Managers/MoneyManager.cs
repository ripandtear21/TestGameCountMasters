using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int currentMoney = 0;
    private int savedMoney = 0;
    private string saveKey = "SavedMoney";

    private void OnEnable()
    {
        EventManager.SendMoney += AddMoney;
    }

    private void OnDisable()
    {
        EventManager.SendMoney -= AddMoney;
    }

    void Start()
    {
        savedMoney = PlayerPrefs.GetInt(saveKey, 0);
        moneyText.text = savedMoney.ToString() + "$";
    }

    public void AddMoney(int amount)
    {
        currentMoney += amount;
        PlayerPrefs.SetInt(saveKey, currentMoney + savedMoney);
        PlayerPrefs.Save();
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.SetInt(saveKey, currentMoney + savedMoney);
        PlayerPrefs.Save();
    }

}
