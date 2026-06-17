using UnityEngine;
using UnityEngine.AI;

namespace TopDown.Melee
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Melee : MonoBehaviour
    {
        [Header("지속 시간 설정")]
        [SerializeField] private float lifetime;
        [SerializeField] private float knockbackForce;
        private Rigidbody2D body2D;
        private float lifeTimer;


        private void Awake()
        {
            body2D = GetComponent<Rigidbody2D>();
        }
        

        private void Update()
        {
            lifeTimer += Time.deltaTime;
            if (lifeTimer >= lifetime)
            {
                gameObject.SetActive(false);
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Enemy"))
            {
                collision.GetComponent<Health_Controll>().TakeDamage(3);
                //넉백 효과 추가
                if(collision.GetComponent<knockbackScript>() != null)
                {
                    knockbackScript knockbackScript = collision.GetComponent<knockbackScript>();
                    knockbackScript.knockBack = true;
                }

                Debug.Log("Enemy Melee");
            }
        }
    }
}

