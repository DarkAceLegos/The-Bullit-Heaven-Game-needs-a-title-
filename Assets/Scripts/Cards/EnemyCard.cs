using UnityEngine;

[CreateAssetMenu(menuName = "Card/EnemyCard")]
public class EnemyCard : Cards
{
    [SerializeField] public int amount { get; set; }
    [SerializeField] public int packs { get; set; }
    [SerializeField] public int type { get; set; }
}
