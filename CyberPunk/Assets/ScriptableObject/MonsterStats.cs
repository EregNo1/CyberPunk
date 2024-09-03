using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterStats", menuName = "ScriptableObjects/MonsterStats", order = 1)]
public class MonsterStats : ScriptableObject
{
    [Header("Basic Stats")]
    public float health;
    public float movementSpeed;

    // 여기에 추가 스탯이나 데이터를 필요에 따라 추가할 수 있습니다.
}
