using System.Collections;
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

    private Direction m_direction;

    private float m_directionChangeDelay;
    private float m_nextDirectionChangeTime;

    // Start is called before the first frame update
    void Start()
    {
        m_direction = (Direction)Random.Range((int)Direction.Left, (int)Direction.Top + 1);

        m_directionChangeDelay = Random.Range(m_directionChangeMinDelay, m_directionChangeMaxDelay);
        m_nextDirectionChangeTime = Time.time + m_directionChangeDelay;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Time.time > m_nextDirectionChangeTime)
        {
            ChangeDirectionRandomly();
        }

        Vector3 move = new Vector3(0.0f, 0.0f, 0.0f);

        switch (m_direction)
        {
            case Direction.Left:
                move.x = -1.0f;
                break;
            case Direction.Right:
                move.x = 1.0f;
                break;
            case Direction.Top:
                move.y = 1.0f;
                break;
            case Direction.Bottom:
                move.y = -1.0f;
                break;
        }

        transform.localPosition = transform.localPosition + move * m_speed * Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;

        if (tag.Equals("LeftWall"))
        {
            GoInGivenDirection(Direction.Right);
        }
        else if (tag.Equals("RightWall"))
        {
            GoInGivenDirection(Direction.Left);
        }
        else if (tag.Equals("TopWall"))
        {
            GoInGivenDirection(Direction.Bottom);
        }
        else if (tag.Equals("BottomWall"))
        {
            GoInGivenDirection(Direction.Top);
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
                GoInGivenDirection(Direction.Left);
            }
            else if (45.0f < angle && angle <= 135.0f)
            {
                GoInGivenDirection(Direction.Bottom);
            }
            else if (135.0f < angle || angle <= -135.0)
            {
                GoInGivenDirection(Direction.Right);
            }
            else if (-135.0f < angle || angle <= -45.0)
            {
                GoInGivenDirection(Direction.Top);
            }
        }
    }

    void ChangeDirectionRandomly()
    {
        Direction newDirection = Direction.Left;

        do
        {
            newDirection = (Direction)Random.Range((int)Direction.Left, (int)Direction.Top + 1);
        } while (newDirection == m_direction);

        m_direction = newDirection;

        m_directionChangeDelay = Random.Range(m_directionChangeMinDelay, m_directionChangeMaxDelay);
        m_nextDirectionChangeTime = Time.time + m_directionChangeDelay;
    }

    void GoInGivenDirection(Direction _direction)
    {
        m_direction = _direction;

        m_directionChangeDelay = Random.Range(m_directionChangeMinDelay, m_directionChangeMaxDelay);
        m_nextDirectionChangeTime = Time.time + m_directionChangeDelay;
    }
}
