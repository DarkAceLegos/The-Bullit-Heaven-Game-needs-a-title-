using UnityEngine;

[CreateAssetMenu(menuName = "Card/AttackCard")]
public class AttackCard : Cards
{
    [SerializeField] private AttackData attackData; //{ get; set; }
}
