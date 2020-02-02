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
	public float chargeSpeed = 8;

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

			stateTimer = random.Next( 10 );

			if ( state == State.Charge )
			{
			    var toTarget = ( target.transform.position - transform.position );
			    chargeDir = toTarget.normalized;
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
			    body.velocity = chargeDir * chargeSpeed;
	    		break;
	    	}
	    }

        if ( body.velocity.magnitude > maxSpeed )
        {
        	body.velocity = body.velocity.normalized * maxSpeed;
        }

        animator.SetFloat( "AttackVelocity", body.velocity.magnitude );
    }
}
