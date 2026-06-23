using System.Collections;
using TopDown.Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Health_Controll : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    private Animator anim;
    private PlayerMovement Player;
    private CameraShake cameraShake;
    public bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = startingHealth;
        Player = GetComponent<PlayerMovement>();
        cameraShake = GameObject.Find("CinemachineTrigger").GetComponent<CameraShake>();


        if (GetComponent<Agent>() != null)
        {
            anim = GameObject.FindWithTag("Enemy_Sprite").GetComponent<Animator>();
        }
        else if(Player != null)
        {
            anim = GameObject.Find("Torso").GetComponent<Animator>();
        }
        else
        {
            anim = GetComponent<Animator>();
        }

        /*if(Player != null )
        {
            currentHealth = NewGameDataManager.Instance.GetPlayerHP();
            Debug.LogError("데이터 로드 성공");
        }*/
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth = (currentHealth - damage);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            
            if(Player != null)
            {
                cameraShake.HitCam();
            }

        }
        else
        {
            if (dead == false)
            {
                anim.SetTrigger("die");

                if (Player != null)
                {
                    Player.enabled = false;
                    GetComponent<PlayerInput>().enabled = false;
                    GameObject.Find("Legs").GetComponent<SpriteRenderer>().enabled = false;
                }

                if (GetComponent<Test_gameover>() != null)
                {
                    GetComponent<Test_gameover>().enabled = false;
                    DestroyObj();
                }

                if (GetComponent<Agent>() != null)
                {
                    GetComponent<Agent>().enabled = false;
                    GetComponent<CircleCollider2D>().enabled = false;
                    EnemyAI enemyAi = GetComponent<EnemyAI>();
                    Destroy(enemyAi);
                    Rigidbody2D body = GetComponent<Rigidbody2D>();
                    EnemyDrop enemyDrop = GetComponent<EnemyDrop>();
                    enemyDrop.HandleDrop();

                    
                    body.freezeRotation = true;
                    body.constraints = RigidbodyConstraints2D.FreezeAll;
                    
                }

                Debug.Log("hit");

                dead = true;



            }
        }
    }



    public void AddHealth(float Add)
    {
        currentHealth += Add;
        if(currentHealth >= startingHealth)
        {
            currentHealth = startingHealth;
        }
    }


    void DestroyObj()
    {
        Destroy(this.gameObject);
    }

}