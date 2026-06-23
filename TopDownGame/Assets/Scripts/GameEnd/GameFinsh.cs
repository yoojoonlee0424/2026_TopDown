using UnityEngine;

public class GameFinsh : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.Instance.GoTitle();
            Debug.Log("∞‘¿” ≥°");
        }
    }
}

