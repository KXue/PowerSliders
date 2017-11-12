using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour {
	public float m_Clearance;
	public BoxCollider m_ColliderBody;
	public bool IsGrounded{
		get{
			return m_isGrounded;
		}
	}
	private bool m_isGrounded = false;
	private Vector3 m_BoxCastCentre;
	private Vector3 m_HalfExtents;
	// Use this for initialization
	void Start () {
		Vector3 scaledSize = Vector3.Scale(m_ColliderBody.size, m_ColliderBody.transform.localScale);

		m_BoxCastCentre = m_ColliderBody.center;
		m_BoxCastCentre.y -= scaledSize.y * 0.5f;

		m_HalfExtents = scaledSize;
		m_HalfExtents.y = m_Clearance;
	}
	
	// Update is called once per frame
	void Update () {
	}
	void FixedUpdate()
	{
		m_isGrounded = Physics.BoxCast(transform.position + m_BoxCastCentre, m_HalfExtents, Vector3.up);
	}
	/// <summary>
	/// Callback to draw gizmos only if the object is selected.
	/// </summary>
	void OnDrawGizmos()
	{
		if(m_isGrounded){
			Gizmos.color = new Color(1, 1, 1, 1.0F);

		}
		else{
			Gizmos.color = new Color(0, 0, 0, 1.0F);
		}
        Gizmos.DrawCube(transform.position + m_BoxCastCentre, m_HalfExtents);
	}
}
