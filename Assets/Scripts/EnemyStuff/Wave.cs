using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(menuName = "Wave")]
public class Wave : ScriptableObject
{
    [SerializeField] public List<EnemyCard> enemyCards;
}
