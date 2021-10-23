using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Colour_Switcher : MonoBehaviour
{
	public enum COLORS
	{
		PRIMARY,
		SECONDARY
	};

	private Renderer re;
	
	public Color active_colour = Color.red;
	public Color inactive_colour = Color.blue;

	private void Start()
	{
		this.re = this.gameObject.GetComponent<Renderer>();
	}

	public void Toggle_Colour()
	{
		if (this.re.material.color == this.active_colour) {
			this.Set_Material_Colour(this.inactive_colour);
		}
		else {
			this.Set_Material_Colour(this.active_colour);
		}
	}

	public void Set_Colour(bool active)
	{
		this.Set_Material_Colour(active ? this.active_colour : this.inactive_colour);
	}

	private void Set_Material_Colour(Color color)
	{
		this.re.material.color = color;
	}
}
