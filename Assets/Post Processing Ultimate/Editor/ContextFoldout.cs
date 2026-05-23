using System;
using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {

    [Serializable]
    public class ContextFoldout : object {

        public string name;
        public bool isActive;
        public bool isVisible = true;
        public List<ContextButton> buttons = new List<ContextButton>();

        public ContextFoldout(string name) {
            this.name = name;
        }
    }
}
