using UnityEngine;

namespace TopDown.Shooting
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Projectile : MonoBehaviour
    {
        [Header("이동 설정")]
        [SerializeField] private float speed;
        [SerializeField] private float lifetime;
        private Rigidbody2D body2D;
        private float lifeTimer;

        private void Awake()
        {
            body2D = GetComponent<Rigidbody2D>();
        }
        public void ShootBullet(Transform shootPoint)
        {
            lifeTimer = 0;
            body2D.linearVelocity = Vector2.zero;
            transform.position = shootPoint.position;
            transform.rotation = shootPoint.rotation;
            gameObject.SetActive(true);

            body2D.AddForce(transform.up * speed, ForceMode2D.Impulse);
        }

        private void Update()
        {
            lifetime += Time.deltaTime;
            if(lifeTimer >= lifetime)
            {
                gameObject.SetActive(false);
            }
        }

    }
}

