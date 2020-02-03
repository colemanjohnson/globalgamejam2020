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

	private bool onGround;

	private bool alive = true;

	public void ChangeVolume( float change )
	{
		SetVolume( snowVolume + change );
	}

	public void Kill( CauseOfDeath causeOfDeath )
	{
		if ( !alive )
			return;

		alive = false;

		if ( causeOfDeath == CauseOfDeath.Falling )
			transform.localScale = new Vector2( 1.0f, 1.0f );

		// Do something I guess
		PlayerDeath.Invoke();
	}

	void Die()
	{
		gameObject.SetActive( false );
	}

	void SetVolume( float volume )
	{
		if ( !alive )
			return;

		snowVolume = Mathf.Min( volume, maxSnowVolume );

		AkSoundEngine.SetRTPCValue( "snowball_size", snowVolume );

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
		alive = true;
        SetVolume( snowVolume );
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	float scale = transform.localScale.x;

    	float horInput = Input.GetAxis( "Horizontal" ) * scale;
    	float verInput = Input.GetAxis( "Vertical" );
    	bool jump = Input.GetButton( "Jump" );
    	bool restart = Input.GetButton( "Submit" );
    	bool quit = Input.GetButton( "Cancel" );

    	if ( quit )
    	{
    		Application.Quit();
    	}

    	if ( restart )
    	{
    		SceneManager.LoadScene( "Final_Level" );
    		return;
    	}

    	var collider = GetComponent<CircleCollider2D>();
    	float radius = collider.radius * scale;

    	var other = Physics2D.OverlapCircle( new Vector2( transform.position.x, transform.position.y - 0.15f ), radius - 0.1f, LayerMask.GetMask( "Terrain" ) );
    	bool nowOnGround = other != null;

		if ( onGround != nowOnGround )
		{
			onGround = nowOnGround;
			if ( nowOnGround )
				AkSoundEngine.PostEvent( "snowball_impact", gameObject );
			AkSoundEngine.PostEvent( nowOnGround ? "snowball_roll_start" : "snowball_roll_stop", gameObject );
		}

	    var body = GetComponent<Rigidbody2D>();
	    float horSpeed = Mathf.Abs( body.velocity.x );
    	if ( onGround )
    	{
	    	if ( body.velocity.magnitude < maxGroundSpeed )
	    		body.AddForce( new Vector2( horInput * moveGroundForce, 0 ) );

	    	ChangeVolume( horSpeed * snowAccumulationFactor );

	    	if ( jump || verInput > 0 )
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

		horSpeed = Mathf.Abs( body.velocity.x );
		AkSoundEngine.SetRTPCValue( "snowball_speed", horSpeed );
	}
}
