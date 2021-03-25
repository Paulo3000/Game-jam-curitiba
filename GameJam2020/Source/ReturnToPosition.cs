using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
public class ReturnToPosition : MonoBehaviour
{
    Vector3 startPosition;
    Quaternion startRotation;
    AudioSource audioSource;

    GameObject parentObj;
    RepairableObj repairableObj;

    [HideInInspector]
    public bool shouldReturn;
    [HideInInspector]
    public bool isRepaired;

    private float speed = 5f;
    private float timer = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = 2f;
        parentObj = transform.parent.gameObject;
        repairableObj = parentObj.GetComponent<RepairableObj>();
        shouldReturn = false;
        startPosition = transform.position;
        startRotation = transform.rotation;
        isRepaired = false;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (shouldReturn)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, startPosition, step);
            transform.rotation = Quaternion.Lerp(transform.rotation, startRotation, Time.time * speed);
        }

        if (Vector3.Distance(transform.position, startPosition) < .05f && !isRepaired && timer <= 0f)
        {
            isRepaired = true;
            repairableObj.numOfRepairedPieces++;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.transform.tag == "Weapon")
        {
            if (!shouldReturn)
            {
                audioSource.Play();
                shouldReturn = true;
                GetComponent<MeshCollider>().enabled = false;
                GetComponent<Rigidbody>().isKinematic = true;
            }
        }
    }
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.transform.tag == "Player")
    //    {
    //        if (!shouldReturn)
    //        {
    //            shouldReturn = true;
    //            GetComponent<MeshCollider>().enabled = false;
    //            GetComponent<Rigidbody>().isKinematic = true;
    //        }
    //    }
    //}
}
