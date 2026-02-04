using UnityEngine;
using UnityEngine.UI;

public class WinButton : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int winCost = 400;

    void Awake()
    {
        if (button == null)
            button = GetComponent<Button>();
    }

    void Update()
    {
        if (GameManager.Instance == null) return;

        
        button.interactable = GameManager.Instance.cookies >= winCost;
    }

    
    public void Win()
    {
        if (GameManager.Instance == null) return;

        if (GameManager.Instance.cookies >= winCost)
        {
            GameManager.Instance.TriggerWin();
        }
    }
}
