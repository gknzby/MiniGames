using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Gknzby.Managers
{
    public class GameSelector : MonoBehaviour, IManager
    {
        [SerializeField] private Transform RingStackGame;

        private IEnumerator Start()
        {
            yield return new WaitForSeconds(2f);
            RingStackGame.gameObject.SetActive(true);
        }


    }
}
