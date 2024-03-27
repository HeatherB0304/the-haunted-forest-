using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimCheatSheet : MonoBehaviour
{
    [SerializeField] public Animator gunAnimator;  //This is a public variable that hold our animator that we want to reference. In this case, it is the animator on a gun

    void Update()
    {
        DirectlyPlayingAnimation();
        //PlayAnimationByStateInt();

    }

    void DirectlyPlayingAnimation() //Directly calls on the animation by name
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunAnimator.Play("recoilAnim"); //here we are telling our animator to play recoilAnim. We could have just put "Recoil" in here instead of making a string but it is better this way in case you end up wanting to change the animation.
        }
    }

    void PlayAnimationByStateInt() //changes integer to play animation in animator
    {
        if (Input.GetMouseButtonDown(0))
        {
            gunAnimator.SetInteger("recoilState", 1);
        }
    }
}
