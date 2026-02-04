using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Stats")]
    public int cookies = 0;
    public float cookiesPerSecond = 0f;

    [Header("UI")]
    [SerializeField] private TMP_Text cookiesText;
    [SerializeField] private TMP_Text perSecondText;

    [Header("Win UI")]
    [SerializeField] private GameObject winPanel;

    private float cpsAccumulator = 0f;
    private bool hasWon = false;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    void Start()
    {
        RefreshUI();

        if (winPanel != null)
            winPanel.SetActive(false);
    }

    void Update()
    {
        if (hasWon) return;

       
        if (cookiesPerSecond > 0f)
        {
            cpsAccumulator += cookiesPerSecond * Time.deltaTime;

            if (cpsAccumulator >= 1f)
            {
                int whole = Mathf.FloorToInt(cpsAccumulator);
                cpsAccumulator -= whole;

                cookies += whole;
                RefreshUI();
            }
        }
    }

    public void AddCookies(int amount)
    {
        if (hasWon) return;

        cookies += amount;
        RefreshUI();
    }

    public bool TrySpendCookies(int amount)
    {
        if (hasWon) return false;
        if (cookies < amount) return false;

        cookies -= amount;
        RefreshUI();
        return true;
    }

    public void AddCookiesPerSecond(float amount)
    {
        if (hasWon) return;

        cookiesPerSecond += amount;
        RefreshUI();
    }

   
    public void TriggerWin()
    {
        if (hasWon) return;

        hasWon = true;

        if (winPanel != null)
            winPanel.SetActive(true);

        Debug.Log("YOU WIN!");
    }

    public void RefreshUI()
    {
        if (cookiesText != null)
            cookiesText.text = $"Cookies:\n{cookies}";

        if (perSecondText != null)
            perSecondText.text = $"Per second:\n{cookiesPerSecond:0.0}";
    }
}



