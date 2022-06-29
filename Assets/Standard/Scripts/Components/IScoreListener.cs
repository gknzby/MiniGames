using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Components
{
    public interface IScoreListener
    {
        void HandleScoreChange(ScoreData scoreData);
    }
}
