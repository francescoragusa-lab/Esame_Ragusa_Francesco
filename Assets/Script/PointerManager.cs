using System.Collections.Generic;
using UnityEngine;

public class PointerManager : MonoBehaviour
{
    [Header("Riferimenti")]
    [SerializeField] private Transform cookieCenter;        
    [SerializeField] private GameObject pointerPrefab;      

    [Header("Orbita")]
    [SerializeField] private float radius = 1.5f;
    [SerializeField] private float rotationsPerSecond = 0.2f; 

    [Header("Visual")]
    [SerializeField] private int extraSortingOrder = 5; 

    private readonly List<PointerOrbit> pointers = new();

    public void AddPointer()
    {
        if (cookieCenter == null)
        {
            Debug.LogError("[PointerManager] cookieCenter NON assegnato!");
            return;
        }
        if (pointerPrefab == null)
        {
            Debug.LogError("[PointerManager] pointerPrefab NON assegnato!");
            return;
        }

        
        GameObject go = Instantiate(pointerPrefab, transform);
        go.name = "Pointer_" + pointers.Count;

        
        go.transform.position = cookieCenter.position + Vector3.up * radius;

        
        var pointerSR = go.GetComponent<SpriteRenderer>();
        var cookieSR = cookieCenter.GetComponent<SpriteRenderer>();

        if (pointerSR == null)
            Debug.LogWarning("[PointerManager] Il prefab Pointer non ha SpriteRenderer (quindi non si vede).");

        if (pointerSR != null && cookieSR != null)
        {
            pointerSR.sortingLayerID = cookieSR.sortingLayerID;
            pointerSR.sortingOrder = cookieSR.sortingOrder + extraSortingOrder;
        }
        else if (pointerSR != null && cookieSR == null)
        {
            
            pointerSR.sortingOrder = 100;
        }

        
        var orbit = go.GetComponent<PointerOrbit>();
        if (orbit == null) orbit = go.AddComponent<PointerOrbit>();

        pointers.Add(orbit);

        Debug.Log("[PointerManager] Spawnato: " + go.name);

        RepositionAll();
    }

    private void RepositionAll()
    {
        int count = pointers.Count;
        if (count == 0) return;

        for (int i = 0; i < count; i++)
        {
            float startAngle = (Mathf.PI * 2f) * (i / (float)count);
            pointers[i].Init(cookieCenter, radius, rotationsPerSecond, startAngle);
        }
    }
}


