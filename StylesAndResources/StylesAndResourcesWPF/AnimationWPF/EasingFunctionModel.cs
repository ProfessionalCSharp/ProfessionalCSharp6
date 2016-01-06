using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace AnimationWPF
{
    public class EasingFunctionModel
    {
        public EasingFunctionModel(EasingFunctionBase easingFunction)
        {
            EasingFunction = easingFunction;
        }

        public EasingFunctionBase EasingFunction { get; }

        public override string ToString() => EasingFunction.GetType().Name;
    }
}
