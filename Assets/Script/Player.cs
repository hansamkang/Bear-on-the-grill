using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public int hungry;
    public int mentality;

    int showDirection;

    // player의 마지막 방향 확인 0=forward, 1=backward, 2=left, 3=right
    int lastDirection =0;

    // player의 움직임을 제어하는 객체
    public CharacterController controller;
    // player의 움직이는 속도
    public float runSpeed = 2;

    Animator animator;
    Vector3 moveDirection = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        showDirection = (int)Direction.South;
    }

    private void Update()
    {
        // 키보드 방향키로 이동.
        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= runSpeed;
        if (moveDirection.x >0 && moveDirection.y >0)
        {
            animator.Play("Player_walk_backward");
            lastDirection = 1;
        }
        else if(moveDirection.x>0 && moveDirection.y < 0)
        {
            animator.Play("Player_walk_forward");
            lastDirection = 0;
        }
        else if(moveDirection.x<0 && moveDirection.y < 0)
        {
            animator.Play("Player_walk_left");
            lastDirection = 2;
        }
        else if(moveDirection.x<0 && moveDirection.y > 0)
        {
            animator.Play("Player_walk_left");
            lastDirection = 2;
        }
        else if(moveDirection.x==0 && moveDirection.y > 0)
        {
            animator.Play("Player_walk_backward");
            lastDirection = 1;
        }
        else if(moveDirection.x ==0 && moveDirection.y < 0)
        {
            animator.Play("Player_walk_forward");
            lastDirection = 0;
        }
        else if(moveDirection.x >0 && moveDirection.y == 0)
        {
            animator.Play("Player_walk_right");
            lastDirection = 3;
        }
        else if(moveDirection.x<0 && moveDirection.y ==0)
        {
            animator.Play("Player_walk_left");
            lastDirection = 2;
        }
        else
        {
            switch (lastDirection)
            {
                case 0:
                    animator.Play("Player_idle_forward");
                    break;
                case 1:
                    animator.Play("Player_idle_backward");
                    break;
                case 2:
                    animator.Play("Player_idle_left");
                    break;
                case 3:
                    animator.Play("Player_idle_right");
                    break;
            }
        }

    }

    void FixedUpdate()
    {
        controller.Move(moveDirection * Time.deltaTime);
    }
    
    // 플레이어 캐릭터가 존재하는 섹터를 Vector2 형태로 반환
    public Vector2 getSector(int sectorSize)
    {
        int x, y;
        x = (int)gameObject.transform.position.x + sectorSize / 2;
        y = (int)gameObject.transform.position.y + sectorSize / 2;

        if (x <= 0)
            x = x / sectorSize - 1;
        else
            x = x / sectorSize;

        if (y <= 0)
            y = y / sectorSize - 1;
        else
            y = y / sectorSize;

        return new Vector2(x, y);
    }

    // 플레이어의 X좌표의 위치가 섹터 변경을 해야하는지 확인하는 함수 
    public int checkChangeSectorX(int sectorSize,int offset )
    {
        int x, t;
        t = (int)gameObject.transform.position.x + sectorSize / 2;
        x = (t % sectorSize);

        if (t > 0)
        {
            if (x < offset) 
                return -1;
            else if (x > sectorSize - offset)   
                return 1;
            else
                return 0;
        }
        else
        {
            if (x < -(sectorSize - offset))
                return -1;
            else if (x > -offset)
                return 1;
            else
                return 0;
        }
    }

    // 플레이어의 Y좌표의 위치가 섹터 변경을 해야하는지 확인하는 함수
    public int checkChangeSectorY(int sectorSize, int offset)
    {
        int y, t;
        t = (int)gameObject.transform.position.y + sectorSize/2;
        y = t % sectorSize;

        if (t > 0)
        {
            if (y < offset)
                return -1;
            else if (y > sectorSize - offset)
                return 1;
            else
                return 0;
        }
        else
        {
            if (y < -(sectorSize - offset))
                return -1;
            else if (y > -offset)
                return 1;
            else
                return 0;
        }
    }

    // 삭제해도 무방
    public void testCheck()
    {
        int sectorSize = 32;
        int offset = 5;
        int x, t, z;
        t = (int)gameObject.transform.position.x + sectorSize / 2;
        z = (int)(gameObject.transform.position.x + sectorSize / 2);

        Debug.Log("t: "+ t+ "   z: " + z);
        x = t % sectorSize;

        if (t > 0)
        {
            if (x < offset)
                Debug.Log(-1);
            else if (x > sectorSize - offset)
                Debug.Log(1);
            else
                Debug.Log(0);
        }
        else
        {
            if (x < -(sectorSize - offset))
                Debug.Log(-2);
            else if (x > -offset)
                Debug.Log(2);
            else
                Debug.Log(3);
        }
    }
}
