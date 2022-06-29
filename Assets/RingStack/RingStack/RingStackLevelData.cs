using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.RingStack
{
    [CreateAssetMenu(fileName = "RingStackLevel", menuName = "Gknzby/Ring Stack/Level", order = 1)]
    public class RingStackLevelData : ScriptableObject, Components.ILevelData
    {
        public SubGame WhatIsThis { get { return gameType; } }
        private readonly SubGame gameType = SubGame.RingStack;


        [Header("Material Dictionary")]
        [SerializeField] public RingMaterials materialDictionary;

        [Header("Ring Holders")]
        [SerializeField] public RingHolderData holder1;
        [SerializeField] public RingHolderData holder2;
        [SerializeField] public RingHolderData holder3;
    }
}
