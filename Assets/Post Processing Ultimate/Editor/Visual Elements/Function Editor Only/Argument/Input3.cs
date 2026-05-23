using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Input3 : VisualElement {

        public const string tooltip = "Triple index argument";

        public Input3() {
            name = "Input3";
            tint = Utils.ElementsInput;
            jointsOffset = 2;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Input X",
                "Y Value", "Input Y",
                "Z Value", "Input Z",
                "XYZ", "Input XYZ"
            );
            options.Add("ZERO (0)");
            Values.Add("0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleInputs(name, 0);
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateInput(0);
        }
    }
}