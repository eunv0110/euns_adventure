using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    public AudioClip audioDamaged;
    public AudioClip audioAttack;
    public AudioClip audioDie;

    AudioSource audioSource;

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

    public GameObject bulletObj;
    public GameObject playerObject;
    public Transform playerPos;
    private PlayerMove playerMove;
    public int startHealth;


    public GameObject S;
    public GameObject M;
    public GameObject L;

    public Transform ShotPosition;
    //GetComponent.

    void Awake()
    {
        playerObject = GameObject.FindWithTag("Player");


        if (playerObject != null)
        {
            playerMove = playerObject.GetComponent<PlayerMove>();
        }

        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
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
        if (!gameObject.activeSelf) return;
        
        Rigidbody2D rigid = GetComponent<Rigidbody2D>();

        InvokeRepeating("Think", 2, 2);
    }
    
    void Think()
    {
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

        PlaySound("ATTACK");

        Invoke("Think", 3);
    }

    void targetShot()
    {
        if (health <= 0)
            return;

        Debug.Log("2");

        GameObject bullet = Instantiate(bulletObj);

        Vector3 direction = playerPos.position - transform.position;

        //방향을 각도로 변환
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //해당 타겟 방향으로 회전한다.
        bullet.transform.rotation = Quaternion.Euler(0, 0, angle);
        bullet.transform.position = ShotPosition.position;

        //bullet.transform.rotation = ShotPosition.rotation;

        Destroy(bullet, 3f);

        PlaySound("ATTACK");

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

        PlaySound("ATTACK");

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

        PlaySound("ATTACK");

        Invoke("Think", 3);
    }
    

    public void OnDamaged(int dmg)
    {
        if (health <= 0)
            return;

        health -= dmg;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("ReturnSprite", 0.1f);
        PlaySound("DAMAGED");
        if (health <= 0)
        {
            Destroy(gameObject);
            playerObject.GetComponent<PlayerMove>().bossDie = true;
            playerObject.GetComponent<PlayerMove>().BossFinish();
            PlaySound("DIE");
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
        }
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
        }
        audioSource.Play();
    }
}
