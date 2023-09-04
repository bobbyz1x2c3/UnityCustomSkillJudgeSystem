using UnityEngine;

namespace DataClass.Effection
{
    [CreateAssetMenu(menuName = "CustomEffect/PostureReduce")]
    public class PostureReduce : EffectBase
    {
        private EntityProps from { get; set; }
        private EntityNetWorkProps netFrom;
        [Tooltip("the coeffience of Posture damage")]
        public float Coeff { get; set; }

        public PostureReduce()
        {
            Coeff = 1;
        }

        public PostureReduce(float coeff)
        {
            Coeff = coeff;
        }
        public override int Execute(EntityProps to)
        {
            to.prop.Posture -= Coeff;
            return 0;
        }

        public override int Execute(EntityNetWorkProps to)
        {
            to.GetPostureReduce(Coeff);
            return 0;;
        }
    }
}