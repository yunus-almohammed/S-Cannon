using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Texture2D : VisualElement {

        public const string tooltip = "Texture2D global variable";

        public _Texture2D() {
            name = "_Texture2D";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 4);
            AddJoint(VisualJoint.RIGHT, 1, "Red", "Red channel", Utils.components[0]);
            AddJoint(VisualJoint.RIGHT, 1, "Green", "Green channel", Utils.components[1]);
            AddJoint(VisualJoint.RIGHT, 1, "Blue", "Blue channel", Utils.components[2]);
            AddJoint(VisualJoint.RIGHT, 1, "Alpha", "Alpha channel", Utils.components[3]);
            AddJointsGroup(VisualJoint.LEFT,
                "X axis", "Texture X coordinates",
                "Y axis", "Texture Y coordinates",
                "XY", "Both texture coordinates"
            );
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
                    Generator.Demand(elements, joints[5], precision),
                    Generator.Demand(elements, joints[6], precision),
                    Generator.Demand(elements, joints[7], precision)
                };
                for (int i = 0; i < 5; ++i) {
                    if (joints[7].Connected()) {
                       output.Add("SAMPLE_TEXTURE2D(_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + ", " + "sampler_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + ", " + demands[2] + ")" + joints[i].component);
                    } else {
                        output.Add("SAMPLE_TEXTURE2D(_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + ", " + "sampler_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + ", " + precision + "2(" + demands[0] + "," + demands[1] + "))" + joints[i].component);
                    }
                }
            } else {
                output.Add("0.0");
            }
            return output;
        }
    }
}