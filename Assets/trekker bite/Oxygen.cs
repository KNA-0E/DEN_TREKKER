using UnityEngine.UI;
using UnityEngine;

public class Oxygen : MonoBehaviour
{
    public float maxOxygen = 100f;
    public float currentOxygen;
    public float oxygenDrainRate = 2f;
    public float sprintDrainMultiplier = 2f;

    public Slider oxygenBar;
    private Sprint sprintScript;
    public GameObject O2Text;


    void Start()
    {
        currentOxygen = maxOxygen;
        sprintScript = GetComponent<Sprint>();
        if (oxygenBar != null)
            oxygenBar.maxValue = maxOxygen;

        if (O2Text != null)
            O2Text.SetActive(false);


    }

    void Update()
    {
        float drain = oxygenDrainRate * Time.deltaTime;

    if (sprintScript != null && sprintScript.isSprinting)
    {
        drain *= sprintDrainMultiplier;
    }

    currentOxygen -= drain;
    currentOxygen = Mathf.Clamp(currentOxygen, 0, maxOxygen);

    if (oxygenBar != null)
        oxygenBar.value = currentOxygen;

    if (O2Text != null)
    {
        O2Text.SetActive(currentOxygen <= 0);
    }
    }

    public void RefillOxygen(float amount)
    {
        currentOxygen = Mathf.Clamp(currentOxygen + amount, 0, maxOxygen);
        if (oxygenBar != null)
            oxygenBar.value = currentOxygen;
    }

    public void RefillOxygenFull()
    {
        currentOxygen = maxOxygen;

        if (oxygenBar != null)
            oxygenBar.value = currentOxygen;


    }

}
