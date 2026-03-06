using UnityEngine;

[CreateAssetMenu(menuName = "Card/AttackCard")]
public class AttackCard : Cards
{
    [SerializeField] public AttackData attackData; //{ get; set; }
}
