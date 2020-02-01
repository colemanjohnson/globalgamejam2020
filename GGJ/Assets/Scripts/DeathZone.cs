using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DeathZone : MonoBehaviour
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
        var player = collision.GetComponent<PlayerControls>();
        if ( player )
        {
            player.Kill( PlayerControls.CauseOfDeath.Falling );
        }

        // TODO: delete other objects
    }
}
