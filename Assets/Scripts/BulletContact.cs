using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContact : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMove playerMove;
    private EnemyMove enemyMove;

    private GameObject playerObject;
    private GameObject enemyObject;
    private GameObject enemy;

    public int enemyHealth;
    // Start is called before the first frame update
    void Start()
    {
        playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
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
                Destroy(collision.gameObject);
                Destroy(gameObject);
                enemyMove.itemDrop();
            }
            else
            {
                Destroy(collision.gameObject);
                enemyHealth--;
            }
        }
    }
}
