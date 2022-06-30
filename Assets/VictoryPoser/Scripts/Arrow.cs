using Gknzby.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.VictoryPoser
{
    public class Arrow : MonoBehaviour
    {
        private CarrierLine carrier;
        public CarrierLine Carrier
        {
            get 
            { 
                return carrier; 
            }
            set
            {
                carrier = value == null ? null : new CarrierLine(value, this.transform);
            }
        }

        [SerializeField] private float lerp;
        public float Lerp
        {
            get 
            {
                return lerp; 
            }
            set 
            { 
                lerp = value;
                Carrier.Lerp(Lerp);
            }
        }

        public ArrowDirection Direction
        {
            get
            {
                return arrowData == null ? ArrowDirection.Undefined : arrowData.arrowDirection;
            }
        }

        private ArrowData arrowData;

        public void SetArrowData(ArrowData arrowData)
        {
            this.arrowData = arrowData;
            SetDirection(arrowData.arrowDirection);
            this.gameObject.SetActive(false);
        }

        public AnimationState GetAnimation(bool isSuccess)
        {
            return isSuccess ? arrowData.successfullAnimation : arrowData.failurefullAnimation;
        }

        private void SetDirection(ArrowDirection direction)
        {
            switch (direction)
            {
                case ArrowDirection.Undefined:
                    Debug.LogError("Arrow's direction value not assigned!");
                    break;
                case ArrowDirection.Up:
                    this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
                    break;
                case ArrowDirection.Down:
                    this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 180f));
                    break;
                case ArrowDirection.Left:
                    this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 90f));
                    break;
                case ArrowDirection.Right:
                    this.transform.localRotation = Quaternion.Euler(new Vector3(0f, 0f, 270f));
                    break;
                default:
                    break;
            }
        }
    }
}

