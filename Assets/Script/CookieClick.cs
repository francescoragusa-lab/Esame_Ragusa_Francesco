using UnityEngine;

public class CookieClick : MonoBehaviour
{
    public int cookies = 0;

    void OnMouseDown()
    {
        cookies++;
        Debug.Log("Cookies (solo test) = " + cookies);
    }
}
