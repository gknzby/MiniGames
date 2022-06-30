using Gknzby.Components;
using Gknzby.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gknzby.VictoryPoser
{
    public class VictoryPoserPlayMechanic : MonoBehaviour, IInputReceiver, IEventListener<ILevelData>
    {
        [SerializeField] private Animator CharacterAnimator;
        [SerializeField] private ArrowFlower arrowFlower;
        [SerializeField] private Photographer photographer;

        private float swipeDuration = 0.2f;
        private float swipeTime = 0f;
        private Vector2 firstPos;

        #region InputReceiver
        public void Cancel()
        {
            return;
        }

        public void Click(Vector2 screenPos)
        {
            swipeTime = Time.time;
            firstPos = screenPos;
        }

        public void PositionUpdate(Vector2 screenPos)
        {
            if(swipeDuration < Time.time - swipeTime)
            {
                ManagerProvider.GetManager<IInputManager>().StartSendingInputs();
            }
        }

        public void Release(Vector2 screenPos)
        {
            ArrowDirection direction;
            Vector2 drag = screenPos - firstPos;

            if(Mathf.Abs(drag.y) < Mathf.Abs(drag.x))
            {
                direction = 0 < drag.x ? ArrowDirection.Right : ArrowDirection.Left;
            }
            else
            {
                direction = 0 < drag.y ? ArrowDirection.Up : ArrowDirection.Down;
            }

            Debug.Log(direction);

            AnimationState state = arrowFlower.SendPlayerInput(direction);
            AnimateCharacter(state);

            if(state != AnimationState.Defeated)
                StartCoroutine(photographer.TakePhoto(0.3f));
        }
        #endregion

        #region Events => LevelChange
        public void HandleEvent(ILevelData eventArg)
        {
            HandleEvent();
        }

        public void HandleEvent()
        {
            AnimateCharacter(AnimationState.Idle);
        }
        #endregion

        #region Class Functions
        //For defined animations
        private void AnimateCharacter(AnimationState animationState)
        {
            AnimateCharacter((int)animationState);
        }

        //For undefined animations, user doesn't have to define all animations.
        private void AnimateCharacter(int animationStateInt)
        {
            CharacterAnimator.SetInteger("AnimationState", animationStateInt);
        }
        #endregion

        #region Unity Functions => OnEnable, OnDisable
        private void OnEnable()
        {
            ManagerProvider.GetManager<IInputManager>().SetDefaultReceiver(this);
            ManagerProvider.GetManager<IEventManager>().AddEventListener(Gknzby.EventName.LevelChange, this);
        }

        private void OnDisable()
        {
            ManagerProvider.GetManager<IInputManager>()?.RemoveDefaultReceiver(this);
            ManagerProvider.GetManager<IEventManager>()?.RemoveEventListener(Gknzby.EventName.LevelChange, this);
        }
        #endregion
    }
}