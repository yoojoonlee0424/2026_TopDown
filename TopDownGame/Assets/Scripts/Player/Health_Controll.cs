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
    private bool dead = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = startingHealth;
        Player = GetComponent<PlayerMovement>();

        if (GetComponent<Agent>() != null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        else if(Player != null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        else
        {
            anim = GetComponent<Animator>();
        }

        if(Player != null )
        {
            currentHealth = NewGameDataManager.Instance.GetPlayerHP();
            Debug.LogError("데이터 로드 성공");
        }
        
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
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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