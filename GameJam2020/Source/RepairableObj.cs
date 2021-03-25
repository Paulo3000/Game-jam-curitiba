using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepairableObj : MonoBehaviour
{
    int numOfChildren;
    public int numOfRepairedPieces;
    public GameObject fullObj;
    public GameObject particleEmitter;
    // tells if should put full block in position
    public bool shouldReplace;
    public bool hasReplacedYet;

    private void Start()
    {
        fullObj = transform.Find("fullObj").gameObject;
        GameController.numOfBrokenObjs++;

        numOfChildren = transform.childCount - 1;
        numOfRepairedPieces = 0;
        shouldReplace = false;
        hasReplacedYet = false;

        for (int i = 0; i < transform.childCount; i++)
        {
            print("test");
            GameObject instParticleEmitter = Instantiate(particleEmitter, transform.GetChild(i));
        }
    }

    private void Update()
    {
        if (numOfRepairedPieces >= numOfChildren)
        {
            shouldReplace = true;
        }
        if (shouldReplace && !hasReplacedYet)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).name != "fullObj")
                {
                    Destroy(transform.GetChild(i).gameObject);
                }
            }
            fullObj.SetActive(true);
            GameController.numOfRepairedObjs++;
            shouldReplace = false;
            hasReplacedYet = true;
        }
    }
}
