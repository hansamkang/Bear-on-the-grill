using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int hp;
    public float hungry;
    public float thirsty;
    public float temperature;

    // player의 상태 변경 가능 확인 bool 변수
    public bool cableAll;
    public bool cableHp;
    public bool cableHungry;
    public bool cableThirsty;
    public bool cableTemperature;

    // player의 상태 기본 감소량
    public int basicDeltaHp;
    public float basicDeltaHungry;
    public float basicDeltaThirsty;
    public float basicDeltaTemperature;

    // playre의 최종 상태 감소량
    int deltaHp;
    float deltaHungry;
    float deltaThirsty;
    float deltaTemprature;

    // player의 마지막 방향 확인 0=forward, 1=backward, 2=left, 3=right
    int lastDirection =0;

    // player의 움직임을 제어하는 객체
    public CharacterController controller;
    // player의 움직이는 속도
    public float runSpeed = 2;

    TimeManager timeManger;

    Animator animator;
    Vector3 moveDirection = Vector3.zero;

    List<GameObject> pickList = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        timeManger = FindObjectOfType<TimeManager>().GetComponent<TimeManager>();
        animator = GetComponent<Animator>();

        timeManger.ChangeHour += Player_ChangeHour;
    }

    private void Update()
    {

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

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log("아이템 이름: " + collision.gameObject.name + "  ID:" + collision.gameObject.GetInstanceID());
        if(collision.tag == "Item")
        {
            pickList.Add(collision.gameObject);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        Debug.Log("아이템 이름: " + collision.gameObject.name + "  ID:" + collision.gameObject.GetInstanceID());
        if (collision.tag == "Item")
        {
            pickList.Remove(collision.gameObject);
        }
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

    // 플레이어 상태 변화 함수
    public void addState(int i, int value)
    {
        switch (i)
        {
            case 0:
                hp += value;
                break;
            case 1:
                hungry += value;
                break;
            case 2:
                thirsty += value;
                break;
            case 3:
                temperature += value;
                break;
        }
    }

    // 플레이어 상태 변경 함수
    public void changeState(int i, int value)
    {
        switch (i)
        {
            case 0:
                hp = value;
                break;
            case 1:
                hungry = value;
                break;
            case 2:
                thirsty = value;
                break;
            case 3:
                temperature = value;
                break;
        }
    }

    public void Player_ChangeHour(object sender, ChangeTimeEventArgs e)
    {
        // 델타값 뒤에 +로 추가값 이어 붙이면 됨.
        deltaHp = basicDeltaHp;
        deltaHungry = basicDeltaHungry;
        deltaThirsty = basicDeltaThirsty;
        deltaTemprature = basicDeltaTemperature;

        hp -= deltaHp;
        hungry -= deltaHungry;
        thirsty -= deltaThirsty;
        temperature -= deltaTemprature;
        Debug.Log("시간 변함");
    }

    // 아이템 리스트에서 아이템 주움
    public void pickUpItem()
    {
        Debug.Log("픽업들어옴");
        if (pickList.Count <= 0) return;
        GameObject temp = pickList[0];
        pickList.RemoveAt(0);
        FindObjectOfType<Inventory>().addItem(temp.GetComponent<Item>());
        temp.SetActive(false);
    }
}
