using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Add : VisualElement {

        public const string tooltip = "Combines numbers (A + LMB)";

        public Add() {
            name = "Add";
            sign = '+';
            tint = Utils.ElementsArithmetic;
            AddJoint(VisualJoint.RIGHT, 0, "Output", "Output value");
            AddArithmeticInput();
            AddArithmeticInput();
            Values.Add("0");
            CalculateHeight();
        }

        public override void Show() {
            base.Show();
            HandleArithmeticExpansion();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateArithmetic(elements, destinedSize, precision);
        }
    }
}