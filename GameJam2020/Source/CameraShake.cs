using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeTimer = 0f;

    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    Vector3 originalPos;

    void Awake()
    {
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shakeTimer > 0)
        {
            if (Time.timeScale > 0f)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                shakeTimer -= Time.deltaTime * decreaseFactor;
            }
        }
        else
        {
            this.enabled = false;
            shakeTimer = 0;
            camTransform.localPosition = originalPos;
        }
    }

    public void CamShake(float myShakeTimer, float myShakeAmount)
    {
        shakeTimer = myShakeTimer;
        shakeAmount = myShakeAmount;
        enabled = true;
    }
}