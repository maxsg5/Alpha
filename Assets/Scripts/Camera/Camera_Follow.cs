using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
	[SerializeField] private GameObject follow_cam_obj;
	private Camera follow_cam;
	[SerializeField] private GameObject target;
	
	[Header("Edges")]
	[SerializeField] private Transform right_edge;
	[SerializeField] private Transform left_edge;

	[Header("Bounding Box")]
	[SerializeField] private bool use_bounding_box;
	[Tooltip("A box surrounding what should be the visible the play space.")]
	[SerializeField] private BoxCollider2D bounding_box;

	private void Awake()
	{
		this.follow_cam = this.follow_cam_obj.GetComponent<Camera>();
	}

	private void LateUpdate()
	{
		this.Follow();
		if (this.use_bounding_box) {
			this.Stop_On_Bounding_Box();
		}
		else {
			this.Stop_On_Edge();
		}
	}

	private void Follow()
	{
		Vector3 target_pos = this.target.transform.position;
		Vector3 camera_pos = this.follow_cam_obj.transform.position;
		
		Vector3 new_pos = new Vector3(
			target_pos.x
			, target_pos.y
			, camera_pos.z
		);
		this.follow_cam_obj.transform.position = new_pos;
	}

	private void Stop_On_Bounding_Box()
	{
		Vector3 box_center = this.bounding_box.transform.TransformVector(this.bounding_box.offset);
		Vector3 box_size = this.bounding_box.size;
		float box_right_edge = box_center.x + box_size.x / 2;
		float box_left_edge = box_center.x + -box_size.x / 2;
		float box_top_edge = box_center.y + box_size.y / 2;
		float box_bottom_edge = box_center.y + -box_size.y / 2;
		
		Vector3 camera_pos = this.follow_cam_obj.transform.position;
		float camera_aspect_ratio = this.follow_cam.aspect;
		float camera_size = this.follow_cam.orthographicSize;
		float camera_top_edge = camera_pos.y + camera_size;
		float camera_bottom_edge = camera_pos.y + -camera_size;
		float camera_right_edge = camera_pos.x + camera_size * camera_aspect_ratio;
		float camera_left_edge = camera_pos.x + -camera_size * camera_aspect_ratio;

		Vector3 new_pos = new Vector3(camera_pos.x, camera_pos.y, camera_pos.z);
		if (camera_right_edge > box_right_edge) {
			new_pos.x = box_right_edge - (camera_right_edge - camera_pos.x);
		}
		else if (camera_left_edge < box_left_edge) {
			new_pos.x = box_left_edge - (camera_left_edge - camera_pos.x);
		}

		if (camera_top_edge > box_top_edge) {
			new_pos.y = box_top_edge - (camera_top_edge - camera_pos.y);
		}
		else if (camera_bottom_edge < box_bottom_edge) {
			new_pos.y = box_bottom_edge - (camera_bottom_edge - camera_pos.y);
		}

		this.follow_cam_obj.transform.position = new_pos;
	}

	private void Stop_On_Edge()
	{
		return;
	}
}
