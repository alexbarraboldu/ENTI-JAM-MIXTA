using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private bool moving;
	private float xToGo, zToGo;
	public enum Direction { NONE, FRONT, REAR, LEFT, RIGHT};
	private Direction direction;
	private Direction looking;
	private bool walking;
	public float speed;
	public float hightOffset;
	public Rigidbody rb2d;
	private float delta;

    void Start()
    {
		rb2d = GetComponent<Rigidbody>();
		xToGo = 0;
		zToGo = 0;
		direction = Direction.NONE;
		moving = false;
		walking = false;
		looking = Direction.NONE;
	}

	void Update()
    {
		delta = Time.deltaTime * 1000.0f;
		PlayerMovement();
    }

	void PlayerMovement()
	{
		if (!moving)
		{
			direction = Direction.NONE;
			xToGo = rb2d.position.x;
			zToGo = rb2d.position.z;

			if (Input.GetKey(KeyCode.W))
			{
				direction = Direction.FRONT;
				looking = Direction.FRONT;
				zToGo += 1f;
				moving = true;
				walking = true;
			}
			else if (Input.GetKey(KeyCode.S))
			{
				direction = Direction.REAR;
				looking = Direction.REAR;
				zToGo -= 1f;
				moving = true;
				walking = true;
			}
			else if (Input.GetKey(KeyCode.A))
			{
				direction = Direction.LEFT;
				looking = Direction.LEFT;
				xToGo -= 1f;
				moving = true;
				walking = true;
			}
			else if (Input.GetKey(KeyCode.D))
			{
				direction = Direction.RIGHT;
				looking = Direction.RIGHT;
				xToGo += 1f;
				moving = true;
				walking = true;
			}
			else
			{
				walking = false;
			}
			if (isCollision(xToGo, zToGo))
			{
				moving = false;
				walking = false;
				direction = Direction.NONE;
				xToGo = rb2d.position.x;
				zToGo = rb2d.position.z;
			}
            //	ESTO TE COLOCA DE VUELTA AL GRID SI EN ALGUN MOMENTO TE SALIERAS
            if (Mathf.FloorToInt((rb2d.position.x % 1) * 10) != 5 && Mathf.FloorToInt(Mathf.Repeat(rb2d.position.z, 1.0f) * 10) != 5)
            {
                Debug.Log("PLAYER OUR OF GRID");

                const float p = 0.5f;
                const float n = -0.5f;
                float x, z;
                if (rb2d.position.x > 0) x = p;
                else x = n;
                if (rb2d.position.z > 0) z = p;
                else z = n;

                rb2d.position = new Vector3(Mathf.Floor(rb2d.position.x) + x, hightOffset, Mathf.Floor(rb2d.position.z) + z);
            }
        }
		else
		{
			float prevX = rb2d.position.x;
			float prevZ = rb2d.position.z;

			switch (direction)
			{
				case Direction.FRONT:										//	Z AXIS
					rb2d.position += new Vector3(0, 0,speed * delta);
                    break;
                case Direction.REAR:
                    rb2d.position -= new Vector3(0, 0, speed * delta);
					break;					   
				case Direction.RIGHT:									//	X AXIS
					rb2d.position += new Vector3(speed * delta, 0, 0);
					break;					   
				case Direction.LEFT:		   
					rb2d.position -= new Vector3(speed * delta, 0, 0);
					break;
				default:
					break;
			}
			if ((prevX < xToGo && rb2d.position.x > xToGo) || (prevX > xToGo && rb2d.position.x < xToGo))
			{
				rb2d.position = new Vector3(xToGo, hightOffset, rb2d.position.z);
				//PlaySteps();
			}
			if ((prevZ < zToGo && rb2d.position.z > zToGo) || (prevZ > zToGo && rb2d.position.z < zToGo))
			{
				rb2d.position = new Vector3(rb2d.position.x, hightOffset, zToGo);
				//PlaySteps();
			}
			if (rb2d.position.x == xToGo && rb2d.position.z == zToGo)
			{
				moving = false;
			}
		}
	}

	private bool isCollision(float _x, float _Z)
	{
		float tileX = _x;
		float tileZ = _Z;
		if (tileX < -200.5 || tileZ < -200.5 || tileZ >= 200.5f || tileX >= 200.5f)
		{
			return true;
		}
		else
		{
			bool ColRight = false;
			bool ColLeft = false;
			bool ColUp = false;
			bool ColDown = false;

			Vector3 Dir = this.transform.up;
			Vector3 Pos = Vector3.zero;

			if (looking == Direction.LEFT)
			{
				Pos = new Vector3(transform.position.x - 0.5f, transform.position.y, transform.position.z);
				Dir = -transform.right;
				RaycastHit2D[] hits = Physics2D.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColLeft = true;
			}

			if (looking == Direction.RIGHT)
			{
				Pos = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
				Dir = transform.right;
				RaycastHit2D[] hits = Physics2D.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColRight = true;
			}

			if (looking == Direction.FRONT)
			{
				Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				Dir = transform.forward;
				RaycastHit2D[] hits = Physics2D.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColUp = true;
			}

			if (looking == Direction.REAR)
			{
				Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
				Dir = -transform.forward;
				RaycastHit2D[] hits = Physics2D.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColDown = true;
			}

			Debug.DrawRay(Pos, Dir);

			if (ColRight || ColLeft || ColUp || ColDown) return true;
			else return false;
		}
	}

	private bool checkRaycastWithScenario(RaycastHit2D[] hits)
	{
		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "wall") { Debug.Log("Hit"); return true; }
			}
		}
		return false;
	}



}
