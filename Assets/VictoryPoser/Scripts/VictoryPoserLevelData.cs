using Gknzby.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.VictoryPoser
{
    [CreateAssetMenu(fileName = "VictoryPoserLevel", menuName = "Gknzby/Victory Poser/Level", order = 1)]
    public class VictoryPoserLevelData : ScriptableObject, ILevelData
    {
        public SubGame subGame { get { return SubGame.VictoryPoser; } }

#pragma warning disable UNT0013 // Remove invalid SerializeField attribute
        [SerializeField] public List<ArrowData> arrowDataCollection = new();
#pragma warning restore UNT0013 // Remove invalid SerializeField attribute
    }
}