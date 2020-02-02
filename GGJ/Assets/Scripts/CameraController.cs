using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerControls player;        //Public variable to store a reference to the player game object

    public float maxCameraDrift = 0.2f;

    // Use this for initialization
    void Start()
    {
    }

    // LateUpdate is called after Update each frame
    void LateUpdate()
    {
        if ( player )
        {
            Camera.main.orthographicSize = Mathf.MoveTowards( Camera.main.orthographicSize, player.transform.localScale.x * 5, 5 * Time.deltaTime );

            /*
            var rb = player.GetComponent<Rigidbody2D>();
            float speedPercent = rb.velocity.x / player.maxGroundSpeed;
            float viewportDrift = speedPercent * maxCameraDrift;
            Vector3 centerPosition = Camera.main.ViewportToWorldPoint( new Vector3( 0.5f, 0.0f, 0.0f ) );
            Vector3 position = Camera.main.ViewportToWorldPoint( new Vector3( 0.5f + viewportDrift, 0.0f, 0.0f ) );
            float xOffset = position.x - centerPosition.x;
            transform.position = player.transform.position + new Vector3( xOffset, 0, transform.position.z );
            */

            transform.position = player.transform.position + new Vector3( 0, 0, -10.0f );
        }
    }
}
