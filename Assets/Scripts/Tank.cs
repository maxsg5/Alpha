using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    #region Delegates and Events
	public delegate void Sequence_Done_Handler();
	public event Sequence_Done_Handler Sequence_Done;
	#endregion

    #region Public Variables
	public float speed = 10f;
	public bool isMoving = false;
    public AudioSource audioSource; //reference to the audio source of the trigger.
	public AudioClip hatchSound; //reference to the audio clip of the tank hatch sound.
    public GameObject explosion; //reference to the explosion prefab.
    public AudioSource tankExplosion; //reference to the tank explosion audio source.
    public AudioSource bossScream; //reference to the boss scream audio source.
    #endregion

    #region Private Variables
	private Animator animator; // Reference to the animator component that's on the child object.
	private ParticleSystem smokeTrail; //reference to the particle system of the tank.

    #endregion

    #region Methods

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
                //play the boss scream sound after the hatch sound has finished.
                StartCoroutine(PlaySoundAfterDelay(hatchSound.length));
				//start final boss sequence here
				this.Sequence_Done?.Invoke();
			}
		}
	}

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