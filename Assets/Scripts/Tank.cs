using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This script is attached to the tank at the end of the level
/// It controls the tank's movement and animation for the cutscene at the end of the level
/// </summary>
/// Author: Max Schafer
/// Date: 2021-12-05
public class Tank : MonoBehaviour
{
    #region Public Variables
    public bool isMoving = false; // Is the tank moving?
    public AudioSource audioSource; //reference to the audio source of the trigger.
    public AudioClip hatchSound; //reference to the audio clip of the tank hatch sound.
    public delegate void Sequence_Done_Handler();
	public event Sequence_Done_Handler Sequence_Done;
    #endregion
   
    #region Private Variables
    private Animator animator; // Reference to the animator component that's on the child object.
    private ParticleSystem smokeTrail; //reference to the particle system of the tank.
    #endregion
   
    #region Methods
    /// <summary>
    /// Initialize the animator and particle system references.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        smokeTrail = GetComponent<ParticleSystem>();
    }

    /// <summary>
    /// check if the tank is supposed to be moving. if so, we move the tank to it's specified position.
    /// Once the tank is in position, we start the animation and play the hatch sound. and stop moving the tank.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    void Update()
    {
        //if the tank is supposed to be moving, we move the tank left until it's reached it's specified position.
        if(isMoving)
        {
            MoveLeft();
            // once the tank is in position, we start the animation and play the hatch sound. and stop moving the tank.
            if(transform.position.x <= 529)
            {
                isMoving = false;
                smokeTrail.Stop();
                audioSource.Stop();
                animator.SetTrigger("popUp");
                audioSource.PlayOneShot(hatchSound);
                //start final boss sequence here
                this.Sequence_Done?.Invoke();
            }
        }
    }
    /// <summary>
    /// transforms the tank to the left.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-05
    private void MoveLeft()
    {
        transform.position += Vector3.left * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
	    // TODO: Play explosion
	    Destroy(this.gameObject);
    }

    #endregion
}
