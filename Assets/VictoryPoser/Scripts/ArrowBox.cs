using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.VictoryPoser
{
    public class ArrowBox : MonoBehaviour
    {
        [SerializeField] private Vector2 size;

        private void Awake()
        {
            GetComponent<RectTransform>().sizeDelta = size;
        }

        public void GetMinMax(out float min, out float max)
        {
            float widthRatio = size.x/Screen.width;
            float origin = 1f - this.transform.position.x / Screen.width;

            min = origin - widthRatio / 2f;
            max = origin + widthRatio / 2f;
        }
    }
}
