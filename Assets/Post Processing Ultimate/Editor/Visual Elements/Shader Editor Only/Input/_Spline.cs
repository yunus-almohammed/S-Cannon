using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Spline : VisualElement {

        public const string tooltip = "Spline global variable";

        public _Spline() {
            name = "_Spline";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 1);
            AddJoint(VisualJoint.LEFT, 1, "X axis", "Texture X coordinates");
            options.Add("ZERO (0)");
            Values.Add("0");
            Values.Add("0");
            Values.Add("0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleInputs(name.Substring(1));
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            if (Values[2] != "-2") {
                List<string> demands = new List<string> {
                    Generator.Demand(elements, joints[1], precision)
                };
                output.Add("SAMPLE_TEXTURE2D(_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + ", " + "sampler_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + ", " + precision + "2(" + demands[0] + ", 0.5)).a");
            } else {
                output.Add("0.0");
            }
            return output;
        }
    }
}