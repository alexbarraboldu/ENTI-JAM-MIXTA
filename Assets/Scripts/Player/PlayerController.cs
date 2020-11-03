using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public enum Direction { NONE, FRONT, REAR, LEFT, RIGHT};
	
	public float speed;
	public float hightOffset;
	public Rigidbody rb2d;

	public bool arrived;
	public bool moving;
	public float xToGo, zToGo;
	public Direction direction;
	public Direction looking;
	public bool walking;
	public float delta;

	public bool isOnFloor;

	//	ANIMATOR
	public Animator anim;
	public bool isRear;

    void Start()
    {
		rb2d = GetComponent<Rigidbody>();
		xToGo = 0;
		zToGo = 0;
		direction = Direction.NONE;
		moving = false;
		walking = false;
		looking = Direction.NONE;

		//	ANIMATOR
		anim = GetComponentInChildren<Animator>();
	}

	void Update()
    {
		delta = Time.deltaTime * 1000.0f;
		PlayerMovement();
		PlayerAnimation();
    }

	void PlayerMovement()
	{
		if (!moving)
		{
			direction = Direction.NONE;
		//	anim.SetInteger("Direction", 0);
			xToGo = rb2d.position.x;
			zToGo = rb2d.position.z;

			if (Input.GetKey(KeyCode.W))
			{
				direction = Direction.FRONT;
				looking = Direction.FRONT;
				zToGo += 1f;
				moving = true;
				walking = true;
			//	anim.SetInteger("Direction", 1);
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

			isOnFloor = false;
			isCollisionVertical(xToGo, zToGo);
			if (isCollisionHorizontal(xToGo, zToGo) || isOnFloor == false)
			{
				moving = false;
				walking = false;
				direction = Direction.NONE;
				xToGo = rb2d.position.x;
				zToGo = rb2d.position.z;
			}

			//ESTO TE COLOCA DE VUELTA AL GRID SI EN ALGUN MOMENTO TE SALIERAS
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

                xToGo = rb2d.position.x;
                zToGo = rb2d.position.z;
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
					arrived = false;
                    break;
                case Direction.REAR:
                    rb2d.position -= new Vector3(0, 0, speed * delta);
					arrived = false;
					break;					   
				case Direction.RIGHT:										//	X AXIS
					rb2d.position += new Vector3(speed * delta, 0, 0);
					arrived = false;
					break;					   
				case Direction.LEFT:		   
					rb2d.position -= new Vector3(speed * delta, 0, 0);
					arrived = false;
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
				direction = Direction.NONE;
				arrived = true;
			}

            //CHECK IF ToGo POSITOINS ARE OUT OF GRID
            if (Mathf.FloorToInt((prevX % 1) * 10) != 5 && Mathf.FloorToInt(Mathf.Repeat(prevZ, 1.0f) * 10) != 5)
            {
               // Debug.Log("ToGo OUT OF GRID");

                const float p = 0.5f;
                const float n = -0.5f;
                float x, z;
                if (prevX > 0) x = p;
                else x = n;
                if (prevZ > 0) z = p;
                else z = n;

                prevX = Mathf.Floor(prevX) + x;
                prevZ = Mathf.Floor(prevZ) + z;
            }

            if (rb2d.position.x != xToGo && rb2d.position.z != zToGo && !arrived)
            {
                rb2d.position = new Vector3(prevX, hightOffset, prevZ);
                moving = false;
            }
		}
	}


	private void PlayerAnimation()
	{
		if (!walking)
		{
			anim.SetBool("Moving", false);
		}
		else
		{
			anim.SetBool("Moving", true);
		}
		switch (looking)
		{
			case Direction.NONE:
				anim.SetInteger("Direction", 0);
				break;
			case Direction.FRONT:
				anim.SetInteger("Direction", 1);
				break;
			case Direction.REAR:
				anim.SetInteger("Direction", 2);
				break;
			case Direction.LEFT:
				anim.SetInteger("Direction", 3);
				break;
			case Direction.RIGHT:
				anim.SetInteger("Direction", 4);
				break;
			default:
				break;
		}

		switch (direction)
		{
            case Direction.NONE:
                //sanim.SetInteger("Direction", 0);
                break;
            //case Direction.FRONT:
            //	anim.SetInteger("Direction", 1);
            //	break;
            case Direction.REAR:
					anim.SetTrigger("Rear");
					//isRear = true;
				break;
			//case Direction.LEFT:
			//	anim.SetInteger("Direction", 3);
			//	break;
			//case Direction.RIGHT:
			//	anim.SetInteger("Direction", 4);
			//	break;
			default:
				break;
		}
	}

	private bool isCollisionHorizontal(float _x, float _Z)
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
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColLeft = true;
			}

			if (looking == Direction.RIGHT)
			{
				Pos = new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
				Dir = transform.right;
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColRight = true;
			}

			if (looking == Direction.FRONT)
			{
				Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z + 0.5f);
				Dir = transform.forward;
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColUp = true;
			}

			if (looking == Direction.REAR)
			{
				Pos = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
				Dir = -transform.forward;
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 0.5f);
				if (checkRaycastWithScenario(hits)) ColDown = true;
			}

			Debug.DrawRay(Pos, Dir);

			if (ColRight || ColLeft || ColUp || ColDown) return true;
			else return false;
		}
	}

	private bool isCollisionVertical(float _x, float _Z)
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

			Vector3 Dir = -transform.up;
			Vector3 Pos = Vector3.zero;

			if (looking == Direction.LEFT)
			{
				Pos = new Vector3(transform.position.x - 1f, transform.position.y + 0.5f, transform.position.z);
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 1.5f);
				if (checkRaycastWithScenario(hits)) ColLeft = true;
			}

			if (looking == Direction.RIGHT)
			{
				Pos = new Vector3(transform.position.x + 1f, transform.position.y + 0.5f, transform.position.z);
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 1.5f);
				if (checkRaycastWithScenario(hits)) ColRight = true;
			}

			if (looking == Direction.FRONT)
			{
				Pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z + 1f);
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 1.5f);
				if (checkRaycastWithScenario(hits)) ColUp = true;
			}

			if (looking == Direction.REAR)
			{
				Pos = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z - 1f);
				RaycastHit[] hits = Physics.RaycastAll(Pos, Dir, 1.5f);
				if (checkRaycastWithScenario(hits)) ColDown = true;
			}

			Debug.DrawRay(Pos, Dir, Color.red);

			if (ColRight || ColLeft || ColUp || ColDown) return true;
			else return false;
		}
	}

	private bool checkRaycastWithScenario(RaycastHit[] hits)
	{
		foreach (RaycastHit hit in hits)
		{
			if (hit.collider != null)
			{
				if (hit.collider.gameObject.tag == "Portal")
				{
					Debug.Log("Hit: Portal"); return true;
				}

				if (hit.collider.gameObject.tag == "Floor")
				{
					/*Debug.Log("Hit: Floor");*/
					isOnFloor = true;
					return false;
				}
				else
                {
					return true;
                }
			}
		}
		return false;
	}



}
