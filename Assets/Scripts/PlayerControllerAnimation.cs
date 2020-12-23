using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerAnimation : MonoBehaviour
{
    public GameObject player;
    Animator anim;
    int helloHash = Animator.StringToHash("isHello");
    int listenningHash = Animator.StringToHash("isDance");

    // Start is called before the first frame update
    void Start()
    {
        anim = player.GetComponent<Animator>();
    }

    public bool ValidarEstaOuvindo()
    {
        return !anim.GetBool("isWalk") && !anim.GetBool("isTalk");
    }

    public void StartWalking()
    {
        anim.SetBool("isWalk", true);
    }

    public void StopWalking()
    {
        anim.SetBool("isWalk", false);
    }

    public void StartTalking()
    {
        anim.SetBool("isTalk", true);
    }

    public void StopTalking()
    {
        anim.SetBool("isTalk", false);
    }

    public void StartTalkingHello()
    {
        anim.SetTrigger("isHello");
    }

    public void StartDance()
    {
        anim.SetTrigger("isDance");
    }
}
