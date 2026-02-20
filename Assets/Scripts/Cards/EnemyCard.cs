using UnityEngine;

[CreateAssetMenu(menuName = "Card/EnemyCard")]
public class EnemyCard : Cards
{
    [SerializeField] public int amount { get; set; }
    [SerializeField] public int packsAmount { get; set; }
    [SerializeField] public int type { get; set; }
}
