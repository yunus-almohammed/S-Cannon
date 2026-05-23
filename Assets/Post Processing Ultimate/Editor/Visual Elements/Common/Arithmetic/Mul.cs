using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Mul : VisualElement {

        public const string tooltip = "Combines multipliers and the multiplicands (M + LMB)";

        public Mul() {
            name = "Mul";
            sign = '*';
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