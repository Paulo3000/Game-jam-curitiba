using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScriptEnabler : MonoBehaviour
{
    public Animator myAnimator;

    void OnEnable()
    {
        myAnimator.SetBool("shouldStrike", false);
    }
}
