using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Animation;

namespace AnimationWPF
{
    public class EasingFunctionsManager
    {
        private static IEnumerable<EasingFunctionBase> s_easingFunctions = new List<EasingFunctionBase>()
        {
            new BackEase(),
            new BounceEase(),
            new CircleEase(),
            new CubicEase(),
            new ElasticEase(),
            new ExponentialEase(),
            new PowerEase(),
            new QuadraticEase(),
            new QuinticEase(),
            new SineEase(),
        };

        public IEnumerable<EasingFunctionModel> EasingFunctionModels =>
            s_easingFunctions.Select(f => new EasingFunctionModel(f));

    }
}
