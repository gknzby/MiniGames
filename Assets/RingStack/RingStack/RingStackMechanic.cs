using UnityEngine;
using Gknzby.Components;
using Gknzby.Managers;
using System.Collections.Generic;

namespace Gknzby.RingStack
{
    public class RingStackMechanic : MonoBehaviour, IInputReceiver
    {
        private RingHolder oldHolder;
        private RingHolder newHolder;
        private Ring holdedRing;

        [SerializeField] private List<Transform> RingHolderList = new();

        #region IInputReceiver
        public void Cancel()
        {
            if(holdedRing != null && oldHolder != null)
            {
                oldHolder.PushRingToStack(holdedRing);
            }
        }

        public void Click(Vector2 screenPos)
        {
            if(TryGetHolder(screenPos, out oldHolder))
            {
                oldHolder.PopRingFromStack(out holdedRing);
            }
        }

        public void PositionUpdate(Vector2 screenPos)
        {

        }

        public void Release(Vector2 screenPos)
        {
            if(holdedRing == null)
            {
                return;
            }

            if (TryGetHolder(screenPos, out newHolder)
                && newHolder != oldHolder
                && newHolder.IsStackable(holdedRing))
            {
                newHolder.PushRingToStack(holdedRing);
                CheckWinCondition();
            }
            else
            {
                oldHolder.PushRingToStack(holdedRing);
            }
            holdedRing = null;
        }
        #endregion

        #region Class Functions
        private bool TryGetHolder(Vector2 screenPos, out RingHolder holder)
        {
            Ray screenRay = GetScreenRay(screenPos);
            RaycastHit hit;
            holder = null;

            return Physics.Raycast(screenRay, out hit) && hit.transform.TryGetComponent<RingHolder>(out holder);
        }

        private Ray GetScreenRay(Vector2 screenPos)
        {
            return Camera.main.ScreenPointToRay(screenPos);
        }

        private void CheckWinCondition()
        {
            bool emptyOne = false;
            bool oneColor = true;
            foreach(Transform holderTransform in RingHolderList)
            {
                RingHolder holder = holderTransform.GetComponent<RingHolder>();

                if(holder.IsEmpty())
                {
                    emptyOne = true;
                }
                else if(!holder.IsOneColor())
                {
                    oneColor = false;
                }
            }

            if(emptyOne && oneColor)
            {
                Debug.Log("Win");
            }
            else
            {
                Debug.Log("Not");
            }
        }

        #endregion

        #region Unity Functions => OnEnable, OnDisable
        private void OnEnable()
        {
            ManagerProvider.GetManager<IInputManager>().SetDefaultReceiver(this);
        }

        private void OnDisable()
        {
            ManagerProvider.GetManager<IInputManager>()?.RemoveDefaultReceiver(this);
        }
        #endregion
    }
}