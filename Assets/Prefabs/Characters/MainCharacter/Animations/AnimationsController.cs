using Prefabs.Characters.MainCharacter.Scripts;
using UnityEngine;

namespace Prefabs.Characters.MainCharacter.Animations
{
    public class AnimationsController : MonoBehaviour
    {
        private Agent _agent;
        private Animator _animator;
        private static readonly int IsWalking = Animator.StringToHash("isWalking");
        private static readonly int IsRunning = Animator.StringToHash("isRunning");

        private void Awake()
        {
            _agent = GetComponent<Agent>();
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            HandleAnimation();
        }

        private void HandleAnimation()
        {
            var isWalking = _animator.GetBool(IsWalking);
            var isRunning = _animator.GetBool(IsRunning);

            switch (_agent.IsMove)
            {
                case true when !isWalking:
                    _animator.SetBool(IsWalking, true);
                    break;
                case false when isWalking:
                    _animator.SetBool(IsWalking, false);
                    break;
                case true when _agent.IsRun && !isRunning:
                    _animator.SetBool(IsRunning, true);
                    break;
                case true when !_agent.IsRun && isRunning:
                    _animator.SetBool(IsRunning, false);
                    break;
                case false when isRunning:
                    _animator.SetBool(IsRunning, false);
                    break;
                
            }
        }
    }
}