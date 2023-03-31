using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public TextMeshPro CounterTxt;
    [SerializeField] private GameObject enemyPrefab;
    [Range(0f,1f)] [SerializeField] private float DistanceFactor, Radius;

    public Transform enemy;
    public bool attack;
    void Start()
    {
        for (int i = 0; i < Random.Range(20,100); i++)
        {
            Instantiate(enemyPrefab, transform.position, new Quaternion(0f, 180f, 0f, 1f), transform);
        }
        CounterTxt.text = (transform.childCount - 1).ToString();
        FormatGroup();
    }
    void Update()
    {
        if (attack && transform.childCount > 1)
        {
            
            //Debug.Log("Attack");
            var enemyDirection = enemy.position - transform.position;

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.Slerp(transform.GetChild(i).rotation,
                    Quaternion.LookRotation(enemyDirection,Vector3.up), Time.deltaTime * 3f);

                if (enemy.childCount > 1)
                {
                    var distance = enemy.GetChild(1).position - transform.GetChild(i).position;

                    if (distance.magnitude < 1.5f)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position,
                            enemy.GetChild(1).position,Time.deltaTime * 2f);
                    } 
                }
              
            }
        }
    }
    private void FormatGroup()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
            
            var NewPos = new Vector3(x,-0.6f,z);

            transform.transform.GetChild(i).localPosition = NewPos;
        }
    }
    public void Attack(Transform enemyForce)
    {
        enemy = enemyForce;
        attack = true;

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("move",true);
        }
    }

    public void StopAttack()
    {
         PlayerScript.PlayerScriptInstance.gameState =  attack = false;
        
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Animator>().SetBool("move",false);
        }
        
    }
}
