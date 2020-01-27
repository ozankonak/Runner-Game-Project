using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;

    private string idleHash = "Idle";
    private string jumpHash = "Jump";
    private string slideHash = "Slide";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        anim.SetBool(idleHash,true);
    }

    public void PlayJumpAnimation()
    {
        anim.SetTrigger(jumpHash);
    }

    public void PlaySlideAnimation()
    {
        anim.SetTrigger(slideHash);
    }

    public void SetIdleAnimation(bool value)
    {
        anim.SetBool(idleHash,value);
    }

}
