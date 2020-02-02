using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
    public Animator animator;
	public GameObject target = null;
	public float acceleration = 0.1f;
	public float maxSpeed = 6;
	public float stateTime = 5;
	public float keepAwayDistance = 10.0f;
	public float keepAwaySpeedFactor = 0.25f;

	private bool attacking = false;
	private float stateTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
    	attacking = false;
    }

    void UpdateState()
    {
    	stateTimer += Time.deltaTime;
    	if ( stateTimer >= stateTime )
    	{
    		attacking = !attacking;
    		stateTimer = 0;
    	}
    }

    // Update is called once per frame
    void Update()
    {
    	UpdateState();

	    var body = GetComponent<Rigidbody2D>();

	    var toTarget = ( target.transform.position - transform.position );
	    var dir = toTarget.normalized;

        if ( attacking )
        {
	        body.velocity += (Vector2)dir * acceleration;
        }
        else
        {
        	float dist = toTarget.magnitude - keepAwayDistance;
        	body.velocity += (Vector2)dir * dist * keepAwaySpeedFactor;
        }

        if ( body.velocity.magnitude > maxSpeed )
        {
        	body.velocity = body.velocity.normalized * maxSpeed;
        }

        animator.SetFloat( "AttackVelocity", body.velocity.magnitude );
    }
}
