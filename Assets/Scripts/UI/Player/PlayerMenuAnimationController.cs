using System;
using UnityEngine;
using System.Collections;

public class PlayerMenuAnimationController : MonoBehaviour
{
    Animator player = null;

    public static event Action PlayerEntered = delegate { };

    private void Start()
    {
        if (!player)
            player = this.GetComponent<Animator>();

        StartCoroutine(PlayerEntry());
    }

    IEnumerator PlayerEntry()
    {
        yield return new WaitForSeconds(player.GetCurrentAnimatorStateInfo(0).length);

        player.SetTrigger("Idel");

        PlayerEntered?.Invoke();
    }
}