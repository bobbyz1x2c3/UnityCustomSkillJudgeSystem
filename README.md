___A Network Custom Skill Judge System which base on Unity Netcode___

一个基于Unity Netcode的网络自定义技能判断系统
--
---


## 游戏系统设计
>  ##  __实体属性__
>> __实体类型__
>>>  区分队伍，人物，npc
>>
>> __各项战斗属性__
>>> 如生命值/防御力等
>>
>> __携带buff__
>    
> ## __职能天赋树__
> 朝四个方向职责发展，在战场中的职责理论上应当由前到后
> 
> 但一个人可以承担多种职责
>> ### __中坚__
>>
>>> 吸引仇恨\
>>> 各类防御技能
>>
>> ### __架势破坏者__
>>
>>> 架势削减\
>>> 牵制手段
>>
>> ### __战斗专家__
>>
>>> BUFF\
>>> DEBUFF
>>
>> ### __救援者__
>>
>>> 生命值恢复\
>>> 架势恢复
> ## __武器分类__
> 
>>  __近战长器械类__\
>>  __法术类__\
>>  我也想有更多...
> ## __怪物__
> 


# 代码架构
> ## EntityNetworkProp.cs
> 负责实体战斗属性
>> ## TotalManager.cs
>> 以后会改名，负责管理场景中EntityNetworkProp
> 
>> ## NetworkCharacter/
>> 这个namespace里的代码只在玩家实体上绑定
>>> ### ~InputHandler.cs
>>> 接受处理输入，并传给下列Handler
>>>> ### ~MovementHandler.cs
>>>> 负责控制角色移动
>>>
>>>> ### ~PropHandler.cs
>>>> 玩家战斗属性相关
>>>
>>>> ### ~SkillHandler.cs
>>>> 玩家技能
>>>>> #### NetworkAttackColliderManager.cs
>>>>>
>>>>> 目前只跟animator的事件联动，临时写的，以后会改名。\
>>>>> 以后要有一个独立的技能管理系统，skillHandler只负责触发对应技能动画
>>>>>> #### SkillEffectTriggerCarrier.cs
>>>>>> 携带技能效果，绑在Trigger所在的GameObject上，以后应当由技能管理系统获得Skill的效果
>

> ## Skill
> 每一个技能

        
---
    5 basic types of Effection:
        DamageCause
        BuffCause
        PostureReduce
        HealCause
        BuffRemove
        
    5种基本Effection类型:
        伤害效果(DamageCause)
        施加增/减益效果(BuffCause)
        姿态削减(PostureReduce)
        治疗效果(HealCause)
        移除增/减益效果(BuffRemove)

they could be found in AssetMenu at "/CustomEffect", and they are inherit from EffectionBase, which is a ScriptableObject. 
它们可以在资源菜单“/CustomEffect”中找到,并继承自EffectionBase这个ScriptableObject。


> ## haven't done yet but planning to do 
> ## 计划待做的功能
>> ### 技能管理系统
>>> 统一管理技能效果，职能天赋树

