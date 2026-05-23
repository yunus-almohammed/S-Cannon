using System;

namespace DawidMoza.PostProcessingUltimate {

    [Serializable]
    public class ContextButton : object {

        public string name;
        public bool isVisible;

        public ContextButton(string name) {
            this.name = name;
        }
    }
}
