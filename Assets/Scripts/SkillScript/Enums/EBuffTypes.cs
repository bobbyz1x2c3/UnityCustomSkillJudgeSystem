namespace SkillScript.Enums
{
    /// <summary>
    ///   buff类型
    /// </summary>
    public enum EBuffTypes
    {
        
        #region 常规debuff
        atk_down_ratio,
        atk_down,
        arm_down_ratio,
        arm_down,
        mag_down_ratio,
        mag_down,
        spd_down,
        #endregion
        
        #region 常规buff
        atk_up_ratio,
        atk_up,

        arm_up_ratio,

        arm_up,
        mag_up_ratio,
        mag_up,
        spd_up,
        #endregion

        #region 特殊BUFF
        Blocking,
        /// <summary>
        /// 尽量别用，最终造成伤害 减少固定伤害
        /// </summary>
        Damage_reduce,
        /// <summary>
        /// 尽量别用，最终造成伤害变为比例
        /// </summary>
        Damage_ratio,
        /// <summary>
        /// 尽量别用，最终造成伤害 增加固定伤害
        /// </summary>
        Damage_amp



        #endregion
        
        
    }
    
    public static class BuffUtils{
        public static bool IsPositiveBuff(this EBuffTypes type)
        {
            return type == EBuffTypes.arm_up
                   || type == EBuffTypes.atk_up
                   || type == EBuffTypes.mag_up
                   || type == EBuffTypes.spd_up
                   || type == EBuffTypes.arm_up_ratio
                   || type == EBuffTypes.atk_up_ratio
                   || type == EBuffTypes.mag_up_ratio;
        }
        
        public static bool IsNegativeBuff(this EBuffTypes type)
        {
            return type == EBuffTypes.arm_down
                   || type == EBuffTypes.atk_down
                   || type == EBuffTypes.mag_down
                   || type == EBuffTypes.spd_down
                   || type == EBuffTypes.arm_down_ratio
                   || type == EBuffTypes.atk_down_ratio
                   || type == EBuffTypes.mag_down_ratio;
        }
        
        
    }
}