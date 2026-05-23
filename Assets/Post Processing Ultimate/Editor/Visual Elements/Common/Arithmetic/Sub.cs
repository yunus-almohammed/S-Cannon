using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Sub : VisualElement {

        public const string tooltip = "Finds difference between numbers (S + LMB)";

        public Sub() {
            name = "Sub";
            sign = '-';
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