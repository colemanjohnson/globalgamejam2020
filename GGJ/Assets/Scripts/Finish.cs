using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
	public GameObject sun;
	public float minPlayerScale = 1;
	public float maxPlayerScale = 100;
	public float maxAngleDiffFromUp = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollision2D( Collision2D collision )
    {
    	var other = collision.gameObject;
    	
    	if ( other.tag == "Player" )
    	{
    		float scale = other.transform.localScale.x;
    		if ( scale < minPlayerScale || scale > maxPlayerScale )
    			return;

    		var toPlayer = other.transform.position - transform.position;
    		if ( Vector3.Angle( new Vector3( 0, 1 ), toPlayer ) > maxAngleDiffFromUp )
    			return;

    		var body = other.GetComponent<Rigidbody2D>();
    		body.velocity = Vector2.zero;
    		body.angularVelocity = 0;
    		body.freezeRotation = true;
    		body.isKinematic = true;
    		body.simulated = false;

    		sun.GetComponent<Sun>().OnPlayerWin();
    	}
    }

    void OnCollisionEnter2D( Collision2D c ) { OnCollision2D( c ); }
    void OnCollisionStay2D( Collision2D c ) { OnCollision2D( c ); }
}
