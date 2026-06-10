using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    public float MoveSpeed = 5f;

    public Sprite[] spritesUp;
    public Sprite[] spritesDown;
    public Sprite[] spritesLeft;
    public Sprite[] spritesRight;

    public float frameTime = 0.15f;

    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private Vector2 input;
    private Vector2 velocity;

    private Sprite[] currentSprites;

    private int FrameIndex;
    private float Timer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();

        currentSprites = spritesDown;

        sr.sprite = currentSprites[0];
    }

    public void OnMove(InputValue value)
    {
        input = value.Get<Vector2>();
        velocity = input.normalized * MoveSpeed;

        if(input.sqrMagnitude > 0.0f)
        {
            if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
            {
                if (input.x > 0)
                {
                    ChangeSprites(spritesRight);
                }
                else
                {
                    ChangeSprites(spritesLeft);
                }
            }
            else
            {
                if (input.y > 0)
                {
                    ChangeSprites(spritesUp);
                }
                else
                {
                    ChangeSprites(spritesDown);
                }
            }
        }


    }


    private void Update()
    {
        if (input.sqrMagnitude <= 0.01f)
        {
            FrameIndex = 0;
            sr.sprite = currentSprites[FrameIndex];
            return;
        }
        
        Timer += Time.deltaTime;

        if (Timer >= frameTime)
        {
            Timer = 0f;
            FrameIndex++;
            
            if (FrameIndex >= currentSprites.Length)
            {
                FrameIndex = 0;
            }

            sr.sprite = currentSprites[FrameIndex];
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }


    private void ChangeSprites(Sprite[] newSprites)
    {
        if (currentSprites == newSprites)
            return;


        currentSprites = newSprites;
        FrameIndex = 0;
        Timer = 0f;
        sr.sprite = currentSprites[FrameIndex];
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Item"))
        {
            GameDataManager.Instance.PlayerDead();
        }
    }








}
