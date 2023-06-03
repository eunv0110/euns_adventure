using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    
    Rigidbody2D rigid;
    Animator anim;
    SpriteRenderer spriteRenderer;
    BoxCollider2D boxCollider;

    public int nextMove;

    public GameObject attack1;
    public GameObject attack2;
    public GameObject mujeok;
    public GameObject big;
    public GameObject key;
    public GameObject lifeItem;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();

        Invoke("Think", 5); //Think를 5초 뒤에 호출
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //Platform Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Platform"));
        if (rayHit.collider == null){
            Turn();
        }
    }

    void Think()
    {
        //Set Next Active
        nextMove = Random.Range(-1, 2);

        //Sprite Animation
        anim.SetInteger("WalkSpeed", nextMove);

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

        if (ran < 1) //Not Item 10%
        {
            Debug.Log("Not Item");
        }
        else if (ran < 10) //10%
        {
            //얼레벌레 뭔가 이상한 코드!
            int ran2 = Random.Range(1, 10);
            Debug.Log(ran2);
            int dirc = transform.position.x - ran2 > 0 ? 1 : -1;
            rigid.AddForce(new Vector2(ran2,1)*1, ForceMode2D.Impulse);
            Instantiate(lifeItem, transform.position + new Vector3(dirc,1,1), lifeItem.transform.rotation);
        }
        else if (ran < 3) //10%
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
