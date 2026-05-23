using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Vector2 : VisualElement {

        public const string tooltip = "Vector2 global variable";

        public _Vector2() {
            name = "_Vector2";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 2);
            AddJoint(VisualJoint.RIGHT, 1, "X value", "X value", Utils.components[0]);
            AddJoint(VisualJoint.RIGHT, 1, "Y value", "Y value", Utils.components[1]);
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