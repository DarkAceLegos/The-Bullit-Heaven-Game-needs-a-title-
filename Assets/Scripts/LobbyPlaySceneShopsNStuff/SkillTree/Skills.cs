using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private List<SkillNode> skillNodes;

    private void Start()
    {
        skillNodes.Clear();
        foreach (var child in this.GetComponentsInChildren<SkillNode>())
        {
            skillNodes.Add(child);
        }

    }

    private void OnEnable()
    {
        foreach (SkillNode node in skillNodes)
        {
            if (node.unlocked && node.clickable) { node.ShowConections(); }
        }
    }
}
