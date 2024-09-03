using UnityEngine;

[CreateAssetMenu(fileName = "NewMonsterStats", menuName = "ScriptableObjects/MonsterStats", order = 1)]
public class MonsterStats : ScriptableObject
{
    [Header("Basic Stats")]
    public float health;
    public float movementSpeed;

    // ���⿡ �߰� �����̳� �����͸� �ʿ信 ���� �߰��� �� �ֽ��ϴ�.
}
