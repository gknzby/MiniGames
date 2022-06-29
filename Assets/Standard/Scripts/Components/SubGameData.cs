using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Components
{
    [System.Serializable]
    public class SubGameData : IData
    {
        public SubGame subGame;
        public string Prefix { get { return subGame.ToString(); } }
    }
}
