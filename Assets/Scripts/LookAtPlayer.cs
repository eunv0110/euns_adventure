using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform Target;

    private void Update()
    {
        //���� �y���� ���� ������ ����
        Vector3 direction = Target.position - transform.position;

        //������ ������ ��ȯ
        float angle = DirectionToAngle(direction);

        //�ش� Ÿ�� �������� ȸ���Ѵ�.
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    /// <summary>
    /// ������ ������ �����մϴ�.
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private float DirectionToAngle(Vector3 direction)
    {
        //x,y�� ���� �����Ͽ� Z���� ������ ������. -> ~�� ������ ����
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        return angle;
    }
}
