using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerControls player;        //Public variable to store a reference to the player game object

    // Use this for initialization
    void Start()
    {
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        Camera.main.orthographicSize = Mathf.MoveTowards( Camera.main.orthographicSize, player.transform.localScale.x * 5, 0.05f * Time.deltaTime );
        transform.position = player.transform.position + new Vector3( 0, 0, transform.position.z );
    }
}
