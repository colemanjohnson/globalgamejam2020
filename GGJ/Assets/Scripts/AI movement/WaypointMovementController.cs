using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointMovementController : MonoBehaviour
{
	public Waypoint[] waypoints;
	public bool circular;

	private float waitTimeRemaining = 0.0f;
	private float currentWaypointTime = 0.0f;

	private int currentWaypoint = 0;

	// Start is called before the first frame update
	void Start()
	{
		// snap to first waypoint
		transform.position = waypoints[ currentWaypoint ].transform.position;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if ( waitTimeRemaining > 0.0f )
		{
			waitTimeRemaining -= Time.fixedDeltaTime;
			return;
		}

		var cur = waypoints[ currentWaypoint ];
		var nextWaypoint = ( currentWaypoint + 1 ) % waypoints.Length;
		var next = waypoints[ nextWaypoint ];

		currentWaypointTime += Time.fixedDeltaTime;
		if ( currentWaypointTime > next.travelTime )
		{
			transform.position = next.transform.position;
			currentWaypoint = nextWaypoint;
			waitTimeRemaining = next.waitTime;
			currentWaypointTime = 0.0f;
		}
		else
		{
			var dir = next.transform.position - cur.transform.position;
			var angle = 90 + Mathf.Atan2( dir.y, dir.x ) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.AngleAxis( angle, Vector3.forward );
			transform.position = Vector3.Lerp( cur.transform.position, next.transform.position, currentWaypointTime / next.travelTime );
		}
	}
}
