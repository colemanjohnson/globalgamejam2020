using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
	public GameObject target = null;
	public float acceleration = 0.1f;
	public float maxSpeed = 6;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var toTarget = ( target.transform.position - transform.position );
        var dir = toTarget.normalized;

        var body = GetComponent<Rigidbody2D>();
        body.velocity += (Vector2)dir * acceleration;

        if ( body.velocity.magnitude > maxSpeed )
        {
        	body.velocity = body.velocity.normalized * maxSpeed;
        }
    }
}
