### UnityCustomSkillJudgeSystem
### Unity自定义技能判断系统
## A Network Custom Skill Judge System which base on Unity Netcode 
## 一个基于Unity Netcode的网络自定义技能判断系统

---
3 basic types of Effection:
    DamageCause
    BuffCause
    PostureReduce
包含3种基本Effection类型:
    伤害效果(DamageCause)
    增益效果(BuffCause)
    姿态削减(PostureReduce)
they could be found in AssetMenu at "/CustomEffect", and they are inherit from EffectionBase, which is a ScriptableObject. 
它们可以在资源菜单“/CustomEffect”中找到,并继承自EffectionBase这个ScriptableObject。

---

Each Skill(maybe named with SimpleSkill later) consists with some Effections, and when Skill is called to be Executed, the Effections will Execute by order 

每个Skill(后面可能命名为SimpleSkill)由一些Effection组成,当技能被调用执行时,Effection会按顺序执行。

Each entity has an only EntityNetworkProps (usually one player owns only one entity, which means in common condition each client has its own EntityNetworkProps, Use TotalManager.GetEntityByID(ulong clientID) to get the EntityNetworkProps belongs to the specified client)

每个实体都只有一个EntityNetworkProps(通常每个玩家只控制一个实体,也就是在普通条件下,每个客户端都有自己的EntityNetworkProps,可以通过TotalManager.GetEntityByID(ulong clientID)获取指定客户端所属的EntityNetworkProps)。


## haven't done yet but planning to do 
## 计划待做的功能
More Types of Effection
Each ComplicatedSkill could contain more types of Effections,

更多类型的Effection
每个ComplicatedSkill可以包含更多类型的Effection。