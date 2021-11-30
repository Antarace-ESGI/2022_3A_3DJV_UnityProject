using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
    public Transform player;
    public GameObject camera;

    // Update is called once per frame
    void Update()
    {
        camera.transform.position = new Vector3(player.position.x, 
                                                player.position.y,
                                                player.position.z);
    }
}