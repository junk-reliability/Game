using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsOpen : MonoBehaviour
{
    Animator animator;
    
    bool DOp;
    void Start()
    {
        DOp = false;
        animator = GetComponent<Animator>();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E) && DOp == false)
        {
                DOp = true;
                Doors("Open");
        }   
    }
    void Doors (string dir)
    {
        animator.SetTrigger(dir);
    }
}
