using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

namespace Prefabs.Characters.MainCharacter.Movement
{
    public class MovementController : MonoBehaviour
    {
        public float speed;
        public float runFactor;
        
        private NavMeshAgent _navMeshAgent;
        private InputActions _inputActions;
        private Camera _camera;
        
        
        public bool IsMove => _navMeshAgent.velocity.magnitude >= speed - speed*0.7f;
        public bool IsRun => _navMeshAgent.velocity.magnitude > 100f;

        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
            
            _navMeshAgent = GetComponent<NavMeshAgent>();
                
            _inputActions = new InputActions();
            _inputActions.CharacterController.MouseClick.Enable();
            
            SubscribeOnInput();
        }

        private void SubscribeOnInput()
        {
            _inputActions.CharacterController.MouseClick.performed += Move;
        }

        private void Move(InputAction.CallbackContext context)
        {
            var ray = _camera.ScreenPointToRay(Mouse.current.position.ReadValue());

            _navMeshAgent.speed = speed;
            if (Physics.Raycast(ray, out var hit, Mathf.Infinity))
                _navMeshAgent.SetDestination(hit.point);
        }
    }
}
