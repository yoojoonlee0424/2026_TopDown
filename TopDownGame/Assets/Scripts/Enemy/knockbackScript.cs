using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class knockbackScript : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private float thrust;
    [SerializeField] private float knockdownTime = 2f;
    private float knockupTimer;
    private bool isknockup;
    private Rigidbody2D rb;
    public bool knockBack;
    


    // Use this for initialization
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        Player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        knockupTimer += Time.deltaTime;

        if( knockupTimer <= knockdownTime )
        {
            isknockup = false;
        }
        if(isknockup)
        {
            knockDown();
        }

        knockBacking();
    }

    private void knockBacking()
    {
        Vector2 direction = (transform.position - Player.transform.position).normalized;

        if (knockBack)
        {
            knockBack = !knockBack;

            rb.AddForce(direction * thrust);

            isknockup = true;

        }

        knockupTimer = 0;
    }


    private void knockDown()
    {
        if (isknockup)
        {
            rb.freezeRotation = true;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }
}