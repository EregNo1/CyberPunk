using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    GameManager gameManager;
    protected Transform ClosestTarget { get; private set; }

    /*protected override void Awake()
    {
        base.Awake();
    }*/

    protected virtual void Start()
    {
        gameManager = GameManager.instance;
        ClosestTarget = gameManager.Player;
    }

    protected virtual void FixedUpdate()
    {

    }

    // ����� ������ �Ÿ��� ���ϴ� �޼ҵ�
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position); // ���� ������Ʈ�� ����� Ÿ�� ������ �Ÿ�
    }

    // Ÿ���� ������ ���ϴ� �޼ҵ�
    protected Vector2 DirectionToTarget()
    {
        // transform.position���� ClosestTarget.position�� �ٶ󺸴� ����
        return (ClosestTarget.position - transform.position).normalized; // normalized : ����ȭ �Ͽ� ���⸸ �����.
    }
}
