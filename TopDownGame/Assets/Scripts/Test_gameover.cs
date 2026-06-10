using UnityEngine;

public class Test_gameover : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.LogError("Game Over Triggered");
            GameManager.Instance.GameOver();
        }
    }
}
