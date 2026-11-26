using UnityEngine;

public class MantisAnimator : MonoBehaviour
{
    [SerializeField] BallLauncher launcher;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if(launcher.angle <= -120)
        {
            animator.SetBool("mouseIsOnLeft", true);
            animator.SetBool("mouseIsOnRight", false);
            animator.SetBool("mouseIsInMiddle", false);
            //Debug.Log("MouseIsOnLeft");
        }

        if (launcher.angle >= -120 && launcher.angle <= -80)
        {
            animator.SetBool("mouseIsInMiddle", true);
            animator.SetBool("mouseIsOnLeft", false);
            animator.SetBool("mouseIsOnRight", false);
            //Debug.Log("MouseIsInMiddle");
        }
        if (launcher.angle >= -80)
        {
            animator.SetBool("mouseIsOnRight", true);
            animator.SetBool("mouseIsOnLeft", false);
            animator.SetBool("mouseIsInMiddle", false);
            //Debug.Log("MouseIsOnRight");
        }
    }
}
