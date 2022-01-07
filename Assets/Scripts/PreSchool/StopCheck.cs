using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopCheck : MonoBehaviour
{
    public ButtonManager manager;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //manager.anim.enabled = false;
            //manager.anim.SetBool("isWalking", false);
        }
    }
}
