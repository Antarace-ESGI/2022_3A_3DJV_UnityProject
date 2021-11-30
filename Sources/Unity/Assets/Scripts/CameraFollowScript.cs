using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject player;
    public Vector3 offset;
    public float timeOffset;
    public Rigidbody body;
    
    private Vector3 velocity;

    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Mathf.Cos(player.transform.rotation.y) * player.transform.position + offset, ref velocity, timeOffset);
        transform.LookAt(player.transform);
    }
}