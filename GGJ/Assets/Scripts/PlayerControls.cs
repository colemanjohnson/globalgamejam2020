using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerControls : MonoBehaviour
{
	public enum CauseOfDeath
	{
		Falling,
		Melting
	};

	public UnityEvent PlayerDeath;

	public float moveGroundForce = 1.0f;
	public float moveAirForce = 0.5f;

	public float maxGroundSpeed = 10.0f;
	public float maxAirSpeed = 5.0f;

	public float jumpSpeed = 10.0f;

	public float snowVolume = 1.0f;
	public float minSnowVolume = 1.0f;
	public float maxSnowVolume = 100.0f;
	public float snowAccumulationFactor = 0.001f;

	public void ChangeVolume( float change )
	{
		SetVolume( snowVolume + change );
	}

	public void Kill( CauseOfDeath causeOfDeath )
	{
		// Do something I guess
		PlayerDeath.Invoke();
	}

	void Die()
	{
		gameObject.SetActive( false );
	}

	void SetVolume( float volume )
	{
		snowVolume = Mathf.Min( volume, maxSnowVolume );

		if ( snowVolume < minSnowVolume )
		{
			Kill( CauseOfDeath.Melting );
			return;
		}

    	var transform = GetComponent<Transform>();

    	float scale = Mathf.Sqrt( snowVolume );
    	transform.localScale = new Vector2( scale, scale );

    	/*
    	var body = GetComponent<Rigidbody2D>();
    	body.mass = snowVolume;
    	*/
	}

    // Start is called before the first frame update
    void Awake()
    {
        SetVolume( snowVolume );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	float scale = transform.localScale.x;

    	float horInput = Input.GetAxis( "Horizontal" ) * scale;
    	float verInput = Input.GetAxis( "Vertical" ) * scale;

    	var collider = GetComponent<CircleCollider2D>();
    	float radius = collider.radius * scale;

    	var other = Physics2D.OverlapCircle( new Vector2( transform.position.x, transform.position.y - 0.15f ), radius - 0.1f, LayerMask.GetMask( "Terrain" ) );
    	bool onGround = other != null;

	    var body = GetComponent<Rigidbody2D>();
	    float horSpeed = Mathf.Abs( body.velocity.x );
    	if ( onGround )
    	{
	    	if ( body.velocity.magnitude < maxGroundSpeed )
	    		body.AddForce( new Vector2( horInput * moveGroundForce, 0 ) );

	    	ChangeVolume( horSpeed * snowAccumulationFactor );

	    	if ( verInput > 0 )
	    	{
	    		// jump
	    		body.velocity = new Vector2( body.velocity.x, jumpSpeed );
	    	}
	    }
	    else
	    {
	    	if ( horSpeed < maxAirSpeed )
	    		body.AddForce( new Vector2( horInput * moveAirForce, 0 ) );
	    }
    }

	private void LateUpdate()
	{
		if ( transform.position.y < -50 )
		{
			Kill( CauseOfDeath.Falling );
		}
	}
}
