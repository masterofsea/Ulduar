using UnityEngine;
using UnityEngine.InputSystem;

namespace Prefabs.Characters.MainCharacter.Scripts
{
    public class AnimationAndMovementController : MonoBehaviour
    {
        private InputActions _playerInput;
        private Animator _animator;
        private CharacterController _characterController;
        private Vector2 _currentMovementInput;
        private Vector3 _currentMovement;
        private float _rotationFactorPerFrame = 15.0f;
        private bool _isMovementPressed;
        private float _runFactor = 3.0f;
        private bool _isRunPressed;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsRunning = Animator.StringToHash("isRunning");


        private void Awake()
        {
            _animator = GetComponent<Animator>();
            
            
            _playerInput = new InputActions();
            _characterController = GetComponent<CharacterController>();
            _playerInput.CharacterController.Move.started += OnMovement;
            _playerInput.CharacterController.Move.canceled += OnMovement;
            _playerInput.CharacterController.Move.performed += OnMovement;

            _playerInput.CharacterController.Run.started += OnRun;
            _playerInput.CharacterController.Run.canceled += OnRun;
        }

        private void OnMovement(InputAction.CallbackContext context)
        {
            _currentMovementInput = context.ReadValue<Vector2>();
            _currentMovement.x = _currentMovementInput.x;
            _currentMovement.z = _currentMovementInput.y;
            _isMovementPressed = _currentMovementInput != Vector2.zero;
        }

        private void OnRun(InputAction.CallbackContext context)
        {
            _isRunPressed = context.ReadValueAsButton();
        }

        private void HandleAnimation()
        {
            var isWalking = _animator.GetBool(IsWalking);
            var isRunning = _animator.GetBool(IsRunning);

            switch (_isMovementPressed)
            {
                case true when !isWalking:
                    _animator.SetBool(IsWalking, true);
                    break;
                case false when isWalking:
                    _animator.SetBool(IsWalking, false);
                    break;
                case true when _isRunPressed && !isRunning:
                    _animator.SetBool(IsRunning, true);
                    break;
                case true when !_isRunPressed && isRunning:
                    _animator.SetBool(IsRunning, false);
                    break;
                case false when isRunning:
                    _animator.SetBool(IsRunning, false);
                    break;
                
            }
        }

        private void HandleRotation()
        {
            
            
            Vector3 positionToLookAt;

            positionToLookAt.x = _currentMovement.x;
            positionToLookAt.y = 0.0f;
            positionToLookAt.z = _currentMovement.z;

            var currentRotation = transform.rotation;

            if (_isMovementPressed)
            {
                var targetRotation = Quaternion.LookRotation(positionToLookAt);
                transform.rotation = Quaternion.Slerp(currentRotation, targetRotation, 
                    _rotationFactorPerFrame * Time.deltaTime);
            }
            

        }

        private void HandleGravity()
        {
            if (_characterController.isGrounded)
            {
                var groundedGravity = -.05f;
                _currentMovement.y = groundedGravity;
            }
            else
            {
                var groundedGravity = -9.8f;
                _currentMovement.y += groundedGravity;
            }
        }
        
        private void Update()
        {
            _characterController.Move(_currentMovement * (Time.deltaTime * (_isRunPressed ? _runFactor : 1)));
            HandleAnimation();
            HandleRotation();
            HandleGravity();
        }

        private void OnEnable()
        {
            _playerInput.CharacterController.Enable();
        }

        private void OnDisable()
        {
            _playerInput.CharacterController.Disable();
        }
    }
}
