using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.RingStack
{
    public class Ring : MonoBehaviour
    {
        [SerializeField] private RingType ringType;
        public RingType TypeOfRing { get { return ringType; } set { ringType = value; } }


        public void SetTypeAndMaterial(RingType ringType, Material ringMaterial)
        {
            this.ringType = ringType;
            this.GetComponent<Renderer>().material = ringMaterial;
        }
    }
}
