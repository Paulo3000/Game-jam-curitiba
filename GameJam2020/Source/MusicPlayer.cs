using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    public static bool hasSpawned = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (hasSpawned)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        hasSpawned = true;
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "SampleScene")
        {
            Destroy(gameObject);
        }
    }
}
