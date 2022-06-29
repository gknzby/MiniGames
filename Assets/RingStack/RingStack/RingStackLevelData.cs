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


#pragma warning disable UNT0013 // Remove invalid SerializeField attribute
        [Header("Ring Prefab&Material")]
        [SerializeField] public RingMaterialCollection MaterialCollection;
        [SerializeField] public GameObject RingPrefab;

        [Header("Ring Holders")]
        [SerializeField] public RingHolderData holder1;
        [SerializeField] public RingHolderData holder2;
        [SerializeField] public RingHolderData holder3;
#pragma warning restore UNT0013 // Remove invalid SerializeField attribute
    }
}
