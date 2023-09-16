using SkillScript.Effection;
using Unity.Netcode;
using UnityEngine;

namespace NetworkCharacter
{
    public class NetworkCharacterPropHandler : NetworkBehaviour , INetworkUpdateSystem
    {
        private EntityNetWorkProps m_prop;
        public Animator _animator;
        [SerializeField] private BuffCause Block;
        [SerializeField] private BuffRemove UnBlock;        
        [SerializeField] private float postureRecoverRate = 10f; 
        public void NetworkUpdate(NetworkUpdateStage updateStage)
        {
            if (IsServer)
            {
                var temp = m_prop.prop.Value;
                if (Shortcuts.CharacterAnimatorUtils.isAnimationDefend(_animator) ||
                    Shortcuts.CharacterAnimatorUtils.isAnimationAttack(_animator) ||
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
            m_prop = GetComponentInParent<EntityNetWorkProps>();
            this.RegisterNetworkUpdate(NetworkUpdateStage.FixedUpdate);

        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();
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