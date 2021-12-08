// Author: Declan Simkins

using UnityEngine;

namespace Utilities
{
	/// <summary>
	/// Mouse related utility functions
	/// </summary>
	public static class Mouse
	{
		static Camera main_camera = Camera.main;
	
		/// <summary>
		/// Gets the angle from a position to the mouse's
		/// world space coordinates
		/// </summary>
		/// 
		/// <param name="from">
		/// Position to calculate the angle from
		/// </param>
		/// 
		/// <returns>
		/// The angle from the `from` position to the mouse's world
		/// space coordinates
		/// </returns>
		public static float Angle_To_Mouse(Vector3 from)
		{
			Vector3 mouse_pos = Input.mousePosition;
			mouse_pos.z = 0;
		
			Vector3 to = Mouse.main_camera.ScreenToWorldPoint(mouse_pos);
			to.z = 0;
		
			Vector3 aim_direction = to - from;
			return Mathf.Atan2(aim_direction.y, aim_direction.x) * Mathf.Rad2Deg;
		}
	}
}
