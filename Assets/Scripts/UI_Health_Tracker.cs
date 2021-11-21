using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health_Tracker : MonoBehaviour
{
	private const string hp_suffix = " HP";
	private const string hp_current_max_separator = " / ";
	
	[SerializeField] private Health target_health;
	[SerializeField] private Image bar_image;
	[SerializeField] private TextMeshProUGUI hp_text;
	[SerializeField] private bool hp_as_percent = false;
	
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
	    this.bar_image.fillAmount = new_health / this.target_health.max_health;

	    this.hp_text.text = this.Build_HP_String(new_health);
    }

    private string Build_HP_String(float current_hp)
    {
	    string hp_string = "";
	    
	    if (this.hp_as_percent) {
		    hp_string += Mathf.Floor(current_hp / this.target_health.max_health * 100);
		    hp_string += " %";
	    }
	    else {
		    hp_string += current_hp;
		    hp_string += UI_Health_Tracker.hp_current_max_separator;
		    hp_string += this.target_health.max_health;
		    hp_string += UI_Health_Tracker.hp_suffix;
	    }

	    return hp_string;
    }
}
