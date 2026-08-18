using System.Collections.Generic;
using UnityEngine;

public class Skills : MonoBehaviour
{
    [SerializeField] private List<SkillNode> skillNodes;

    private void Awake()
    {
        skillNodes.Clear();
        foreach (var child in this.GetComponentsInChildren<SkillNode>())
        {
            skillNodes.Add(child);
        }

        //Debug.Log("Done Awake");
    }

    private void OnEnable()
    {
        foreach (SkillNode node in skillNodes)
        {
            if (node.unlocked && node.clickable) { node.ShowConections(); }
        }

        //Debug.Log("Done OnEnable");
    }
}
