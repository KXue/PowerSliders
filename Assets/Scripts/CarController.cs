using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour {
	//public values
	public float m_AccelerationValue;
	public float m_MaxSpeed;
	public float m_TurningValue;
	//private values
	private float m_ThrustInput;
	private float m_TurnInput;
	private Rigidbody m_RigidBody;
	private GroundCheck m_GroundCheck;
	// Use this for initialization
	void Start () {
		m_RigidBody = GetComponent<Rigidbody>();
		m_GroundCheck = GetComponent<GroundCheck>();
	}
	
	// Update is called once per frame
	void Update () {
		m_ThrustInput = Input.GetAxis("Vertical");
		m_TurnInput = Input.GetAxis("Horizontal");
	}
	void ApplyThrust(){
		m_RigidBody.AddRelativeForce(new Vector3(0, 0, m_AccelerationValue * m_ThrustInput));
	}
	void ApplyTurning(){
		Vector3 forwardVelocity = transform.InverseTransformDirection(m_RigidBody.velocity);
		transform.Rotate(Vector3.up * m_TurnInput * m_TurningValue * forwardVelocity.z * Time.deltaTime);
	}
	void LimitVelocity(){
		Vector3 velocity = m_RigidBody.velocity;
		if(velocity.magnitude > m_MaxSpeed){
			velocity = velocity.normalized * m_MaxSpeed;
			m_RigidBody.velocity = velocity;
		}
	}
	/// <summary>
	/// This function is called every fixed framerate frame, if the MonoBehaviour is enabled.
	/// </summary>
	void FixedUpdate()
	{
		if(m_GroundCheck.IsGrounded){
			ApplyThrust();
			LimitVelocity();
			ApplyTurning();
		}
	}
}
