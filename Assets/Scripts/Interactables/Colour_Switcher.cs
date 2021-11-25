using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Switches the game object's renderer between an "active" and "inactive" colour
/// </summary>
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

	/// <summary>
	/// Grabs the renderer from the object and sets its colour to "inactive"
	/// </summary>
	private void Start()
	{
		this.re = this.gameObject.GetComponent<Renderer>();
		this.Set_Material_Colour(this.inactive_colour);
	}

	/// <summary>
	/// Toggles the colour of the object's renderer between active and inactive
	/// </summary>
	public void Toggle_Colour()
	{
		if (this.re.material.color == this.active_colour) {
			this.Set_Material_Colour(this.inactive_colour);
		}
		else {
			this.Set_Material_Colour(this.active_colour);
		}
	}

	/// <summary>
	/// Sets the colour of the object's renderer based on whether it's
	/// "active" (true) or "inactive" (false)
	/// </summary>
	/// 
	/// <param name="active">
	/// True if the object should show it's
	/// "active" colour or false if the object should show it's
	/// "inactive" colour
	/// </param>
	public void Set_Colour(bool active)
	{
		this.Set_Material_Colour(active ? this.active_colour : this.inactive_colour);
	}

	/// <summary>
	/// Sets the colour of the object's renderer to the specified colour
	/// </summary>
	/// <param name="color">The colour to set the object's renderer to</param>
	private void Set_Material_Colour(Color color)
	{
		this.re.material.color = color;
	}
}
