using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HashPlayerAnimations : MonoBehaviour
{
    public readonly int Idle = Animator.StringToHash("isIdle");
    public readonly int Walk = Animator.StringToHash("isWalk");
}
