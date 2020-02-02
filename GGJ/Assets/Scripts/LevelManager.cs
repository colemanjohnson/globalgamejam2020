using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void ReloadLevel()
    {
        StartCoroutine( DoReload() );
    }

    IEnumerator DoReload()
    {
        yield return new WaitForSeconds( 2.0f );
        SceneManager.LoadScene( SceneManager.GetActiveScene().name );
    }
}
