using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class JumpScript : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Ramp"))
        {
            transform.DOJump(transform.position, 1f, 1, 1f).SetEase(Ease.Flash);
        }
    }
    /*private void OnTriggerEnter(Collider other)
    {
        if(other.transform.CompareTag("Ramp"))
        {
            float jumpHeight = 1f;
            float jumpDuration = 1f;
            float lerpDuration = jumpDuration / 2f;

            Vector3 startPos = transform.position;
            Vector3 jumpPos = transform.position + Vector3.up * jumpHeight;
            Vector3 endPos = transform.position;

            StartCoroutine(PerformJump(startPos, jumpPos, endPos, lerpDuration));
        }
    }
    private IEnumerator PerformJump(Vector3 startPos, Vector3 jumpPos, Vector3 endPos, float lerpDuration)
    {
        float elapsedTime = 0f;

        while(elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            transform.position = Vector3.Lerp(startPos, jumpPos, t);
            yield return null;
        }

        elapsedTime = 0f;

        while(elapsedTime < lerpDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / lerpDuration);

            transform.position = Vector3.Lerp(jumpPos, endPos, t);
            yield return null;
        }

        PlayerScript.PlayerScriptInstance.FormatGroup();
    }*/
}
