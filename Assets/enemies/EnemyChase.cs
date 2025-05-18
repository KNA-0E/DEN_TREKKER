using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    public GameObject player;
    public float speed = 3f;
    public float distanceBetween = 5f;

    private float distance;
    private Rigidbody2D rb;
    private bool isStunned = false;
    private float stunDuration = 0.3f;
    private float stunTimer = 0f;

    public bool playerInRange = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (isStunned)
        {
            stunTimer -= Time.deltaTime;
            if (stunTimer <= 0f)
                isStunned = false;

            return; // basically 'skips' movement while it stunned
        }

        distance = Vector2.Distance(transform.position, player.transform.position);

        if (distance < distanceBetween)
        {
            playerInRange = true;

            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.MovePosition(rb.position + direction * speed * Time.deltaTime);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
        }
        else
        {
            playerInRange = false;
        }
    }

    public void Stun()
    {
        isStunned = true;
        stunTimer = stunDuration;
    }
}
