using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
	public float moveForce = 1.0f;
	public float maxSpeed = 10.0f;
	public float snowVolume = 1.0f;
	public float minSnowVolume = 1.0f;
	public float maxSnowVolume = 100.0f;
	public float snowAccumulationFactor = 0.001f;

	void SetVolume( float volume )
	{
		snowVolume = Mathf.Clamp( volume, minSnowVolume, maxSnowVolume );
    	var transform = GetComponent<Transform>();

    	float scale = Mathf.Sqrt( volume );
    	transform.localScale = new Vector2( scale, scale );
	}

    // Start is called before the first frame update
    void Start()
    {
        SetVolume( snowVolume );
    }

    // Update is called once per frame
    void Update()
    {
    	float hor = Input.GetAxis( "Horizontal" );
    	// float ver = Input.GetAxis( "Vertical" );

    	var collider = GetComponent<CircleCollider2D>();
    	var transform = GetComponent<Transform>();

    	var other = Physics2D.OverlapCircle( new Vector2( transform.position.x, transform.position.y - 0.1f ), collider.radius * transform.localScale.x, LayerMask.GetMask( "Terrain" ) );
    	bool onGround = other != null;

    	if ( onGround )
    	{
	    	var body = GetComponent<Rigidbody2D>();
	    	if ( body.velocity.magnitude < maxSpeed )
	    		body.AddForce( new Vector2( hor * moveForce, 0 ) );

	    	SetVolume( snowVolume + Mathf.Abs( body.velocity.x ) * snowAccumulationFactor );
	    }
    }
}
