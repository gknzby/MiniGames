using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using Gknzby.Components;

namespace Gknzby.VictoryPoser
{
    public class ArrowFlower : MonoBehaviour
    {
        private List<Arrow> arrowCollection;
        public List<Arrow> ArrowCollection
        {
            get
            {
                return arrowCollection ?? new();
            }
            set 
            {
                arrowCollection = value;
                SetDefaults();
            }
        }

        private float flowTime = 5f;
        private float spawnInterval = 2f;
        private Queue<Arrow> waitFlowQueue = new();
        private Queue<Arrow> activeFlowQueue = new();

        //Most left is 0 and most right is 1
        [Range(-1f, 2f)]
        [SerializeField] 
        private float LeftBoundary = 0.5f;

        [Range(-1f, 2f)]
        [SerializeField] 
        private float RightBoundary = 0.5f;

        [SerializeField] private GameObject ArrowPrefab;
        [SerializeField] private ArrowBox arrowBox;

        private CarrierLine defaultCarrier;

        private IEnumerator FlowTick()
        {
            float scaledFlowTime = flowTime * (RightBoundary - LeftBoundary);
            float totalTime = waitFlowQueue.Count * spawnInterval + scaledFlowTime;

            float killTimer = -scaledFlowTime;
            float spawnTimer = 0f;
            float initialLerp = 1f - RightBoundary;
            float timer = 0f;

            while (timer < totalTime)
            {
                yield return new WaitForFixedUpdate();
                float deltaTime = Time.fixedDeltaTime;

                timer += deltaTime;
                spawnTimer += deltaTime;
                killTimer += deltaTime;

                if(0 < waitFlowQueue.Count  && spawnInterval < spawnTimer)
                {
                    Arrow arrow = waitFlowQueue.Dequeue();
                    arrow.gameObject.SetActive(true);
                    arrow.Carrier = defaultCarrier;
                    arrow.Lerp = initialLerp;
                    activeFlowQueue.Enqueue(arrow);

                    spawnTimer -= spawnInterval;
                }

                if(0 < activeFlowQueue.Count && spawnInterval < killTimer)
                {
                    Arrow arrow = activeFlowQueue.Dequeue();
                    arrow.gameObject.SetActive(false);

                    killTimer -= spawnInterval;
                }

                UpdatePositions(deltaTime / flowTime);
            }

            foreach(Arrow arrow in activeFlowQueue)
            {
                arrow.gameObject.SetActive(false);
            }

            activeFlowQueue.Clear();
        }

        private void UpdatePositions(float increment)
        {
            foreach(Arrow arrow in activeFlowQueue)
            {
                arrow.Lerp += increment;
            }
        }

        public void StartFlow(out float finishTime)
        {
            if(0 < activeFlowQueue.Count || waitFlowQueue.Count != ArrowCollection.Count)
            {
                SetDefaults();
            }

            float scaledFlowTime = flowTime * (RightBoundary - LeftBoundary);
            finishTime = waitFlowQueue.Count * spawnInterval + scaledFlowTime;

            StartCoroutine(FlowTick());
        }

        public void StopFlow()
        {
            StopAllCoroutines();

            SetDefaults();
        }

        private void SetDefaults()
        {
            waitFlowQueue.Clear();
            activeFlowQueue.Clear();

            foreach (Arrow arrow in ArrowCollection)
            {
                waitFlowQueue.Enqueue(arrow);
            }

            RectTransform recto = GetComponent<RectTransform>();

            Vector3 sourcePoint = this.transform.position;
            sourcePoint.x = Screen.width;
            Vector3 destinationPoint = this.transform.position;
            destinationPoint.x = 0;
            defaultCarrier = new CarrierLine(null, sourcePoint, destinationPoint, false);
        }

        public AnimationState SendPlayerInput(ArrowDirection arrowDirection)
        {
            foreach(Arrow arrow in activeFlowQueue)
            {
                if(IsInBox(arrow.Lerp))
                {
                    return arrow.GetAnimation(arrow.Direction == arrowDirection);
                }
            }
            return AnimationState.Defeated;
        }


        private bool IsInBox(float pos)
        {
            arrowBox.GetMinMax(out float min, out float max);

            return (min < pos && pos < max);
        }
    }
}