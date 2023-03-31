using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnLevelComplete;
    
    public static void LevelComplete()
    {
        OnLevelComplete?.Invoke();
    }
    public static Action OnFighterHit;
    
    public static void FighterHit()
    {
        OnFighterHit?.Invoke();
    }
    public static Action<int> SendMoney;
    
    public static void SendMoneyToPlayer(int money)
    {
        SendMoney?.Invoke(money);
    }
}
