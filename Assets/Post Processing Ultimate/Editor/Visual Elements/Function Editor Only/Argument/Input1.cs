using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Input1 : VisualElement {

        public const string tooltip = "Single index argument";

        public Input1() {
            name = "Input1";
            tint = Utils.ElementsInput;
            jointsOffset = 2;
            AddJoint(VisualJoint.RIGHT, 1, "X Value", "Input X");
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