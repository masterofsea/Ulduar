using Prefabs.Characters.MainCharacter.Animations;
using Prefabs.Characters.MainCharacter.Movement;
using UnityEngine;

namespace Prefabs.Characters.MainCharacter.Scripts
{
    public class Agent : MonoBehaviour
    {
        private AnimationsController _animationsController;
        private MovementController _movementController;

        private void Awake()
        {
            _animationsController = GetComponent<AnimationsController>();
            _movementController = GetComponent<MovementController>();
        }

        public bool IsMove => _movementController.IsMove;
        public bool IsRun => _movementController.IsRun;
    }
}