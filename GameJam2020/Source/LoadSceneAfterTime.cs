using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneAfterTime : MonoBehaviour
{
    public float t;
    public string sceneToLoad;

    private void Update()
    {
        t -= Time.deltaTime;
        if (t <= 0f)
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
