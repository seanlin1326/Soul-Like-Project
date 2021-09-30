using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace SoulLike
{
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator animator;
        // Start is called before the first frame update
        void Start()
        {
            inputHandler = GetComponent<InputHandler>();
            animator = GetComponentInChildren<Animator>();
        }

        // Update is called once per frame
        void Update()
        {

            inputHandler.isInteracting = animator.GetBool("IsInteracting");
            inputHandler.rollFlag = false;
            inputHandler.sprintFlag = false;
        }
    }
}
