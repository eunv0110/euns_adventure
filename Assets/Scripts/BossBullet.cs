using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 10f;
    public bool isRotate;

    private void Start()
    {
        //생성으로부터 2초 후 삭제
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);

        //두번째 파라미터에 Space.World를 해줌으로써 Rotation에 의한 방향 오류를 수정함
        transform.Translate(Vector2.right * (Speed * Time.deltaTime), Space.Self);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.gameObject.tag == "Playerbullet")
        {
            Destroy(gameObject);
        }
        */

        if (collision.gameObject.layer.Equals(LayerMask.NameToLayer("Platform")))
        {
            Destroy(gameObject);
        }
    }
}
