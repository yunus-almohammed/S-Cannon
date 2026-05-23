using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Fmod : VisualElement {

        public const string tooltip = "Returns the floating-point remainder of x/y";

        public Fmod() {
            name = "Fmod";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "X Value", "The floating-point dividend");
            AddJoint(VisualJoint.LEFT, 0, "Y Value", "The floating-point divisor");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}