using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour
{
    public float timer=15;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(HideSplash());
    }

   IEnumerator HideSplash()
    {
        yield return new WaitForSeconds(timer);

        SceneManager.LoadSceneAsync("Demo");
    }
}
