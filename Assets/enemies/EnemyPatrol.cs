using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public EnemyChase chaseScript;
    public Transform[] patrolPoints;
    public float speed = 2f;
    public float loseSightTime = 3f;

    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float chaseTimer = 0f;

    void Update()
    {
        if (chaseScript == null || patrolPoints.Length == 0) return;

        if (chaseScript.playerInRange)
        {
            isChasing = true;
            chaseTimer = 0f;
        }
        else if (isChasing)
        {
            chaseTimer += Time.deltaTime;
            if (chaseTimer >= loseSightTime)
            {
                isChasing = false;
            }
        }

        if (!isChasing)
        {
            Patrol();
        }
    }

    void Patrol()
    {
        Transform target = patrolPoints[currentPatrolIndex];
        Vector2 direction = (target.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if (Vector2.Distance(transform.position, target.position) < 0.2f)
        {
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }

        Debug.Log("Patrolling...");

    }
}
