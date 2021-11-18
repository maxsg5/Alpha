using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class DoorSwitch : MonoBehaviour
{
    private Animator animator;

    //public Switch switch_object;

    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        
    }


    public void OpenDoor()
    {
        
        animator.SetBool("On", true);
    }
    
}
