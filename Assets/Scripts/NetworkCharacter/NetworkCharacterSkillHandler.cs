using System.Collections.Generic;
using SkillScript.Skills;
using Unity.Netcode;
using UnityEngine;

namespace NetworkCharacter
{
    public class NetworkCharacterSkillHandler : NetworkBehaviour , INetworkUpdateSystem
    {
        public List<SkillEffectionsBase> allSkillEffections;
        public NetworkAttackColliderManager TriggerManager;
        public Animator _animator;
        [SerializeField]
        public NetworkList<float> skillCoolDown;
        //public List<float> skillCoolDown;
        /// <summary>
        /// attack : 0
        /// skill1 : 1
        /// skill2 : 2
        /// skill3 : 3
        /// </summary>
        public List<string> AnimMsgParams;

        static NetworkCharacterSkillHandler()
        {
            //allSkillEffections.Add(Resources.Load<SkillEffectionsBase>("test/skill"));
        }

        public void NetworkUpdate(NetworkUpdateStage updateStage)
        {
            switch (updateStage)
            {
                case NetworkUpdateStage.FixedUpdate:
                    if (IsServer)
                    {
                        //Debug.Log(skillCoolDown);        

                        for (int i = 0; i < skillCoolDown.Count; i++)
                        {
                            skillCoolDown[i] -= skillCoolDown[i] >0 ?NetworkManager.Singleton.ServerTime.FixedDeltaTime : 0;
                        }
                    
                    }
                    break;
                case NetworkUpdateStage.Initialization:
                    
                    break;
                    
            }
            

        }

        private void OnEnable()
        {
            //_animator = GetComponentInChildren<Animator>();
            AnimMsgParams.Add(Shortcuts.AnimationKeys.Params.IS_ATTACK);
            AnimMsgParams.Add(Shortcuts.AnimationKeys.Params.IS_UPSLASH);
            skillCoolDown = new NetworkList<float>();
            skillCoolDown.Initialize(this);

        }

        private void Start()
        {
            this.RegisterNetworkUpdate(NetworkUpdateStage.FixedUpdate);
            if (IsClient && IsOwner)
            {
                skillCoolDown.OnListChanged += (changeEvent) =>
                {
                    ClientUIPanel.Instance?.SetText(skillCoolDown[0].ToString());
                };
            }
            
            if (IsOwner && IsClient)
            {
                GenerateServerRPC();
            }
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();


        }

        [ServerRpc]
        public void GenerateServerRPC()
        {
            skillCoolDown.Add(0);
        }
        public override void OnNetworkDespawn()
        {
            base.OnNetworkDespawn();
            this.UnregisterNetworkUpdate(NetworkUpdateStage.FixedUpdate);
        }

        public void InitializeAnimMsgParams()
        {
            /*AnimMsgParams = new List<string>(4)
            {
                [0] = Shortcuts.AnimationKeys.Params.IS_ATTACK,
                [1] = Shortcuts.AnimationKeys.Params.IS_UPSLASH
            };*/
            //AnimMsgParams[2] = Shortcuts.AnimationKeys.Params.IS_SKILL2;
            //AnimMsgParams[3] = Shortcuts.AnimationKeys.Params.IS_SKILL3;            
        }
        
        [ServerRpc]
        void SendAnimBoolServerRpc(string _paramName, bool _Value)
        {
            _animator.SetBool(_paramName, _Value);
        }

        [ServerRpc]
        void SetSkillCoolDownServerRpc(int _skillIndex, float _value)
        {
            skillCoolDown[_skillIndex] = _value;
        }
        public void Attack()
        {
            SendAnimBoolServerRpc(AnimMsgParams[0],  true);
        }

        public void Skill1()
        {
            if (skillCoolDown[0] <= 0)
            {
                SendAnimBoolServerRpc(AnimMsgParams[1],  true);
                SetSkillCoolDownServerRpc(0, 6f);               
            }
            

        }
        public void Skill2()
        {
            SendAnimBoolServerRpc(AnimMsgParams[2],  true);

        }

        public void Skill3()
        {
            SendAnimBoolServerRpc(AnimMsgParams[3],  true);
        }
        public void Defend(bool _isDefend)
        {
            SendAnimBoolServerRpc(Shortcuts.AnimationKeys.Params.IS_DEFEND, _isDefend);
        }

    }
}