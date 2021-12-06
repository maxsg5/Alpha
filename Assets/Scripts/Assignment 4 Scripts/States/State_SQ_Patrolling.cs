// Author: Declan Simkins

using UnityEngine;

using Controllers;
using Motors;

namespace States
{
	public class State_SQ_Patrolling : State_SQ
	{
		private Transform next_control_point;
		private int next_control_point_i;
		private float patrol_point_range2;

		public State_SQ_Patrolling(State prev_state, Controller_SQ controller, Motor_SQ sq_motor)
			: base(prev_state, controller, sq_motor)
		{
			this.next_control_point_i = 0;
			this.Set_Control_Point(this.next_control_point_i);
			this.patrol_point_range2 = this.controller.Patrol_Point_Range * this.controller.Patrol_Point_Range;
		}
		
		public override void Enter()
		{
			return;
		}

		/// <summary>
		/// Moves the spider queen towards the next control point, updating
		/// what is considered as the "next" control point when within
		/// the distance specified by the controller.
		/// If the controller's target can be seen, transition to the
		/// `Tracking` state. 
		/// </summary>
		public override void Tick()
		{
			Vector3 to_next_control_point = this.next_control_point.position - this.controller.transform.position;
			if (to_next_control_point.sqrMagnitude <= this.patrol_point_range2) {
				this.Set_Control_Point(this.next_control_point_i + 1);
			}
			
			this.sq_motor.MoveForward(this.next_control_point.position, this.controller.Speed);

			if (this.controller.Spider_Eye.CanSee(this.controller.Target)) {
				this.controller.Behaviour_State = this.controller.State_Tracking;
			}
		}

		/// <summary>
		/// Stops the walking animation via the motor.
		/// </summary>
		public override void Exit()
		{
			this.sq_motor.Stop_Moving();
		}

		/// <summary>
		/// Updates the current control point, treating the list of
		/// control points as circular.
		/// </summary>
		/// <param name="control_point_i">
		/// Index of what should be the new "next" control point.
		/// </param>
		private void Set_Control_Point(int control_point_i)
		{
			this.next_control_point_i = control_point_i % this.controller.Control_Points.Length;
			this.next_control_point = this.controller.Control_Points[this.next_control_point_i];
		}
	}
}
