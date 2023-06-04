using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;
    private GameObject playerObject;
    public int nextMove;

    public GameObject attack1;
    public GameObject attack2;
    public GameObject mujeok;
    public GameObject big;
    public GameObject key;
    public GameObject lifeItem;

    public GameObject EnemyBullet;
    public GameObject EnemyBullet2;
    public int EnemyIdx = 0;

    private float attackDelay = 2;
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            //playerAttack = playerObject.GetComponent<PlayerAttack>();
            //playerMove = playerObject.GetComponent<PlayerMove>();
        }



        //if (EnemyIdx==0)
            //Invoke("Think", 5); //Think를 5초 뒤에 호출
        //if (EnemyIdx == 1)
            //Invoke("Chase", 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        attackDelay -= Time.deltaTime;
        if (attackDelay < 0) attackDelay = 0;

        if (EnemyIdx == 0)
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

        if (EnemyIdx == 1 || EnemyIdx == 2|| EnemyIdx == 3)
        {
            // 타겟과 자신의 거리를 확인
            float distance = Vector3.Distance(transform.position, playerObject.transform.position);
            //시야 범위안에 들어올 때
            if (distance <= 10)
            {
                //Debug.Log(distance);
                float dir = playerObject.transform.position.x - transform.position.x;
                dir = (dir < 0) ? -1 : 1;

                spriteRenderer.flipX = dir != 1;

                rigid.velocity = new Vector2( dir, rigid.velocity.y);
                transform.Translate(new Vector2(dir, 0) * 1 * Time.deltaTime);

                if (attackDelay == 0 && EnemyIdx==2)
                {
                    //공격
                    attackDelay = 2;
                    GameObject temp = Instantiate(EnemyBullet, transform.position, transform.rotation);
                    Destroy(temp, 3f);
                }

                if (EnemyIdx == 3 && distance <= 2)
                {
                    //자폭
                    GameObject temp = Instantiate(EnemyBullet2, transform.position, transform.rotation);
                    Destroy(temp, 1f);
                    Destroy(gameObject);

                }
            }
            else // 시야 범위 밖에 있을 때
            {
                //enemyAnimator.SetBool("moving", false);
            }
        }


    }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        //anim.SetInteger("WalkSpeed", nextMove);

        //Flip Sprite
        if(nextMove!=0) //가만히 서있을 때는 변화 x
            spriteRenderer.flipX = nextMove != 1;

        //Recursive
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime); //랜덤 시간 마다 Think 호출

    }

    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == 1;
        CancelInvoke(); //현재 작동중인 모든 Invoke함수를 멈춤
        Invoke("Think", 2);
    }

    public void Hit()
    {
        //Debug.Log("Hit");
        anim.SetTrigger("Hit");
    }

    public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        boxCollider.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Destroy
        Invoke("DeActive", 5);

        itemDrop();

        Destroy(gameObject);
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

    public void itemDrop()
    {
        float ran = Random.Range(0, 10);

/*        if (ran < 1) //Not Item 10%
        {
            Debug.Log("Not Item");
        }*/

        if (ran < 10) //10%
        { //key는 무조건 하나씩 나오게
            //얼레벌레 뭔가 이상한 코드!
            int ran2 = Random.Range(1, 10);
            Debug.Log(ran2);
            int dirc = transform.position.x - ran2 > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(ran2,1)*1, ForceMode2D.Impulse);
            Instantiate(key, transform.position + new Vector3(dirc,1,1), lifeItem.transform.rotation);
        }

        if (ran < 3) //10%
        {
            Instantiate(lifeItem, transform.position, lifeItem.transform.rotation);
        }
        else if (ran < 5.5) //25%
        {
            Instantiate(attack1, transform.position, attack1.transform.rotation);
        }
        else if (ran < 8) //25%
        {
            Instantiate(attack2, transform.position, attack2.transform.rotation);
        }
        else if (ran < 9) //10%
        {
            Instantiate(mujeok, transform.position, mujeok.transform.rotation);
        }
        else if (ran < 10) //10%
        {
            Instantiate(big, transform.position, big.transform.rotation);
            int dirc = transform.position.x - 10 > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(1, 1) * 7, ForceMode2D.Impulse);
        }
    }
}
