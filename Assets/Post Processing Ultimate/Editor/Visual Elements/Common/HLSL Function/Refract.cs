using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Refract : VisualElement {

        public const string tooltip = "Returns a refraction vector using an entering ray, a surface normal, and a refraction index";

        public Refract() {
            name = "Refract";
            tint = Utils.ElementsHLSL;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddJoint(VisualJoint.LEFT, 0, "I Value", "A floating-point, ray direction vector");
            AddJoint(VisualJoint.LEFT, 0, "N Value", "A floating-point, surface normal vector");
            AddJoint(VisualJoint.LEFT, 0, "n Value", "A floating-point, refraction index scalar");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateFunction(elements, destinedSize, precision, name.ToLower());
        }
    }
}