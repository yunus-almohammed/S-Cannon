using System;
using System.Collections.Generic;

namespace DawidMoza.PostProcessingUltimate {
    [Serializable]
    public class Settings : object {

        public List<string> Values = new List<string>();

        public Settings() {
            Values.Add("0");
            Values.Add(Utils.shaderName + "/MyShader");
            Values.Add("_MainTex");
            Values.Add("0");
            Values.Add("");
        }
    }
}
