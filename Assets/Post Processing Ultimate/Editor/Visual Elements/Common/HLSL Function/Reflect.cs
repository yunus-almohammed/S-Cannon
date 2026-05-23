using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Reflect : VisualElement {

        public const string tooltip = "Returns a reflection vector using an incident ray and a surface normal (LAlt + R + LMB)";

        public Reflect() {
            name = "Reflect";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "I Value", "A floating-point, incident vector");
            AddJoint(VisualJoint.LEFT, 0, "N Value", "A floating-point, normal vector");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}