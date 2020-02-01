using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireComponent : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D( Collider2D collision )
    {
        var player = collision.gameObject.GetComponent<PlayerControls>();
        if ( player )
        {
            player.ChangeVolume( -1 );
        }
    }

    private void OnTriggerStay2D( Collider2D collision )
    {
        var player = collision.gameObject.GetComponent<PlayerControls>();
        if ( player )
        {
            player.ChangeVolume( -1 );
        }
    }

    private void OnTriggerExit2D( Collider2D collision )
    {
        
    }
}
