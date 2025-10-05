using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Attack/FollowingAttack")]
public class FollowingAttackData : AttackData
{
    [System.Serializable]
    public struct LevelData
    {
        public int projCount;
        public int damage;
        public float cooldown;
        public float speed;
        public float Area;
    }

    [SerializeField] private List<LevelData> levels = new();
    public LevelData GetLevelData(int level) => levels[Mathf.Clamp(level, 0, levels.Count - 1)];
}
