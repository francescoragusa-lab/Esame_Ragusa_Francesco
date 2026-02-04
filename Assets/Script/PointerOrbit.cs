using UnityEngine;

public class PointerOrbit : MonoBehaviour
{
    private Transform center;
    private float radius;
    private float angularSpeed;
    private float angle;

    private Vector3 fixedScale;

    [Header("Rotazione")]
    [SerializeField] private float rotationOffset = 0f;
    

    void Awake()
    {
        
        fixedScale = transform.localScale;
    }

    public void Init(Transform center, float radius, float rotationsPerSecond, float startAngleRadians)
    {
        this.center = center;
        this.radius = radius;
        this.angularSpeed = rotationsPerSecond * Mathf.PI * 2f;
        this.angle = startAngleRadians;

        UpdatePositionAndRotation();
    }

    void Update()
    {
        if (center == null) return;

        angle += angularSpeed * Time.deltaTime;
        UpdatePositionAndRotation();
    }

    private void UpdatePositionAndRotation()
    {
        
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        Vector2 pos = (Vector2)center.position + offset;
        transform.position = pos;

        
        Vector2 dirToCenter = (Vector2)center.position - pos;

        
        float zAngle = Mathf.Atan2(dirToCenter.y, dirToCenter.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, zAngle + rotationOffset);

        
        transform.localScale = fixedScale;
    }
}

