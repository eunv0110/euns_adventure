using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    private SpriteRenderer spriteRenderer;
    private GameObject playerObject;

    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.FindWithTag("Player");


        if (playerObject != null)
        {
            playerMove = playerObject.GetComponent<PlayerMove>();
            playerAttack = playerObject.GetComponent<PlayerAttack>();
        }
     
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();


        if (playerAttack.skill != 1)
           spriteRenderer.flipX = playerMove.direction == -1;
        else
           spriteRenderer.flipX = playerMove.direction == 1;

        if (playerAttack.skill != 2)
           rb2d.velocity = transform.right * playerMove.direction * speed;

        rb2d.velocity = transform.right * playerMove.direction * speed;
        

        //���� enemy�� ���
        if (gameObject.CompareTag("Enemy"))
        {
            float dir = playerObject.transform.position.x - transform.position.x;
            dir = (dir < 0) ? -1 : 1;
            spriteRenderer.flipX = dir == -1;
            rb2d.velocity = transform.right * dir * speed;
        }
        else if (gameObject.CompareTag("Boss"))
        {
            float dir = playerObject.transform.position.x - transform.position.x;
            dir = (dir < 0) ? 1 : -1;
            spriteRenderer.flipX = dir == -1;
            rb2d.velocity = transform.right * dir * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bossbullet")
        {
            Destroy(gameObject);
        }
    }
}
