using UnityEngine;
using System.Collections.Generic;

namespace Gknzby.RingStack
{
    public class RingHolder : MonoBehaviour
    {
        [SerializeField] private GameObject RingPrefab;

        [SerializeField] private int maxRingCount;
        [SerializeField] private float firstRingHeight;
        [SerializeField] private float ringSpacing;

        private readonly Stack<Ring> Rings = new();

        //private void Start()
        //{
        //    //AddRingToStack();
        //    //AddRingToStack();
        //    //RemoveRingFromStack();
        //    //AddRingToStack();
        //    //AddRingToStack();
        //    //RemoveRingFromStack();
        //    //RemoveRingFromStack();
        //    //AddRingToStack();
        //    //AddRingToStack();
        //    //AddRingToStack();

        //    TestCreate();
        //}

        //[System.Serializable]
        //public class TestClass
        //{
        //    public RingType ringType;
        //    public Material ringMat;
        //}
        //[SerializeField] private List<TestClass> testList = new();
        //private void TestCreate()
        //{
        //    foreach(TestClass test in testList)
        //    {
        //        AddRingToStack();
        //        Rings.Peek().TypeOfRing = test.ringType;
        //        Rings.Peek().transform.GetComponent<MeshRenderer>().material = test.ringMat;
        //    }
        //}

        public void AddRingToStack()
        {
            Ring newRing = GameObject.Instantiate(RingPrefab, this.transform).GetComponent<Ring>();

            Vector3 newPosition = newRing.transform.position;
            newPosition.y = GetNewRingHeight();
            newRing.transform.position = newPosition;

            Rings.Push(newRing);
        }

        public void RemoveRingFromStack()
        {
            if(PopRingFromStack(out Ring ring))
            {
                GameObject.Destroy(ring.gameObject);
            }
        }

        public void PushRingToStack(Ring ring)
        {
            ring.transform.parent = this.transform;
            Vector3 newPosition = this.transform.position;
            newPosition.y = GetNewRingHeight();
            ring.transform.position = newPosition;

            Rings.Push(ring);
        }

        public bool PopRingFromStack(out Ring ring)
        {
            if(Rings.Count > 0)
            {
                ring = Rings.Pop();
                return true;
            }
            else
            {
                ring = null;
                return false;
            }
        }

        public void ClearStack()
        {
            while(Rings.Count > 0)
            {
                Ring ring = Rings.Pop();
                GameObject.Destroy(ring.gameObject);
            }
        }

        public void SimulateRingToStack()
        {

        }

        public bool IsStackable(Ring ring)
        {
            return (IsEmpty() || (!IsFull() && Rings.Peek().TypeOfRing == ring.TypeOfRing));
        }

        public bool IsFull()
        {
            return Rings.Count == maxRingCount;
        }

        public bool IsEmpty()
        {
            return Rings.Count == 0;
        }

        public bool IsOneColor()
        {
            if (IsEmpty())
                return true;

            Ring[] ringArr = Rings.ToArray();
            RingType firstType = ringArr[0].TypeOfRing;
            for(int i = 1; i < ringArr.Length; i++)
            {
                if (ringArr[i].TypeOfRing != firstType)
                    return false;
            }

            return true;
        }

        private float GetNewRingHeight()
        {
            return Rings.Count > 0 ? Rings.Peek().transform.position.y + ringSpacing : firstRingHeight;
        }
    }

}