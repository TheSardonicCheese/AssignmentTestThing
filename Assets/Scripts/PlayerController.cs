using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour 
{
	CharacterController controller;

	public float Speed = 6.0f;
	public float JumpSpeed = 8.0f;
	public float Gravity =40.0f;

	Vector3 MoveDirection = Vector3.zero;


	void Start () 
	{
		controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(controller.isGrounded)
		{
			MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			MoveDirection *= Speed;

			if(Input.GetButton("Jump"))
			{
				MoveDirection.y = JumpSpeed;
			}
		}
		else
		{
			MoveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			MoveDirection *= Speed;

			if(Input.GetButton("Jump"))
			{
				MoveDirection.y = JumpSpeed;
			}
		}

		MoveDirection.y -= Gravity * Time.deltaTime;

		controller.Move(MoveDirection * Time.deltaTime);

		Vector3 LookDir = new Vector3(MoveDirection.x, 0, MoveDirection.z);
		transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(MoveDirection), 0.15f);
	}
}
