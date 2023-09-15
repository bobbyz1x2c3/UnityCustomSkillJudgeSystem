using System;
using SkillScript.Effection;
using Unity.Netcode;
using UnityEngine;

namespace NetworkCharacterPropManager
{
    public class NetworkCharacterPropManager : NetworkBehaviour , INetworkUpdateSystem
    {
        private EntityNetWorkProps m_prop;
        private MovementStatusNetworkLateSync  m_movementStatus;
        [SerializeField] private BuffCause Block;
        [SerializeField] private BuffRemove UnBlock;        
        [SerializeField] private float postureRecoverRate = 10f; 
        public void NetworkUpdate(NetworkUpdateStage updateStage)
        {
            if (IsServer)
            {
                var temp = m_prop.prop.Value;
                if (Shortcuts.CharacterAnimatorUtils.isAnimationDefend(m_movementStatus._animator) ||
                    Shortcuts.CharacterAnimatorUtils.isAnimationAttack(m_movementStatus._animator) ||
                    temp.Posture >= temp.maxPosture)
                {
                    return;
                }
                
                temp.Posture += NetworkManager.Singleton.ServerTime.FixedDeltaTime * postureRecoverRate;
                m_prop.prop.Value = temp;                
            }
            
            
        }

        private void Start()
        {
            m_movementStatus = GetComponentInChildren<MovementStatusNetworkLateSync>();
            m_prop = GetComponent<EntityNetWorkProps>();
            
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
            this.RegisterNetworkUpdate(NetworkUpdateStage.FixedUpdate);
        }

        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            this.UnregisterAllNetworkUpdates();
        }

        public void StartBlock()
        {
            if (IsOwner && IsClient)
            {
                m_prop.EffectRequest(Block, m_prop.id.Value);
            }
        }

        public void EndBlock()
        {
            if (IsOwner && IsClient)
            {
                m_prop.EffectRequest(UnBlock, m_prop.id.Value);
            }
        }
    }
}