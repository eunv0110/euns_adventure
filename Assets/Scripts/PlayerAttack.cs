using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject playerBullet;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Å¬¸¯µÊ");
            GameObject temp = Instantiate(playerBullet, transform.position, transform.rotation);
            Destroy(temp, 1f);
            //Debug.Log(transform.position);
            //Debug.Log(transform.rotation);
        }
    }
}
