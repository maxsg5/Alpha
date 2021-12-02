using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    public float speed = 10f;
    public bool isMoving = false;
    private Animator animator; // Reference to the animator component that's on the child object.
    public AudioSource audioSource; //reference to the audio source of the trigger.

    public AudioClip hatchSound; //reference to the audio clip of the tank hatch sound.

    private ParticleSystem smokeTrail; //reference to the particle system of the tank.

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        smokeTrail = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMoving)
        {
            MoveLeft();
            if(transform.position.x <= 529)
            {
                isMoving = false;
                smokeTrail.Stop();
                audioSource.Stop();
                animator.SetTrigger("popUp");
                audioSource.PlayOneShot(hatchSound);
            }
        }

        
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * Time.deltaTime;
        
    }


}