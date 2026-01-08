using System.Collections.Generic;
using UnityEngine;

public abstract class AttackData : ScriptableObject
{
    public string attackId;
    public string attackName;
    [SerializeField] private List<string> levelDiscriptions = new();
    public Sprite icon;
    public Attack prefab;
    //public struct LevelData { }

    public string GetLevelDescription(int level) => levelDiscriptions[Mathf.Clamp(level, 0, levelDiscriptions.Count - 1)];
    public int maxLevel => levelDiscriptions.Count - 1;
    //public abstract void AddALevelData();
}
