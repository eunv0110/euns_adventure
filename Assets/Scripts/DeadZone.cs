using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadZone : MonoBehaviour
{

    public GameManager gameManager;



    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            gameManager.HealthDown();
            if(gameManager.health>0)
                PlayerReposition();
        }
    }

    void PlayerReposition()
    {
        gameManager.player.transform.position= new Vector3(-35.6f, -1.7f, 0);
        gameManager.player.VelocityZero();
    }

    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

}
