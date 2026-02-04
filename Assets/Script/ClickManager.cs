using UnityEngine;

public class ClickManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mouseWorldPos, Vector2.zero);

            if (hit.collider != null)
            {
                Debug.Log("Cliccato: " + hit.collider.name);

                if (hit.collider.CompareTag("Cookie"))
                {
                    GameManager.Instance.AddCookies(1);
                }
            }
        }
    }
}

