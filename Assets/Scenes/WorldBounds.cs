using UnityEngine;

public class WorldBounds : MonoBehaviour
{
    public Transform target;           
    public Vector2 minBounds;          
    public Vector2 maxBounds;          

    private float camHalfHeight;
    private float camHalfWidth;

    void Start()
    {
        Camera cam = Camera.main;
        camHalfHeight = cam.orthographicSize;
        camHalfWidth = camHalfHeight * cam.aspect;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 newPos = target.position;

        newPos.x = Mathf.Clamp(newPos.x, minBounds.x + camHalfWidth, maxBounds.x - camHalfWidth);
        newPos.y = Mathf.Clamp(newPos.y, minBounds.y + camHalfHeight, maxBounds.y - camHalfHeight);
        newPos.z = transform.position.z; 

        transform.position = newPos;
    }
}
