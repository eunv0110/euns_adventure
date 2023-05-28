using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public GameManager gameManager;
    public AudioClip audioJump;
    public AudioClip audioAttack;
    public AudioClip audioDamaged;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;


    public float maxSpeed;
    public float jumpPower;
    private int keyNumber;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    BoxCollider2D boxCollider;
    AudioSource audioSource;

    private PlayerAttack playerAttack;

    public int direction = -1;

    private SpriteRenderer spriteSetting;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        playerAttack = this.GetComponent<PlayerAttack>();
        spriteSetting = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Jump
        if (Input.GetButton("Jump") && !anim.GetBool("isJumping"))
        {
            rigid.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            anim.SetBool("isJumping", true);
            PlaySound("JUMP");
        }
        //Stop Speed
        if (Input.GetButtonUp("Horizontal"))
        {   //normalized: ???? ???? 1?? ????
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
            if (spriteRenderer.flipX)
            {
                direction = -1;
            }
            else
                direction = 1;
            //Debug.Log(direction);
        }

        //Animation
        if (Mathf.Abs(rigid.velocity.x) < 0.3 )
            anim.SetBool("isWalking", false);
        else
            anim.SetBool("isWalking", true);
    }
    void FixedUpdate()
    {
        //Move By Key Control
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right * h, ForceMode2D.Impulse);

        //Max Speed
        if (rigid.velocity.x > maxSpeed)//Right max speed
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        else if (rigid.velocity.x < maxSpeed*(-1)) //Left max speed
            rigid.velocity = new Vector2(maxSpeed * (-1), rigid.velocity.y);

        //Landing Platform
        if (rigid.velocity.y < 0)
        {
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance < 3.0f) //?????? ???? ???? ???? ???? ????
                {
                    anim.SetBool("isJumping", false);
                }
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            //?????????? ???? ???? + ?????? = ???? =>Attack
            if(rigid.velocity.y < 0 && transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }else
                OnDamaged(collision.transform.position);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            bool isBronze = collision.gameObject.name.Contains("Bronze");
            bool isSilver = collision.gameObject.name.Contains("Silver");
            bool isGold = collision.gameObject.name.Contains("Gold");

            if(isBronze)
                gameManager.stagePoint += 50;
            else if(isSilver)
                gameManager.stagePoint += 100;
            else if(isGold)
                gameManager.stagePoint += 150;

            //?????? ?????? ??????
            collision.gameObject.SetActive(false);
            PlaySound("ITEM");

            

        }
        if (collision.gameObject.CompareTag("Attack1"))
        {
            collision.gameObject.SetActive(false);
            playerAttack.skill = 1;
            //????1
        }
        else if (collision.gameObject.CompareTag("Attack2"))
        {
            collision.gameObject.SetActive(false);
            playerAttack.skill = 2;
            //????2
        }
        else if (collision.gameObject.CompareTag("Mujeok"))
        {
            collision.gameObject.SetActive(false);
            Mujeok();
        }
        else if (collision.gameObject.CompareTag("Big"))
        {
            collision.gameObject.SetActive(false);
            StartCoroutine(playerBigEffect());
        }
        else if (collision.gameObject.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            keyNumber++;
        }
        else if(collision.gameObject.tag == "Finish")
        {
            //Next Stage
            gameManager.NextStage();
            PlaySound("FINISH");
        }
    }

    void Mujeok()
    {
        gameObject.layer = 10;
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        Invoke("OffDamaged", 5);
    }

    IEnumerator playerBigEffect()
    {
        //isPlayerBig = true;
        transform.localScale = new Vector3(2.0f, 2.0f, 1.0f);
        yield return new WaitForSeconds(5.0f);

        for (int i = 0; i < 3; i++)
        {
            spriteSetting.color = new Color(1, 1, 1, 0.5f);
            yield return new WaitForSeconds(0.3f);
            spriteSetting.color = new Color(1, 1, 1, 1.0f);
            yield return new WaitForSeconds(1.3f);
        }
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        yield return new WaitForSeconds(0.3f);

        //isPlayerBig = false;
    }

    void OnAttack(Transform enemy)
    {
        // Point
        gameManager.stagePoint += 100;
        //Reaction Force
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);

        // Enemy Die
        EnemyMove enemyMove = enemy.gameObject.GetComponent<EnemyMove>();
        enemyMove.OnDamaged();
        PlaySound("ATTACK");
    }

    void OnDamaged(Vector2 targetPos)
    {
        //Health Down
        gameManager.HealthDown();

        // Change Layer (Immortal Active)
        gameObject.layer = 10;
        // View Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Reaction Force
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc,1)*7, ForceMode2D.Impulse);

        // Animation
        //anim.SetTrigger("doDamaged"); //?????????? ?????? ????????

        Invoke("OffDamaged", 3);
        PlaySound("DAMAGED");
    }

    void OffDamaged()
    {
        gameObject.layer = 9;
        spriteRenderer.color = new Color(1, 1, 1, 1);
    }

    public void OnDie()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        boxCollider.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        PlaySound("DIE");
    }

    public void VelocityZero()
    {
        rigid.velocity = Vector2.zero;
    }

    void PlaySound(string action)
    {
        switch (action)
        {
            case "JUMP":
                audioSource.clip = audioJump;
                break;
            case "ATTACK":
                audioSource.clip = audioAttack;
                break;
            case "DAMAGED":
                audioSource.clip = audioDamaged;
                break;
            case "ITEM":
                audioSource.clip = audioItem;
                break;
            case "DIE":
                audioSource.clip = audioDie;
                break;
            case "FINISH":
                audioSource.clip = audioFinish;
                break;
        }
        audioSource.Play();
    }
}
