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

    // 가까운 적과의 거리를 구하는 메소드
    protected float DistanceToTarget()
    {
        return Vector3.Distance(transform.position, ClosestTarget.position); // 현재 오브젝트와 가까운 타겟 까지의 거리
    }

    // 타겟의 방향을 구하는 메소드
    protected Vector2 DirectionToTarget()
    {
        // transform.position에서 ClosestTarget.position를 바라보는 방향
        return (ClosestTarget.position - transform.position).normalized; // normalized : 정규화 하여 방향만 남긴다.
    }
}
