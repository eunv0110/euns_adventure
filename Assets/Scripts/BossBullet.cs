using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour
{
    public float Speed = 10f;
    public bool isRotate;

    private void Start()
    {
        //�������κ��� 2�� �� ����
        Destroy(gameObject, 2f);
    }

    private void Update()
    {
        if (isRotate)
            transform.Rotate(Vector3.forward * 10);

        //�ι�° �Ķ���Ϳ� Space.World�� �������ν� Rotation�� ���� ���� ������ ������
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
