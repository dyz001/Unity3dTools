using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMouseOper : MonoBehaviour {
	public float distance = 100;
	public float angle = 1.8f;
    public float moveSpeed = 10;
    public float rotateSpeed = 20;

	protected Vector3 m_position;
	protected Vector3 m_angles;
    protected Quaternion m_quaternion;

	protected bool m_bMove;
	protected bool m_bRotate;

    protected Vector3 m_posTarget;
    protected Vector3 m_dirTarget;

    protected Vector3 m_mouseLastPos;
    protected Vector3 m_curAngleVector;

    protected float m_curAngle;

	// Use this for initialization
	void Start () {
        m_posTarget = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width >> 1, Screen.height >> 1, 0));
        m_dirTarget = Vector3.up * 100;
        m_position = transform.position;
        m_angles = transform.rotation.eulerAngles;
        m_bRotate = false;
        m_bMove = false;
        m_curAngleVector = GetVector(distance * Mathf.Sin(angle), 0);
        transform.position =m_posTarget + new Vector3(m_curAngleVector.x, distance * Mathf.Sin(angle), m_curAngleVector.z);
        m_mouseLastPos = Input.mousePosition;
        m_quaternion = transform.rotation;
        //transform.position = GetStartPos(distance, angle);
        transform.LookAt(m_posTarget);
        m_curAngle = Vector3.AngleBetween(Vector3.right, m_curAngleVector);
	}

    Vector3 GetStartPos(float dis, float angle)
    {
        return new Vector3(distance * Mathf.Sin(angle) + m_posTarget.x, distance * Mathf.Cos(angle) + m_posTarget.y,
            m_posTarget.z);
    }

	// Update is called once per frame
	void Update () {
        //m_posTarget = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width >> 1, Screen.height >> 1, 0));
        //m_dirTarget = Vector3.up * 100;
        if (Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            m_mouseLastPos = Input.mousePosition;
        }
        if (Input.GetMouseButton(1))
        {
            m_bMove = true;
        }
        else if (Input.GetMouseButton(2))
        {
            m_bRotate = true;
        }
        else
        {
            return;
        }
        Vector3 deltaPos = Input.mousePosition - m_mouseLastPos;
        if(m_bMove)
        {
            Vector3 forward = new Vector3(transform.forward.x, 0, transform.forward.z);
            Vector3 right = new Vector3(transform.right.x, 0, transform.right.z);
            float tmp = moveSpeed * Time.deltaTime;
            if (deltaPos.x > float.Epsilon || deltaPos.x < -float.Epsilon)
            {
                m_posTarget += new Vector3(right.x * tmp, 0, right.z * tmp) * Mathf.Sign(deltaPos.x);
                m_position += new Vector3(right.x * tmp, 0, right.z * tmp) * Mathf.Sign(deltaPos.x);
            }
            if(deltaPos.y > float.Epsilon || deltaPos.y < -float.Epsilon)
            {
                m_posTarget += new Vector3(forward.x * tmp, 0, forward.z * tmp) * Mathf.Sign(deltaPos.y);
                m_position += new Vector3(forward.x * tmp, 0, forward.z * tmp) * Mathf.Sign(deltaPos.y);
            }
        }

        if (m_bRotate)
        {
            float dis = distance * Mathf.Sin(angle);
            float delta = deltaPos.x;
            m_curAngle += delta * rotateSpeed * Time.deltaTime;
            while (m_curAngle > 2 * Mathf.PI)
            {
                m_curAngle -= 2 * Mathf.PI;
            }
            m_curAngleVector = GetVector(dis, m_curAngle);
            m_position = m_posTarget + new Vector3(m_curAngleVector.x, distance * Mathf.Sin(angle), m_curAngleVector.z);
        }

        m_mouseLastPos = Input.mousePosition;
        m_bRotate = false;
        m_bMove = false;
	}

    Vector3 GetVector(float dis, float angle)
    {
        return new Vector3(dis * Mathf.Cos(angle), 0, dis * Mathf.Sin(angle));
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(m_posTarget, m_dirTarget + m_posTarget);
        Gizmos.DrawLine(m_posTarget, Vector3.left * 100 + m_posTarget);
    }

	void FixedUpdate(){
        transform.position = m_position;
        transform.LookAt(m_posTarget);
	}
}
