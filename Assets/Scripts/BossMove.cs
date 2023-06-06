using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public string enemyName;
    public int enemyScore;
    public float speed;
    public int health;
    public Sprite[] sprites;

    public float maxShotDeley;
    public float curShotDeley;

    Rigidbody2D rigid;
    Animator anim;
    BoxCollider2D boxCollider;
    SpriteRenderer spriteRenderer;

    public int nextMove;

    public int patternIndex;
    public int curPatternCount;
    public int[] maxPatternCount;

    public GameObject bulletObj;
    public GameObject player;
    public Transform playerPos;

    public int startHealth;

    public Transform ShotPosition;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

    }

    void OnEnable()
    {
        switch (enemyName)
        {
            case "L":
                health = 30;
                startHealth = 30;
                Invoke("Stop", 2);
                break;
            case "M":
                health = 20;
                startHealth = 20;
                Invoke("Stop", 2);
                break;
            case "S":
                health = 10;
                startHealth = 10;
                Invoke("Stop", 2);
                break;
        }
    }

    void Stop()
    {
        Debug.Log("한번만작동");
        if (!gameObject.activeSelf)
            return;
        
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();
        //rigid.velocity = Vector2.zero;

        
        InvokeRepeating("Think", 2, 2);
    }
    
    void Think()
    {
        Debug.Log("다시생각");
        curPatternCount = 0;

        if (enemyName == "S")
        {
            if (health == 0)
                return;
            else if (health >= startHealth / 2)
                patternIndex = 0;
            else
                patternIndex = 1;
        }
        else if (enemyName == "M")
        {
            if (health == 0)
                return;
            else if (health >= startHealth / 2)
                patternIndex = 1;
            else
                patternIndex = 2;
        }
        else if (enemyName == "L")
        {
            if (health == 0)
                return;
            else if (health >= startHealth / 2)
                patternIndex = 2;
            else
                patternIndex = 3;
        }


        switch (patternIndex)
        {
            case 0:
                UDLR();
                break;
            case 1:
                targetShot();
                break;
            case 2:
                randomRotShot();
                break;
            case 3:
                circleShot();
                break;
        }
    }
    

    void UDLR()
    {
        if (health <= 0)
            return;

        Debug.Log("1");

        GameObject bulletU = Instantiate(bulletObj);
        GameObject bulletD = Instantiate(bulletObj);
        GameObject bulletL = Instantiate(bulletObj);
        GameObject bulletR = Instantiate(bulletObj);

        bulletU.transform.position = ShotPosition.position;
        bulletD.transform.position = ShotPosition.position;
        bulletL.transform.position = ShotPosition.position;
        bulletR.transform.position = ShotPosition.position;

        Quaternion rotU = Quaternion.Euler(0, 0, 0);
        Quaternion rotD = Quaternion.Euler(0, 0, 90);
        Quaternion rotL = Quaternion.Euler(0, 0, 180);
        Quaternion rotR = Quaternion.Euler(0, 0, 270);

        bulletU.transform.rotation = rotU;
        bulletD.transform.rotation = rotD;
        bulletL.transform.rotation = rotL;
        bulletR.transform.rotation = rotR;

        Destroy(bulletU, 3f);
        Destroy(bulletD, 3f);
        Destroy(bulletR, 3f);
        Destroy(bulletL, 3f);

        //#.Pattern Counting
        //curPatternCount++;

        //if (curPatternCount < maxPatternCount[patternIndex])
        //    Invoke("UDLR", 2);
        //else
            Invoke("Think", 3);
    }

    void targetShot()
    {
        if (health <= 0)
            return;

        Debug.Log("2");

        GameObject bullet = Instantiate(bulletObj);

        bullet.transform.position = ShotPosition.position;

        bullet.transform.rotation = ShotPosition.rotation;

        Destroy(bullet, 3f);

        //#.Pattern Counting
        //curPatternCount++;

        //if (curPatternCount < maxPatternCount[patternIndex])
           // Invoke("targetShot", 3.5f);
       // else
            Invoke("Think", 3);
    }
    void randomRotShot()
    {
        if (health <= 0)
            return;

        Debug.Log("3");

        GameObject bulletL = Instantiate(bulletObj);
        GameObject bulletC = Instantiate(bulletObj);
        GameObject bulletR = Instantiate(bulletObj);

        bulletL.transform.position = ShotPosition.position;
        bulletC.transform.position = ShotPosition.position;
        bulletR.transform.position = ShotPosition.position;

        float ran = Random.Range(0, 360);

        Quaternion rotL = Quaternion.Euler(0, 0, ran - 10);
        Quaternion rotC = Quaternion.Euler(0, 0, ran);
        Quaternion rotR = Quaternion.Euler(0, 0, ran + 10);

        bulletL.transform.rotation = rotL;
        bulletC.transform.rotation = rotC;
        bulletR.transform.rotation = rotR;

        Destroy(bulletL, 3f);
        Destroy(bulletC, 3f);
        Destroy(bulletR, 3f);

        //#.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("randomRotShot", 0.7f);
        else
            Invoke("Think", 3);
    }
    void circleShot()
    {
        if (health <= 0)
            return;

        Debug.Log("4");

        //360번 반복
        for (int i = 0; i < 360; i += 13)
        {
            //총알 생성
            GameObject temp = Instantiate(bulletObj);

            //2초마다 삭제
            Destroy(temp, 2f);

            //총알 생성 위치를 ShotPosition 좌표로 한다.
            temp.transform.position = ShotPosition.position;

            //Z에 값이 변해야 회전이 이루어지므로, Z에 i를 대입한다.
            temp.transform.rotation = Quaternion.Euler(0, 0, i);
        }

        //#.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("circleShot", 0.7f);
        else
            Invoke("Think", 3);
    }
    /*
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null)
        {
            Turn();
        }
    }

    void Think2()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if (nextMove != 0) //가만히 서있을 때는 변화 x
            spriteRenderer.flipX = nextMove != 1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think2", nextThinkTime); //랜덤 시간 마다 Think 호출

    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke(); //현재 작동중인 모든 Invoke함수를 멈춤
        Invoke("Think2", 2);
    }

    void Update()
    {
        //Fire();
        Reload();
        BossState();
    }
    */
    

    //해도 그만 안해도 그만인 기능
    void BossState()
    {
        if (health <= startHealth/2)
            spriteRenderer.material.color = Color.green;

    }

    public void OnDamaged(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnSprite", 0.1f);
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    void ReturnSprite()
    {
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Playerbullet")
        {
            //BulletMove에서 총알의 위력을 설정하면 (int dmg)
            //BulletMove bullet = collision.gameObject.GetComponent<BulletMove>();
            //OnHit(bullet.dmg);
            OnDamaged(1);
            Debug.Log("보스공격당함" + health);
        }
    }


}
