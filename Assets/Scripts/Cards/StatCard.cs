using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/StatCard")]
public class StatCard : Cards
{
    [SerializeField] public List<int> statId = new List<int>();
    [SerializeField] public List<float> statChangeAmount = new List<float>();
}
