using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Components
{
    public class CarrierLine
    {
        //It's a carrier class and carriers carry cargo. Because of that, name of object that being carried is Cargo*****
        private readonly Transform cargoTransform;
        private readonly bool clamped = true;
        private readonly Vector3 sourceVector;
        private readonly Vector3 diffVector;

        public CarrierLine(in Transform cargoTransform, in Vector3 sourceVector, in Vector3 destinationVector, in bool clamped = true)
        {
            //source + (destination - source)*lerp
            //source + diff * lerp
            //To do not calculate everytime (destination - source) value
            this.cargoTransform = cargoTransform;
            this.sourceVector = sourceVector;
            this.clamped = clamped;
            this.diffVector = destinationVector - sourceVector;
        }   
        
        public CarrierLine(in CarrierLine carrierLine, in Transform cargoTransform)
        {
            this.cargoTransform = cargoTransform;
            this.clamped = carrierLine.clamped;
            this.sourceVector = carrierLine.sourceVector;
            this.diffVector = carrierLine.diffVector;
        }

        private static float Clamp01(in float value)
        {
            float clampedValue = value < 0.0f ? 0.0f : value;
            return 1.0f < clampedValue ? 1.0f : clampedValue;
        }

        public void Lerp(float lerp)
        {
            lerp = clamped ? CarrierLine.Clamp01(in lerp) : lerp;
            cargoTransform.position = sourceVector + diffVector * lerp;
        }
    }
}