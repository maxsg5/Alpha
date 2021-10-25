using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Will contain the Boomer class
/// </summary>
/// 
/// Author: Josh Coss   (JC)
public class Boomer : Enemy
{
	public Boomer(Transform transform, Rigidbody2D rigidbody, Weapon weapon, PathMove movement, Health health)
		: base(transform, rigidbody, weapon, movement, health) { }

	public override void Attack()
	{
		throw new System.NotImplementedException();
	}

	public override void MoveForward()
	{
		throw new System.NotImplementedException();
	}
}
