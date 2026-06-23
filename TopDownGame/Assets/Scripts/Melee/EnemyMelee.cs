using UnityEngine;

namespace TopDown.Melee
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class EnemyMelee : MonoBehaviour
    {
        [Header("지속 시간 설정")]
        [SerializeField] private float lifetime;
        [SerializeField] private float damage;
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
            if (collision.CompareTag("Player"))
            {
                collision.GetComponent<Health_Controll>().TakeDamage(damage);
                //속도 저하 효과 추가

                Debug.LogWarning("Player Melee!");
            }
        }
    }
}

