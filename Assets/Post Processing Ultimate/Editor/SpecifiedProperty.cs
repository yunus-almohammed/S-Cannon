using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {

    public class SpecifiedProperty : object {

        public List<VisualProperty> properties = new List<VisualProperty>();
        public List<string> options = new List<string> { "ZERO (0)" };
        public List<int> uniques = new List<int> { 0 };
    }
}
