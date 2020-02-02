using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteoriteBehavior : MonoBehaviour
{
    public Animator animator;
    public FireballBehavior fireball;
    public float activationRadius = 6.0f;
    public float fireballShotDelay = 2.0f;
    public float despawnAfterShotDelay = 0.5f;

    private GameObject target = null;
    private float delayRemaining = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<CircleCollider2D>().radius = activationRadius;
        delayRemaining = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if ( delayRemaining > 0.0f )
        {
            delayRemaining -= Time.deltaTime;
            if ( delayRemaining <= 0.0f )
            {
                // fire fireball
                var newFireball = Instantiate( fireball );
                newFireball.transform.position = transform.position;
                newFireball.FireAt( target );
                Destroy( gameObject, despawnAfterShotDelay );
            }
        }
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( collision.tag == "Player" )
        {
            target = collision.gameObject;
            animator.SetTrigger( "Activated" );
            delayRemaining = fireballShotDelay;
        }
    }
}
