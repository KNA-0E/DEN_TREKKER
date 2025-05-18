using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerTorch : MonoBehaviour
{
    public float burnDuration = 10f;
    public float maxLightIntensity = 1.5f;
    public float dimStartTime = 3f;

    public GameObject torchLight;
    public GameObject torchVisual;
    public float knockbackForce = 5f;

    private bool hasTorch = false;
    private float burnTimer = 0f;

    void Start() => SetTorchState(0f);

    void Update()
    {
        if (!hasTorch) return;

        burnTimer += Time.deltaTime;
        float timeLeft = burnDuration - burnTimer;

        if (timeLeft <= dimStartTime)
        {
            float dimProgress = Mathf.Clamp01(timeLeft / dimStartTime);
            SetTorchState(dimProgress);
        }
        else
        {
            SetTorchState(1f);
        }

        if (burnTimer >= burnDuration)
            BurnOut();
    }

    public void ActivateTorch()
    {
        if (hasTorch) return;
        hasTorch = true;
        burnTimer = 0f;
        Debug.Log("Torch lit!");
    }

    private void BurnOut()
    {
        hasTorch = false;
        burnTimer = 0f;
        SetTorchState(0f);
        Debug.Log("Torch burned out.");
    }

    private void SetTorchState(float intensity)
    {
        if (torchLight != null)
        {
            torchLight.SetActive(intensity > 0f);
            var light2D = torchLight.GetComponent<Light2D>();
            if (light2D != null)
                light2D.intensity = intensity * maxLightIntensity;
        }

        if (torchVisual != null)
        {
            torchVisual.SetActive(intensity > 0f);
            var sr = torchVisual.GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                Color c = sr.color;
                c.a = intensity;
                sr.color = c;
            }
        }
    }

    public bool HasTorchActive() => hasTorch;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!hasTorch) return;

        if (other.CompareTag("Enemy"))
        {
            Rigidbody2D enemyRb = other.GetComponent<Rigidbody2D>();
            EnemyChase enemy = other.GetComponent<EnemyChase>();

            if (enemyRb != null)
            {
                Vector2 knockbackDir = (other.transform.position - transform.position).normalized;
                enemyRb.velocity = Vector2.zero;
                enemyRb.AddForce(knockbackDir * knockbackForce, ForceMode2D.Impulse);

                if (enemy != null) enemy.Stun();

                Debug.Log("Enemy repelled by torch!");
            }
        }
    }
}