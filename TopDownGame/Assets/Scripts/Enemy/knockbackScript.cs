using System.Collections;
using UnityEngine;

public class knockbackScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float thrust;
    [SerializeField] private float knockdownTime = 2f;
    private Rigidbody2D rb;
    private Health_Controll health_Controll;
    private AgentMover agentMover;
    public bool knockBack = false;

    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        health_Controll = GetComponent<Health_Controll>();
        agentMover = GetComponent<AgentMover>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (transform.position - Player.transform.position).normalized;


        if (knockBack)
        {
            rb.AddForce(direction * thrust);
            knockBackStart();
            knockBack = !knockBack;
        }
        else
        {
            knockBackStop();
        }

    }

    /*private void knockBacking()
    {
        Vector2 direction = (transform.position - Player.transform.position).normalized;

        if (knockBack)
        {
            knockBack = !knockBack;

            rb.AddForce(direction * thrust);

            
        }

        
    }


    private void knockDown()
    {
        rb.freezeRotation = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        Invoke("knockDownDisable", knockdownTime);
    }

    private void knockDownDisable()
    {
        rb.freezeRotation = false;
        rb.constraints = RigidbodyConstraints2D.None;

    }*/

    private void knockBackStart()
    {
        StartCoroutine(knockBacking());
        
    }

    private void knockBackStop()
    {
        StopCoroutine(knockBacking());
    }

    IEnumerator knockBacking()
    {

        yield return null;

        agentMover.maxSpeed = 0;

        yield return new WaitForSeconds(knockdownTime);

        agentMover.maxSpeed = agentMover.originalSpeed;

        if(health_Controll.currentHealth <= 0)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
    
}