using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	public float acceleration = 1;
	public float maxSpeed = 10;
	public float snowVolume = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	float hor = Input.GetAxis( "Horizontal" );
    	// float ver = Input.GetAxis( "Vertical" );

    	var collider = GetComponent<CircleCollider2D>();
    	var transform = GetComponent<Transform>();

    	var other = Physics2D.OverlapCircle( new Vector2( transform.position.x, transform.position.y - 0.1f ), collider.radius, LayerMask.GetMask( "Terrain" ) );
    	bool onGround = other != null;

    	if ( onGround )
    	{
	    	var body = GetComponent<Rigidbody2D>();
	    	if ( body.velocity.magnitude < maxSpeed )
	    		body.AddForce( new Vector2( hor * acceleration, 0 ) );
	    }
    }
}
