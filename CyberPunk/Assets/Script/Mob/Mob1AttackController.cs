using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class Mob1AttackController : MonoBehaviour
{
    //[SerializeField] private float followRange = 15f;
    //[SerializeField] private float shootRange = 10f;


    [SerializeField] private float shootDelay;

    [SerializeField] GameObject mob1BulletPref;

    private float startTime;

    /*protected override void FixedUpdate()
    {
        base.FixedUpdate();

        float distance = DistanceToTarget();
        Vector2 direction = DirectionToTarget();

        IsAttacking = false;
        if (distance <= followRange)
        {
            if (distance <= shootRange)
            {
                int layerMaskTarget = Stats.CurrentStates.attackSO.target;
                // Enemy와 Player 사이에 지형(장애물)이 있다면 공격(원거리)할 필요가 없다. 막혀있는 지형이 있는지 검사하는 코드
                RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 11f, (1 << LayerMask.NameToLayer("Level")) | layerMaskTarget);

                if (hit.collider != null && layerMaskTarget == (layerMaskTarget | (1 << hit.collider.gameObject.layer)))
                {
                    CalllLookEvent(direction);
                    CallMoveEvent(Vector2.zero);
                    IsAttacking = true;
                }
                else
                {
                    CallMoveEvent(direction);
                }
            }
            else
            {
                CallMoveEvent(direction);
            }
        }
        else
        {
            CallMoveEvent(direction);
        }
    }*/

    private void Start()
    {
        StartCoroutine(repeat());
    }




    IEnumerator repeat()
    {
        Instantiate(mob1BulletPref, transform.position, transform.rotation);
        yield return new WaitForSeconds(shootDelay);
        StartCoroutine(repeat());
    }

}
