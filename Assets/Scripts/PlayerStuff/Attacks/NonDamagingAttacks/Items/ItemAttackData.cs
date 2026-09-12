using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Attack/Item")]
public class ItemAttackData : AttackData
{
    [System.Serializable]
    public struct LevelData
    {
        public List<int> index;
        public List<int> numStacks;
        //public List<int> stacks;
        //public SerializableDictionary<int, int> items;
    }

    [SerializeField] private List<LevelData> levels = new();
    //[SerializeField] private List<LevelData> stacks = new();
    public LevelData GetLevelData(int level) => levels[Mathf.Clamp(level, 0, levels.Count - 1)];

    public void AddALevelData(LevelData levelData)
    { levels.Add(levelData); }
}
