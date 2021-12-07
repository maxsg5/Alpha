using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    #region Delegates and Events
	public delegate void Sequence_Done_Handler(); // Delegate for the event
	public event Sequence_Done_Handler Sequence_Done; // Event
	#endregion

    #region Public Variables
	public float speed = 10f; // Speed of the tank
	public bool isMoving = false; // Is the tank moving?
    public AudioSource audioSource; //reference to the audio source of the trigger.
	public AudioClip hatchSound; //reference to the audio clip of the tank hatch sound.
    public GameObject explosion; //reference to the explosion prefab.
    public AudioSource tankExplosion; //reference to the tank explosion audio source.
    public AudioSource wilhelmScream; //reference to the wilhelm scream audio source.
    public AudioSource bossScream; //reference to the boss scream audio source.
    #endregion

    #region Private Variables
	private Animator animator; // Reference to the animator component that's on the child object.
	private ParticleSystem smokeTrail; //reference to the particle system of the tank.

    #endregion

    #region Methods

    /// <summary>
    /// initializes the animator and particle system. turns off the box collider at start
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-06
	void Start()
	{
		animator = GetComponentInChildren<Animator>();
		smokeTrail = GetComponent<ParticleSystem>();
        GetComponent<BoxCollider2D>().enabled = false;
	}

	/// <summary>
    /// if the tank is moving, it will move the tank left until it reaches a specified position.
    /// The box collider is also turned on.
    /// once the tank reaches the position, it will stop moving and trigger the boss sequence.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-06
	void Update()
	{
		if(isMoving)
		{
            GetComponent<BoxCollider2D>().enabled = true;
            MoveLeft();
			if(transform.position.x <= 529)
			{
				isMoving = false;
				smokeTrail.Stop();
				audioSource.Stop();
				animator.SetTrigger("popUp");
				audioSource.PlayOneShot(hatchSound);
                //play the boss scream sound after the hatch sound has finished.
                StartCoroutine(PlaySoundAfterDelay(hatchSound.length));
				//start final boss sequence here
				this.Sequence_Done?.Invoke();
			}
		}
	}

    /// <summary>
    /// Moves the tank left at the speed specified.
    /// </summary>
    /// Author: Max Schafer
	private void MoveLeft()
	{
		transform.position += Vector3.left * (Time.deltaTime * this.speed);
	}


    /// <summary>
    /// Called before the object is destroyed.
    /// Instantiate the explosion particles and play the explosion sound effect.
    /// </summary>
    /// Author: Max Schafer
    /// Date: 2021-12-06
    private void OnDestroy()
    {
        Instantiate(explosion, transform.position , transform.rotation);
        tankExplosion.Play();
        wilhelmScream.Play();
        
    }

    /// <summary>
    /// Plays the boss scream sound effect after a delay.
    /// </summary>
    /// <param name="time">float length of delay</param>
    /// Author: Max Schafer
    /// Date: 2021-12-06
    IEnumerator PlaySoundAfterDelay(float time)
    {
        yield return new WaitForSeconds(time);
        bossScream.Play();
    }

    #endregion
}