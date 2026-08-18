using System.Collections.Generic;
using UnityEngine;

public class ConnectionHolder : MonoBehaviour
{
    [SerializeField] private List<ConnectionLine> skillNodes;

    private void Awake()
    {
        skillNodes.Clear();
        foreach (var child in this.GetComponentsInChildren<ConnectionLine>())
        {
            skillNodes.Add(child);
        }

        //Debug.Log("Done Awake");

        SkillNode.OnUnlocked += SkillNode_OnUnlocked;
    }

    private void SkillNode_OnUnlocked(object sender, SkillNode.OnUnlockedEventArgs e)
    {
        foreach (ConnectionLine line in skillNodes)
        {
            //if(line != null) { continue; }
            if (line.startNode.unlocked || line.endNode.unlocked) { line.Show(); } // needs work
            //else { line.Hide(); }
        }
    }

    private void OnEnable()
    {
        foreach (ConnectionLine line in skillNodes)
        {
            if (line.startNode.unlocked || line.endNode.unlocked ) { line.Show(); } // needs work
            else { line.Hide(); }
        }

        //Debug.Log("Done OnEnable");
    }

    private void OnDestroy()
    {
        SkillNode.OnUnlocked -= SkillNode_OnUnlocked;
    }
}
