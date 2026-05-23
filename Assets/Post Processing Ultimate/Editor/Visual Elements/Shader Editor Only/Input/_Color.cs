using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class _Color : VisualElement {

        public const string tooltip = "Color global variable";

        public _Color() {
            name = "_Color";
            tint = Utils.ElementsInput;
            AddJoint(VisualJoint.RIGHT, 4);
            AddJoint(VisualJoint.RIGHT, 1, "Red", "Red channel", Utils.components[0]);
            AddJoint(VisualJoint.RIGHT, 1, "Green", "Green channel", Utils.components[1]);
            AddJoint(VisualJoint.RIGHT, 1, "Blue", "Blue channel", Utils.components[2]);
            AddJoint(VisualJoint.RIGHT, 1, "Alpha", "Alpha channel", Utils.components[3]);
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