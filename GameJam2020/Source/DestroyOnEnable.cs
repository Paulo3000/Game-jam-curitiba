using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnEnable : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject);
    }
}
