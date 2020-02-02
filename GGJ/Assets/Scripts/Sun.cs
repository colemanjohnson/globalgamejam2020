using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Animator animator;
	public GameObject target = null;
	public float acceleration = 0.1f;
	public float maxSpeed = 6;
	public float keepAwayDistance = 10.0f;
	public float keepAwaySpeedFactor = 0.25f;
	public float chargeAcceleration = 0.3f;
	public float chargeMaxSpeed = 16;

	private enum State
	{
		KeepAway,
		Attack,
		Charge
	}

	private State state = State.KeepAway;
	private float stateTimer = 5;

	private Vector2 chargeDir;

    // Start is called before the first frame update
    void Start()
    {
    	state = State.KeepAway;
    }

    void UpdateState()
    {
    	stateTimer -= Time.deltaTime;
    	if ( stateTimer < 0 )
    	{
    		var values = Enum.GetValues( typeof( State ) );
			var random = new System.Random();
			state = (State)values.GetValue( random.Next( values.Length ) );

			if ( state == State.Charge )
			{
			    var toTarget = ( target.transform.position - transform.position );
			    chargeDir = toTarget.normalized;

	    		var body = GetComponent<Rigidbody2D>();
	    		body.velocity = Vector2.zero;

	    		stateTimer = 3;
			}
			else
			{
				stateTimer = random.Next( 10 );
			}
    	}
    }

    // Update is called once per frame
    void Update()
    {
    	UpdateState();

	    var body = GetComponent<Rigidbody2D>();

	    var toTarget = ( target.transform.position - transform.position );
	    var dir = toTarget.normalized;

	    switch( state )
	    {
	    	case State.KeepAway:
	    	{
	        	float dist = toTarget.magnitude - keepAwayDistance;
	        	body.velocity += (Vector2)dir * dist * keepAwaySpeedFactor;
	    		break;
	    	}

	    	case State.Attack:
	    	{
	     		body.velocity += (Vector2)dir * acceleration;
	    		break;
	    	}

	    	case State.Charge:
	    	{
			    body.velocity += chargeDir * chargeAcceleration;
	    		break;
	    	}
	    }

    	float curMaxSpeed = maxSpeed;
    	if ( state == State.Charge )
    		curMaxSpeed = chargeMaxSpeed;
        if ( body.velocity.magnitude > curMaxSpeed )
        {
        	body.velocity = body.velocity.normalized * curMaxSpeed;
        }

        animator.SetFloat( "AttackVelocity", body.velocity.magnitude );
    }
}
