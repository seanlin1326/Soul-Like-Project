using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class ResetInteracting : StateMachineBehaviour
    {
        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            animator.SetBool("IsInteracting", false);
        }
    }
}
