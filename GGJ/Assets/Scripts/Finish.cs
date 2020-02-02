using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
	public GameObject sun;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D( Collision2D collision )
    {
    	var other = collision.gameObject;
    	
    	if ( other.tag == "Player" )
    	{
    		var body = other.GetComponent<Rigidbody2D>();
    		body.velocity = Vector2.zero;
    		body.angularVelocity = 0;
    		body.freezeRotation = true;
    		body.isKinematic = true;
    		body.simulated = false;

    		sun.GetComponent<Sun>().OnPlayerWin();
    	}
    }
}
