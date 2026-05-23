using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    public class Iterator : VisualElement {

        public const string tooltip = "Increasing value in a loop";

        public Iterator() {
            name = "Iterator";
            tint = Utils.ElementsVariable;
            AddJoint(VisualJoint.RIGHT, 1, "Output", "Iterates inside loop");
            CalculateHeight();
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return GenerateConst("IteratorVariable");
        }
    }
}