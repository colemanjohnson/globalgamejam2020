using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballBehavior : MonoBehaviour
{
    public float speed = 1.0f;
    public float despawnTime = 10.0f;

    private float initTime = 0.0f;

    private void Start()
    {
        initTime = Time.timeSinceLevelLoad;
    }
    private void Update()
    {
        if ( Time.timeSinceLevelLoad - initTime >= despawnTime )
        {
            Destroy( gameObject );
        }
    }

    public void FireAt( GameObject target )
    {
        var dir = target.transform.position - transform.position;
        var angle = 90 + Mathf.Atan2( dir.y, dir.x ) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis( angle, Vector3.forward );
        GetComponent<Rigidbody2D>().velocity = speed * dir.normalized;
        AkSoundEngine.PostEvent( "fireball", gameObject );
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        if ( collision.gameObject.layer == LayerMask.NameToLayer( "Terrain" ) )
        {
            Destroy( gameObject );
        }
    }
}
