using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed;
    private PlayerMove playerMove;
    private PlayerAttack playerAttack;
    private GameObject playerObject;

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
        Debug.Log(playerMove.direction);

        if(playerAttack.skill != 1)
            spriteRenderer.flipX = playerMove.direction == -1;
        else
            spriteRenderer.flipX = playerMove.direction == 1;


        if (playerAttack.skill != 2)
            rb2d.velocity = transform.right * playerMove.direction * speed;

        rb2d.velocity = transform.right * playerMove.direction * speed;

    }

    // Update is called once per frame
    void Update()
    {

    }
}
