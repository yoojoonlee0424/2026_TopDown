using System.Collections;
using TopDown.Movement;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health_Controll : MonoBehaviour
{
    public float startingHealth;
    public float currentHealth;
    private Animator anim;
    private PlayerMovement Player;
    private bool dead = false;

    float score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        currentHealth = startingHealth;

        if (GetComponent<Agent>() != null)
        {
            anim = GetComponentInChildren<Animator>();
        }
        else
        {
            anim = GetComponent<Animator>();
        }

        if(Player != null )
        {
            Player = GetComponent<PlayerMovement>();
            currentHealth = NewGameDataManager.Instance.GetPlayerHP();
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
                    GetComponent<BoxCollider2D>().enabled = false;
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
                    Rigidbody2D body = GetComponent<Rigidbody2D>();
                    
                    body.freezeRotation = true;
                    body.constraints = RigidbodyConstraints2D.FreezeAll;
                    
                }

                Debug.Log("hit");

                dead = true;



            }
        }
    }






    void DestroyObj()
    {
        Destroy(this.gameObject);
    }

}