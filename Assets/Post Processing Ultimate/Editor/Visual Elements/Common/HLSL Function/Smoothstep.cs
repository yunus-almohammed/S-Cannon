using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Smoothstep : VisualElement {

        public const string tooltip = "Returns a smooth Hermite interpolation between 0 and 1, if x is in the range [min, max]";

        public Smoothstep() {
            name = "Smoothstep";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "Min", "The minimum range of the x parameter");
            AddJoint(VisualJoint.LEFT, 0, "Max", "The maximum range of the x parameter");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The specified value to be interpolated");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}