using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fill_Bar : MonoBehaviour
{
	[SerializeField] private Health target_health;
	[SerializeField] private Image bar_image;
	
    // Start is called before the first frame update
    private void Start()
    {
	    this.target_health.Health_Changed += this.On_Target_Health_Changed;
    }

    private void OnDestroy()
    {
	    this.target_health.Health_Changed -= this.On_Target_Health_Changed;
    }


    private void On_Target_Health_Changed(float new_health)
    {
	    Debug.Log(new_health);
	    Debug.Log(this.target_health.max_health);
	    this.bar_image.fillAmount = new_health / this.target_health.max_health;
    }
}
