using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContact : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    private EnemyMove enemyMove;

    private Rigidbody2D rigid;

    private GameObject playerObject;
    private GameObject enemyObject;
    private GameObject enemy;

    public int enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
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
                if(playerAttack.skill!=1) //fire일때는 fire 안사라짐
                    Destroy(collision.gameObject);
                Destroy(gameObject);
                enemyMove.itemDrop();
            }
            else
            {
                if (playerAttack.skill != 1)
                    Destroy(collision.gameObject);

                //맞으면 몬스터가 위로 튕기게
                float damageDirection = playerMove.direction;
                rigid.AddForce(new Vector2(damageDirection, 1) * 7, ForceMode2D.Impulse);

                enemyHealth--;
            }
        }
    }
}
