using System.Collections.Generic;
using System.Linq;

namespace DawidMoza.PostProcessingUltimate {

    public class RenderQueue : object {

        public int tempTextures;
        public List<VisualPass> passes = new List<VisualPass>();
        public List<string> passOptions = new List<string>();
        public List<string> inputOptions = new List<string>();
        public List<string> outputOptions = new List<string>();
        public List<string> variableOptions = new List<string>();

        internal List<string> uniques = new List<string>();
        internal List<VisualProperty> properties;
        internal List<List<VisualElement>> elements;

        public void Show() {
            passOptions.Clear();
            for (int i = 0; i < elements.Count; ++i) {
                passOptions.Add(i.ToString());
            }
            tempTextures = elements.Count - 1;
            if (inputOptions.Count != tempTextures + 1) {
                inputOptions.Clear();
                outputOptions.Clear();
                inputOptions.Add("Game");
                outputOptions.Add("Screen");
                for (int i = 0; i < tempTextures; ++i) {
                    inputOptions.Add("tempRT" + i);
                    outputOptions.Add("tempRT" + i);
                }
            }
            variableOptions.Clear();
            variableOptions.Add("None");
            variableOptions.AddRange(properties.Where(x => x.name == "Int" || x.name == "IntSlider")
                .Select(x => x.Values[0])
                .ToList());
            uniques.Clear();
            uniques.Add("0");
            uniques.AddRange(properties.Where(x => x.name == "Int" || x.name == "IntSlider")
                .Select(x => x.unique.ToString())
                .ToList());
            passes.ForEach(x => x.Show());
        }
    }
}
