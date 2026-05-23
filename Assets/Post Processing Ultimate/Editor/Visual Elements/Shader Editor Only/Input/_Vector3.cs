using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Vector3 : VisualElement {

        public const string tooltip = "Vector3 global variable";

        public _Vector3() {
            name = "_Vector3";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 3);
            AddJoint(VisualJoint.RIGHT, 1, "X value", "X value", Utils.components[0]);
            AddJoint(VisualJoint.RIGHT, 1, "Y value", "Y value", Utils.components[1]);
            AddJoint(VisualJoint.RIGHT, 1, "Z value", "Z value", Utils.components[2]);
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