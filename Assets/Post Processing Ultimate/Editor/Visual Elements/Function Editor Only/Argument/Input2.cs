using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Input2 : VisualElement {

        public const string tooltip = "Double index argument";

        public Input2() {
            name = "Input2";
            tint = Utils.ElementsInput;
            jointsOffset = 2;
            AddJointsGroup(VisualJoint.RIGHT,
                "X Value", "Input X",
                "Y Value", "Input Y",
                "XY", "Input XY"
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