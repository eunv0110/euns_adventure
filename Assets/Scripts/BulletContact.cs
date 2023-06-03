using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContact : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    private EnemyMove enemyMove;


    Animator anim;
    private Rigidbody2D rigid;

    private GameObject playerObject;
    private GameObject enemyObject;
    private GameObject enemy;

    public int enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody2D>();

        playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            playerAttack = playerObject.GetComponent<PlayerAttack>();
            playerMove = playerObject.GetComponent<PlayerMove>();
        }

        GameObject gameManagerObject = GameObject.FindWithTag("GameController");
        if (gameManagerObject != null)
        {
            gameManager = gameManagerObject.GetComponent<GameManager>();
        }
        
        enemyMove = this.GetComponent<EnemyMove>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Playerbullet"))
        {
            if (enemyHealth <= 0)
            {
                if(playerAttack.skill!=2) //fire일때는 fire 안사라짐
                    Destroy(collision.gameObject);
                Destroy(gameObject);
                if (enemyMove != null)
                { 
                    enemyMove.itemDrop();
                }
            }
            else
            {
                if (playerAttack.skill != 2)
                    Destroy(collision.gameObject);

                //맞으면 몬스터가 위로 튕기게
                //float damageDirection = playerMove.direction;

                rigid.AddForce(Vector2.up * 7, ForceMode2D.Impulse);
                //rigid.AddForce(Vector2.right * damageDirection*7, ForceMode2D.Impulse);
                enemyHealth--;
            }
        }
    }
}
