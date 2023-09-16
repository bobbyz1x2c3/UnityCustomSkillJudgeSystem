using System;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

namespace NetworkCharacter
{
    public class NetworkCharacterInputHandler : NetworkBehaviour
    {
        private NetworkCharacterMovementHandler m_MovementHandler;
        private NetworkCharacterPropHandler m_PropHandler;
        private NetworkCharacterSkillHandler m_skillHandler;
        public Animator _animator;
        private bool DefendMode = false;
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            m_MovementHandler = GetComponent<NetworkCharacterMovementHandler>();
            m_MovementHandler._animator = _animator;
            m_PropHandler = GetComponent<NetworkCharacterPropHandler>();
            m_PropHandler._animator = _animator;            
            m_skillHandler = GetComponent<NetworkCharacterSkillHandler>();
            m_skillHandler._animator = _animator;            
        }
        public void OnMove(InputValue value)
        {
            if (IsOwner && IsClient)
            {
                m_MovementHandler.lastInputFaceValue = value.Get<Vector2>();
            
            }

        }
        public void OnAttack(InputValue value)
            {
                var a = value.Get<float>();
                
                
                if (IsOwner && IsClient && a>0)
                {
                    //attackCancelCheck = a > 0;
                    m_skillHandler.Attack();
                    //SendAnimBoolServerRpc(Shortcuts.AnimationKeys.PARAM_IS_ATTACK, a>0);
                }
            }
        
            public void OnAttack2(InputValue value)
            {
                if (
                    IsOwner && 
                    IsClient 
                    )
                {
                    /*SendAnimTriggerServerRpc("Attack");
                    _animator.SetTrigger("Attack");*/
                    //SendAnimBoolServerRpc(Shortcuts.AnimationKeys.PARAM_IS_UPSLASH, true);
                    m_skillHandler.Skill1();
                }
            }
            public void OnDefend(InputValue value)
            {
                var a = value.Get<float>();
                DefendMode = a > 0;
        
                if (IsOwner && IsClient)
                {
                    //DefendServerRpc(DefendMode); 
                    
                    //SendAnimBoolServerRpc(Shortcuts.AnimationKeys.PARAM_IS_DEFEND, DefendMode);
                    
                    //_animator.SetBool("isDefend",DefendMode);
                    m_skillHandler.Defend(DefendMode);
                }
        
        
            }
            public void OnDefend_Switch(InputValue value)
            {
                DefendMode = !DefendMode;
                if (IsOwner && IsClient)
                {
                    //DefendServer,Rpc(DefendMode); 
                    
                    //SendAnimBoolServerRpc(Shortcuts.AnimationKeys.PARAM_IS_DEFEND, DefendMode);
                    
                    //_animator.SetBool("isDefend",DefendMode);
                    m_skillHandler.Defend(DefendMode);
                }
            }
        
    }
}