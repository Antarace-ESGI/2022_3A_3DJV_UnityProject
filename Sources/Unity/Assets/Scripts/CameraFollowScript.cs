using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float timeOffset;
    public Rigidbody body;
    public float cameraDistance = 10.0f;
    
    private Vector3 velocity;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        Vector3 position = player.transform.position - player.transform.forward * cameraDistance;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, timeOffset);
        transform.LookAt(player.transform);
    }
}