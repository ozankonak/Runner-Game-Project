using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;

    private string jumpHash = "Jump";
    private string slideHash = "Slide";

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayJumpAnimation()
    {
        anim.SetTrigger(jumpHash);
    }

    public void PlaySlideAnimation()
    {
        anim.SetTrigger(slideHash);
    }

}
