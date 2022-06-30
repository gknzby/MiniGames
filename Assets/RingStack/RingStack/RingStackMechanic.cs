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

        private float sourceScreenPoint;
        private float destinationScreenPoint;


        [Header("Ring Prefab")]
        [SerializeField] private GameObject RingPrefab;
        [SerializeField] private RingStackLevelGenerator rslg;

        private Ring simulationRing;
        private Ring SimulationRing
        {
            get
            {
                if (simulationRing == null)
                {
                    simulationRing = GameObject.Instantiate(RingPrefab, this.transform).GetComponent<Ring>();
                }
                
                return simulationRing;
            }
        }

        private Dictionary<RingType, Material> materialCollection;
        private Dictionary<RingType, Material> MaterialCollection
        {
            get
            {
                if (materialCollection == null)
                {
                    materialCollection = rslg.GetMaterialCollection();
                }

                return materialCollection;
            }
        }

        [SerializeField] private Transform LeftRingHolder;
        [SerializeField] private Transform MidRingHolder;
        [SerializeField] private Transform RightRingHolder;

        private CarrierLine carrierLine;

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
                carrierLine = new CarrierLine(carrierLine, holdedRing.transform);
                PositionUpdate(screenPos);
            }
        }

        public void PositionUpdate(Vector2 screenPos)
        {
            if(holdedRing != null)
            {
                carrierLine.Lerp(GetLerpPoint(screenPos.x));
                SimulateRingToStack(screenPos);
            }
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

            SimulationRing.gameObject.SetActive(false);
        }
        #endregion

        #region Class Functions
        private void SimulateRingToStack(Vector2 screenPos)
        {
            if(TryGetHolder(screenPos, out RingHolder simulationHolder))
            {
                SimulationRing.gameObject.SetActive(true);

                if(simulationHolder == oldHolder)
                {
                    SimulationRing.SetTypeAndMaterial(RingType.SameHolder, MaterialCollection[RingType.SameHolder]);
                }
                else if(simulationHolder.IsStackable(holdedRing))
                {
                    SimulationRing.SetTypeAndMaterial(RingType.StackableHolder, MaterialCollection[RingType.StackableHolder]);
                }
                else
                {
                    SimulationRing.SetTypeAndMaterial(RingType.WrongHolder, MaterialCollection[RingType.WrongHolder]);
                }

                simulationHolder.SimulateRingToStack(SimulationRing.transform);
            }
            else
            {
                SimulationRing.gameObject.SetActive(false);
            }
        }

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

            CheckRingHolder(LeftRingHolder, ref emptyOne, ref oneColor);
            CheckRingHolder(MidRingHolder, ref emptyOne, ref oneColor);
            CheckRingHolder(RightRingHolder, ref emptyOne, ref oneColor);

            if (emptyOne && oneColor)
            {
                Debug.Log("Win");
                ManagerProvider.GetManager<IGameManager>().SendGameAction(GameAction.Win);
            }
            else
            {
                Debug.Log("Not Win");
            }
        }

        private void CheckRingHolder(Transform ringHolderTransform, ref bool emptyHolder, ref bool oneColor)
        {
            RingHolder holder = ringHolderTransform.GetComponent<RingHolder>();

            if (holder.IsEmpty())
            {
                emptyHolder = true;
            }
            else if (!holder.IsOneColor())
            {
                oneColor = false;
            }
        }

        private void SetCarrierLine()
        {
            Vector3 leftOffSet = new Vector3(-0.2f, 3f, 0f);
            Vector3 rightOffSet = new Vector3(0.2f, 3f, 0f);

            Vector3 sourceVector = LeftRingHolder.position + leftOffSet;
            Vector3 destinationVector = RightRingHolder.position + rightOffSet;

            sourceScreenPoint = Camera.main.WorldToScreenPoint(sourceVector).x;
            destinationScreenPoint = Camera.main.WorldToScreenPoint(destinationVector).x;

            carrierLine = new CarrierLine(null, sourceVector, destinationVector);
        }

        private float GetLerpPoint(float point)
        {
            return (point - sourceScreenPoint) / (destinationScreenPoint - sourceScreenPoint);
        }

        #endregion

        #region Unity Functions => OnEnable, OnDisable
        private void OnEnable()
        {
            ManagerProvider.GetManager<IInputManager>().SetDefaultReceiver(this);
            SetCarrierLine();
        }

        private void OnDisable()
        {
            ManagerProvider.GetManager<IInputManager>()?.RemoveDefaultReceiver(this);
        }
        #endregion
    }
}