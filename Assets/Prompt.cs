using UnityEngine;
using UnityEngine.UI;

public class PromptTrigger : MonoBehaviour
{
    public GameObject promptText;

    private void Start()
    {
        if (promptText != null)
            promptText.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
                promptText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (promptText != null)
                promptText.SetActive(false);
        }
    }
}