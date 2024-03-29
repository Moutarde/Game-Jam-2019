﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    enum Direction
    {
        Left,
        Right,
        Bottom,
        Top
    }

    public float m_speed;
    public float m_directionChangeMinDelay;
    public float m_directionChangeMaxDelay;

    private bool m_moving;

    private Vector3 m_directionVector;

    private float m_directionChangeDelay;
    private float m_nextDirectionChangeTime;

    // Start is called before the first frame update
    void Start()
    {
        int index = Random.Range(0, 4);
        switch (index)
        {
            case 0:
                m_directionVector = new Vector3(-1.0f, 0.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirLeft");
                break;

            case 1:
                m_directionVector = new Vector3(1.0f, 0.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirRight");
                break;

            case 2:
                m_directionVector = new Vector3(0.0f, -1.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirRight");
                break;

            case 3:
                m_directionVector = new Vector3(0.0f, 1.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirRight");
                break;
        }

        m_directionChangeDelay = Random.Range(m_directionChangeMinDelay, m_directionChangeMaxDelay);
        m_nextDirectionChangeTime = Time.time + m_directionChangeDelay;
        m_moving = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!m_moving)
            return;

        bool wasInRightDirection = true;
        if (m_directionVector.x < 0.0f)
        {
            wasInRightDirection = false;
        }

        float xOffset = Random.Range(-0.2f, 0.2f);
        float yOffset = Random.Range(-0.2f, 0.2f);
        m_directionVector = new Vector3(m_directionVector.x + xOffset, m_directionVector.y + yOffset, 0.0f);
        m_directionVector = Vector3.Normalize(m_directionVector);

        transform.localPosition = transform.localPosition + m_directionVector * m_speed * Time.deltaTime;

        bool rightDirection = true;
        if (m_directionVector.x < 0.0f)
        {
            rightDirection = false;
        }

        if (!wasInRightDirection && rightDirection)
        {
            GetComponent<Animator>().Play("SetDirRight");
        }
        else if (wasInRightDirection && !rightDirection)
        {
            GetComponent<Animator>().Play("SetDirLeft");
        }
        GetComponent<Animator>().Play("Walk");
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!m_moving)
            return;

        string tag = collision.gameObject.tag;

        if (tag.Equals("LeftWall"))
        {
            m_directionVector = new Vector3(1.0f, 0.0f, 0.0f);
            GetComponent<Animator>().Play("SetDirRight");
        }
        else if (tag.Equals("RightWall"))
        {
            m_directionVector = new Vector3(-1.0f, 0.0f, 0.0f);
            GetComponent<Animator>().Play("SetDirLeft");
        }
        else if (tag.Equals("TopWall"))
        {
            m_directionVector = new Vector3(0.0f, -1.0f, 0.0f);
            GetComponent<Animator>().Play("SetDirRight");
        }
        else if (tag.Equals("BottomWall"))
        {
            m_directionVector = new Vector3(0.0f, 1.0f, 0.0f);
            GetComponent<Animator>().Play("SetDirRight");
        }
        else
        {
            Vector2 colliderOffset = GetComponent<BoxCollider2D>().offset;
            Vector3 colliderPosition = transform.localPosition + new Vector3(colliderOffset.x, colliderOffset.y, 0.0f);

            GameObject other = collision.gameObject;
            Vector2 otherColliderOffset = other.GetComponent<BoxCollider2D>().offset;
            Vector3 otherColliderPosition = other.transform.localPosition + new Vector3(otherColliderOffset.x, otherColliderOffset.y, 0.0f);

            Vector3 toOtherCollider = otherColliderPosition - colliderPosition;
            Vector2 toOtherCollider2D = new Vector2(toOtherCollider.x, toOtherCollider.y);
            float angle = Vector2.SignedAngle(new Vector2(1.0f, 0.0f), toOtherCollider2D);

            if (-45.0f < angle && angle <= 45.0f)
            {
                m_directionVector = new Vector3(-1.0f, 0.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirLeft");
            }
            else if (45.0f < angle && angle <= 135.0f)
            {
                m_directionVector = new Vector3(0.0f, -1.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirRight");
            }
            else if (135.0f < angle || angle <= -135.0)
            {
                m_directionVector = new Vector3(1.0f, 0.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirRight");
            }
            else if (-135.0f < angle || angle <= -45.0)
            {
                m_directionVector = new Vector3(0.0f, 1.0f, 0.0f);
                GetComponent<Animator>().Play("SetDirRight");
            }
        }
    }

    public void StopMoving()
    {
        m_moving = false;
    }
}
