using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float timeOffset = 0.3f;
    public Rigidbody body;
    public float cameraDistance = 5f;
    public float floatDistance = 2f;
    
    private Vector3 velocity;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 position = player.transform.position - player.transform.forward * cameraDistance;
        position.y += floatDistance;
        
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, floatDistance, layerMask))
        {
            position.y = hit.transform.position.y + floatDistance;
        }

        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, timeOffset);
        transform.LookAt(player.transform);
    }
}