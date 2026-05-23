using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Vector4 : VisualElement {

        public const string tooltip = "Vector4 global variable";

        public _Vector4() {
            name = "_Vector4";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 4);
            AddJoint(VisualJoint.RIGHT, 1, "X value", "X value", Utils.components[0]);
            AddJoint(VisualJoint.RIGHT, 1, "Y value", "Y value", Utils.components[1]);
            AddJoint(VisualJoint.RIGHT, 1, "Z value", "Z value", Utils.components[2]);
            AddJoint(VisualJoint.RIGHT, 1, "W value", "W value", Utils.components[3]);
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
            return GenerateVectorProperty();
        }
    }
}