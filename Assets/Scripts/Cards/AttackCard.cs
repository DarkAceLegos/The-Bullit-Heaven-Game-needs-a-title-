using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/AttackCard")]
public class AttackCard : Cards
{
    [SerializeField] public string attackId;
    [SerializeField] public List<string> addIfMax;
    //[SerializeField] public AttackData attackData; 
}
