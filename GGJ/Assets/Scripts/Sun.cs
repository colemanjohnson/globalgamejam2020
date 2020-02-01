using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sun : MonoBehaviour
{
	public GameObject target = null;
	public float speedFactor = 1;
	public float minSpeed = 3;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var toTarget = ( target.transform.position - transform.position );
        var dir = toTarget.normalized;
        var dist = toTarget.magnitude;

        float speed = Mathf.Max( dist * speedFactor, minSpeed ) * Time.deltaTime;

        transform.position = Vector3.MoveTowards( transform.position, target.transform.position, speed );
    }
}
