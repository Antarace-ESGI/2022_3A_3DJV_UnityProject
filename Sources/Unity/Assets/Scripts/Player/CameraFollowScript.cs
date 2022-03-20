using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public GameObject player;
    public float timeOffset = 0.3f;
    public float cameraDistance = 5f;
    public float floatDistance = 2f;

    private Vector3 velocity;
    private float fovVelocity;

    void LateUpdate()
    {
        Vector3 position = player.transform.position - player.transform.forward * cameraDistance;
        position.y += floatDistance;

        transform.position = Vector3.SmoothDamp(transform.position, position, ref velocity, timeOffset);
        transform.LookAt(player.transform);
        
        // Field of view
        float fov = player.GetComponent<Rigidbody>().velocity.magnitude + 60f;
        Camera camera = GetComponent<Camera>();
        camera.fieldOfView = Mathf.SmoothDamp(camera.fieldOfView, fov, ref fovVelocity, timeOffset);
    }
}