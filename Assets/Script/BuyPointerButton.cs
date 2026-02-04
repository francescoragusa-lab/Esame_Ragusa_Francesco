using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyPointerButton : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Button button;
    [SerializeField] private TMP_Text buttonText;

    [Header("Costo")]
    [SerializeField] private int baseCost = 15;
    private int currentCost;

    [Header("Bonus")]
    [SerializeField] private float cpsGain = 0.4f;

    [Header("Riferimenti")]
    [SerializeField] private PointerManager pointerManager;

    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();

        currentCost = baseCost;
        UpdateButtonText();
    }

    void Update()
    {
        if (GameManager.Instance == null || button == null) return;
        button.interactable = GameManager.Instance.cookies >= currentCost;
    }

    public void Buy()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.TrySpendCookies(currentCost))
        {
            GameManager.Instance.AddCookiesPerSecond(cpsGain);

            if (pointerManager == null)
            {
                Debug.LogError("[BuyPointerButton] pointerManager NON assegnato in Inspector!");
            }
            else
            {
                pointerManager.AddPointer();
            }

            currentCost = Mathf.CeilToInt(currentCost * 1.15f);
            UpdateButtonText();
        }
    }

    private void UpdateButtonText()
    {
        if (buttonText != null)
            buttonText.text = $"+ {currentCost}";
    }
}
