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
    public GameObject playerPos;

    public int startHealth;

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
        rigid.velocity = Vector2.zero;

        
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
                FireFoward();
                break;
            case 1:
                FireShot();
                break;
            case 2:
                FireArc();
                break;
            case 3:
                FireAround();
                break;
        }
    }
    

    void FireFoward()
    {
        if (health <= 0)
            return;

        Debug.Log("1");

        //앞으로 4발 발사(Fire 4 Bullet Forward)
        GameObject bulletR = Instantiate(bulletObj, transform.position + Vector3.up * 0.3f, transform.rotation);
        GameObject bulletRR = Instantiate(bulletObj, transform.position + Vector3.up * 0.8f, transform.rotation);
        GameObject bulletL = Instantiate(bulletObj, transform.position + Vector3.down * 0.3f, transform.rotation);
        GameObject bulletLL = Instantiate(bulletObj, transform.position + Vector3.down * 0.8f, transform.rotation);

        Rigidbody2D rigidR = bulletR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidRR = bulletRR.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidL = bulletL.GetComponent<Rigidbody2D>();
        Rigidbody2D rigidLL = bulletLL.GetComponent<Rigidbody2D>();

        rigidR.AddForce(Vector2.right * 8, ForceMode2D.Impulse);
        rigidRR.AddForce(Vector2.right * 8, ForceMode2D.Impulse);
        rigidL.AddForce(Vector2.left * 8, ForceMode2D.Impulse);
        rigidLL.AddForce(Vector2.left * 8, ForceMode2D.Impulse);

        //#.Pattern Counting
        curPatternCount++;

        Destroy(bulletR, 3f);
        Destroy(bulletRR, 3f);
        Destroy(bulletL, 3f);
        Destroy(bulletLL, 3f);

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireFoward", 2);
        else
            Invoke("Think", 3);
    }
    void FireShot()
    {
        if (health <= 0)
            return;

        Debug.Log("2");

        //플레이어 방향으로 샷건(Fire 5 Random Shotgun Bullet to Player)
        for (int index = 0; index < 5; index++)
        {
            /*
            float angle = Mathf.Atan2(player.transform.position.y, player.transform.position.x) * Mathf.Rad2Deg;
            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            //transform.rotation = angle;

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            Vector2 dirVec = player.transform.position - transform.position;
            Vector2 ranVec = new Vector2(Random.Range(-0.5f, 0.5f), Random.Range(0f, 2f));
            dirVec += ranVec;
            

            rigid.AddForce(dirVec.normalized * 3, ForceMode2D.Impulse);
            */

            GameObject bullet = Instantiate(bulletObj, transform.position, transform.rotation);
            transform.position = playerPos.transform.position;
            Destroy(bullet, 3f);
        }


        //#.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireShot", 3.5f);
        else
            Invoke("Think", 3);
    }
    void FireArc()
    {
        if (health <= 0)
            return;

        Debug.Log("4");

        //원 형태로 전체 공격(Fire Around)
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++)
        {

            GameObject bullet = Instantiate(bulletObj, transform.position, Quaternion.identity);

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            //생성되는 총알의 순번을 활용하여 방향 결정
            Vector2 dirVec = new Vector2(Mathf.Sin(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 90 * index / roundNum + Vector3.forward * -90;
            bullet.transform.Rotate(rotVec);
            transform.position = playerPos.transform.position;

            Destroy(bullet, 3f);
        }

        //#.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireArc", 0.7f);
        else
            Invoke("Think", 3);
    }
    void FireAround()
    {
        if (health <= 0)
            return;

        Debug.Log("4");

        //원 형태로 전체 공격(Fire Around)
        int roundNumA = 50;
        int roundNumB = 40;
        int roundNum = curPatternCount % 2 == 0 ? roundNumA : roundNumB;

        for (int index = 0; index < roundNum; index++) 
        { 

            GameObject bullet = Instantiate(bulletObj, transform.position, Quaternion.identity);

            Rigidbody2D rigid = bullet.GetComponent<Rigidbody2D>();
            //생성되는 총알의 순번을 활용하여 방향 결정
            Vector2 dirVec = new Vector2(Mathf.Cos(Mathf.PI * 2 * index / roundNum), Mathf.Sin(Mathf.PI * 2 * index / roundNum));
            rigid.AddForce(dirVec.normalized * 2, ForceMode2D.Impulse);

            Vector3 rotVec = Vector3.forward * 360 * index / roundNum + Vector3.forward * 90;
            bullet.transform.Rotate(rotVec);

            Destroy(bullet, 3f);
        }

        //#.Pattern Counting
        curPatternCount++;

        if (curPatternCount < maxPatternCount[patternIndex])
            Invoke("FireAround", 0.7f);
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

    public void OnHit(int dmg)
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
            OnHit(1);
            Debug.Log("몬스터공격당함" + health);
            
            //Destroy(gameObject);
        }
    }
}
