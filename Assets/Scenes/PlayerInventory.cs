using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    
    public int torchCount = 0;
    public int oxygenCount = 0;


    public TMP_Text torchCountText;
    public TMP_Text oxygenCountText;


    public Image torchIcon;
    public Image oxygenIcon;

    private PlayerTorch torchScript;
    private Oxygen oxygenScript;

    void Start()
    {
        torchScript = GetComponent<PlayerTorch>();
        oxygenScript = GetComponent<Oxygen>();
        UpdateUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && torchCount > 0 && torchScript != null)
        {
            torchScript.ActivateTorch();
            torchCount--;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.Q) && oxygenCount > 0 && oxygenScript != null)
        {
            oxygenScript.RefillOxygenFull(); 
            oxygenCount--;
            UpdateUI();
        }
    }

    public void AddTorch(int amount)
    {
        torchCount += amount;
        UpdateUI();
    }

    public void AddOxygen(int amount)
    {
        oxygenCount += amount;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (torchCountText != null)
            torchCountText.text = "x" + torchCount;

        if (oxygenCountText != null)
            oxygenCountText.text = "x" + oxygenCount;

        if (torchIcon != null)
            torchIcon.enabled = torchCount > 0;

        if (oxygenIcon != null)
            oxygenIcon.enabled = oxygenCount > 0;
    }
}