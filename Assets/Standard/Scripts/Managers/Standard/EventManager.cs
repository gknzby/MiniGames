using Gknzby.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.Managers
{
    public class EventManager : MonoBehaviour, IEventManager
    {
        private readonly Dictionary<EventName, List<IEventListener>> eventDictionary = new();

        public void AddEventListener(EventName eventName, IEventListener eventListener)
        {
            if(!eventDictionary.ContainsKey(eventName))
            {
                eventDictionary.Add(eventName, new());
            }

            eventDictionary[eventName].Add(eventListener);
        }

        public void RemoveEventListener(EventName eventName, IEventListener eventListener)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                return;
            }

            _ = eventDictionary[eventName].Remove(eventListener);
        }

        public void InvokeEvent<T>(EventName eventName, T eventArgs = default) where T : IData
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                return;
            }

            foreach (IEventListener<T> eventListener in eventDictionary[eventName])
            {
                eventListener.HandleEvent(eventArgs);
            }
        }

        public void InvokeEvent(EventName eventName)
        {
            if (!eventDictionary.ContainsKey(eventName))
            {
                return;
            }

            foreach (IEventListener eventListener in eventDictionary[eventName])
            {
                eventListener.HandleEvent();
            }
        }


        #region Unity Functions => Awake, OnDestroy
        private void Awake()
        {
            ManagerProvider.AddManager<IEventManager>(this);
        }

        private void OnDestroy()
        {
            ManagerProvider.RemoveManager<IEventManager>();
        }
        #endregion
    }
}
