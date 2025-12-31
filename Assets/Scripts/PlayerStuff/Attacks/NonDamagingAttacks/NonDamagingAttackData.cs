using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attack/NonDamagingAttack")]
public class NonDamagingAttackData : AttackData
{
    [System.Serializable]
    public struct LevelData
    {
        public int value;
    }

    [SerializeField] private List<LevelData> levels = new();
    public LevelData GetLevelData(int level) => levels[Mathf.Clamp(level, 0, levels.Count - 1)];

    public void AddALevelData(LevelData levelData)
    { levels.Add(levelData); }
}
