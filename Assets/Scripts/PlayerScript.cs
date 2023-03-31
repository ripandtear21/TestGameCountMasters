using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{ 
    [SerializeField] public int numberOfStickmans;
    [SerializeField] private int numberOfEnemyStickmans;
    [SerializeField] private TextMeshPro CounterTxt;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private Transform road;
    [SerializeField] private Transform enemy;
    [SerializeField] private bool moveByTouch;
    [SerializeField] public bool gameState;
    [SerializeField] private bool attack;

    public int NumberOfStickmans => numberOfStickmans;


    public float playerSpeed;
   public float roadSpeed;
   public GameObject SecondCam;
   public bool FinishLine;
   public bool moveTheCamera;
   public static PlayerScript PlayerScriptInstance;
   
   private Vector3 mouseStartPos;
   private Vector3 playerStartPos;
   private Transform player;
   private Camera camera;
   [Range(0f,1f)] [SerializeField] private float DistanceFactor, Radius;
   void Start()
    {
        CounterTxt.text = numberOfStickmans.ToString();
        PlayerScriptInstance = this;
        
        numberOfStickmans = transform.childCount - 1;
        player = transform;
        
        camera = Camera.main;
        gameState = false;
    }
    
    void Update()
    {
        if (attack)
        {
            var enemyDirection = new Vector3(enemy.position.x,transform.position.y,enemy.position.z) - transform.position;

            for (int i = 1; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = 
                    Quaternion.Slerp( transform.GetChild(i).rotation,
                        Quaternion.LookRotation(enemyDirection,Vector3.up), Time.deltaTime * 3f );
            }

            if (enemy.GetChild(1).childCount > 1)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    var Distance = enemy.GetChild(1).GetChild(0).position - transform.GetChild(i).position;

                    if (Distance.magnitude < 1.5f)
                    {
                        transform.GetChild(i).position = Vector3.Lerp(transform.GetChild(i).position, 
                            new Vector3(enemy.GetChild(1).GetChild(0).position.x,transform.GetChild(i).position.y,
                                enemy.GetChild(1).GetChild(0).position.z), Time.deltaTime * 1f );
                    }
                }
            }

            else
            {
                attack = false;
                roadSpeed = -3f;
                for (int i = 1; i < transform.childCount; i++)
                    transform.GetChild(i).rotation = Quaternion.identity;
                enemy.gameObject.SetActive(false);
                
                FormatGroup();
            }

            if (transform.childCount == 1)
            {
                enemy.transform.GetChild(1).GetComponent<EnemyScript>().StopAttack();
                gameObject.SetActive(false);
            }
        }
        else
        {
            MovePlayer();
        }

        
        if (transform.childCount == 1 && FinishLine)
        {
            gameState = false;
        }
        
       
        if (gameState && !FinishLine)
        {
          road.Translate(road.forward * (Time.deltaTime * roadSpeed));
            
            for (int i = 1; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).GetComponent<Animator>() != null)
                   transform.GetChild(i).GetComponent<Animator>().SetBool("move",true);
            }
        }

        
            
    }
    public void FormatGroup()
    {
        for (int i = 1; i < player.childCount; i++)
        {
            var x = DistanceFactor * Mathf.Sqrt(i) * Mathf.Cos(i * Radius);
            var z = DistanceFactor * Mathf.Sqrt(i) * Mathf.Sin(i * Radius);
            
            var NewPos = new Vector3(x,0.3f,z);

            player.transform.GetChild(i).DOLocalMove(NewPos, 0.5f).SetEase(Ease.OutBack);
        }
        
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
        //numberOfStickmans = transform.childCount - 1;
        //CounterTxt.text = numberOfStickmans.ToString();
    }
    

    public void MakeStickMan(int number)
    {
        for (int i = numberOfStickmans; i < number; i++)
        {
            Instantiate(playerPrefab, transform.position, Quaternion.identity, transform);
        }
        numberOfStickmans = transform.childCount - 1;
        CounterTxt.text = numberOfStickmans.ToString();
        FormatGroup();
    }
    
    void MovePlayer()
    {
        if (Input.GetMouseButtonDown(0) && gameState)
        {
            moveByTouch = true;
            
            var plane = new Plane(Vector3.up, 0f);

            var ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (plane.Raycast(ray,out var distance))
            {
                mouseStartPos = ray.GetPoint(distance + 1f);
                playerStartPos = transform.position;
            }

        }
        
        if (Input.GetMouseButtonUp(0))
        {
            moveByTouch = false;
            
        }
        
        if (moveByTouch)
        { 
            var plane = new Plane(Vector3.up, 0f);
            var ray = camera.ScreenPointToRay(Input.mousePosition);
            
            if (plane.Raycast(ray,out var distance))
            {
                var mousePos = ray.GetPoint(distance +  1f);
                   
                var move = mousePos - mouseStartPos;
                   
                var control = playerStartPos + move;


                if (numberOfStickmans > 50)
                    control.x = Mathf.Clamp(control.x, -0.7f, 0.7f);
                else
                    control.x = Mathf.Clamp(control.x, -1.1f, 1.1f);

                transform.position = new Vector3(Mathf.Lerp(transform.position.x,control.x,Time.deltaTime * playerSpeed)
                    ,transform.position.y,transform.position.z);
                  
            }
        }
    }
    public void TrappedInObstacle()
    {
        if (transform.childCount > 1)
        {
            //Destroy(transform.GetChild(1).gameObject);
            numberOfStickmans = transform.childCount - 1;
            CounterTxt.text = numberOfStickmans.ToString();
            //FormatGroup();
        }
    }
    IEnumerator Fight()
    {

        numberOfEnemyStickmans = enemy.transform.GetChild(1).childCount - 1;
        numberOfStickmans = transform.childCount - 1;
        var result = numberOfStickmans - numberOfEnemyStickmans;

        while (numberOfEnemyStickmans > 0 && numberOfStickmans > 0)
        {
            numberOfStickmans--;
            numberOfEnemyStickmans--;
            

            enemy.transform.GetChild(1).GetComponent<EnemyScript>().CounterTxt.text = numberOfEnemyStickmans.ToString();
            CounterTxt.text = numberOfStickmans.ToString();
            yield return null;
        }

        if (numberOfEnemyStickmans == 0)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).rotation = Quaternion.identity;
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Gate"))
        {
            other.transform.parent.GetChild(0).GetComponent<BoxCollider>().enabled = false; 
            other.transform.parent.GetChild(1).GetComponent<BoxCollider>().enabled = false;

            var gateManager = other.GetComponent<GatesScript>();

            numberOfStickmans = transform.childCount - 1;
            CounterTxt.text = numberOfStickmans.ToString();
            if (gateManager.Multiply)
            {
                MakeStickMan(numberOfStickmans * gateManager.RandomNumber);
            }
            else
            {
                MakeStickMan(numberOfStickmans + gateManager.RandomNumber);
            }
        }

        if (other.CompareTag("Enemy"))
        { 
            enemy = other.transform;
            attack = true;

            roadSpeed = 0.5f;
            
            other.transform.GetChild(1).GetComponent<EnemyScript>().Attack(transform);

            StartCoroutine(Fight());

        }

        if (other.CompareTag("Finish"))
        {
            SecondCam.SetActive(true);
            FinishLine = true;
            TowerScript.towerScriptInstance.CreateTower(transform.childCount - 1);
            transform.GetChild(0).gameObject.SetActive(false);
            
        }
    }
}
