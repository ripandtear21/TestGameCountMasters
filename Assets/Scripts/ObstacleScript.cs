using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("blue"))
        {
            PlayerScript.PlayerScriptInstance.TrappedInObstacle();
            Destroy(other.gameObject);
        }
    }
}
