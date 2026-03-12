using UnityEngine;

public class EntryCardPrefabAddingCards : MonoBehaviour
{
    [SerializeField] private AttackCard attackCard;
    [SerializeField] private EnemyCard enemyCard;
    [SerializeField] private StatCard statCard;

    public void AddingCardData(AttackCard attackCard = null, EnemyCard enemyCard = null, StatCard statCard = null)
    {
        this.attackCard = attackCard;
        this.enemyCard = enemyCard;
        this.statCard = statCard;
    }
}
