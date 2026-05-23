using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    public class VisualElement : object {
        public Rect position = new Rect(0, 0, 96, 0);
        public string name;
        public List<VisualJoint> joints = new List<VisualJoint>();
        public bool selected;
        public List<string> Values = new List<string>();
        internal List<string> options = new List<string>();
        internal readonly List<string> names = new List<string>();
        internal int jointsOffset = 1;
        internal Color tint;
        internal Color sele = new Color(1, 0.5f, 0, 0.5f);
        internal char sign;
        internal Texture2D top;
        internal Texture2D blurry;
        internal Texture2D blurryLine;
        internal Texture2D back;
        
        internal RenderQueue renderQueue;
        internal List<VisualProperty> properties;
        internal List<List<VisualElement>> allElements;

        internal static string[] loopOptions = { "+", "-", "*", "/", "%" };

        public virtual void ReadCustomData() { }
        public virtual void Arrange() { }

        public virtual List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            return new List<string>();
        }

        internal List<string> GenerateVectorProperty() {
            List<string> output = new List<string>();
            for (int i = 0; i < joints.Count; ++i) {
                if (Values[2] != "-2") {
                    output.Add("_" + Utils.UniqueToName(properties, int.Parse(Values[2])) + joints[i].component);
                } else {
                    output.Add("0.0");
                }
            }
            return output;
        }

        internal List<string> GenerateArithmetic(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string>();
            for (int i = 1; i < joints.Count; ++i) {
                if (joints[i].Connected()) {
                    demands.Add(Generator.Demand(elements, joints[i], destinedSize, precision));
                }
            }
            if (demands.Count == 0) {
                output.Add("0.0");
            } else if (demands.Count == 1) {
                output.Add(demands[0]);
            } else {
                output.Add("(" + demands[0]);
                for (int i = 1; i < demands.Count; ++i) {
                    output[0] += " " + sign + " " + demands[i];
                }
                if (name == "Av") {
                    output[0] = "(" + output[0] + ") / " + demands.Count + ".0";
                }
                output[0] += ")";
            }
            return output;
        }

        internal List<string> GenerateData(string data) {
            List<string> output = new List<string>();
            foreach (VisualJoint joint in joints) {
                output.Add(data + joint.component);
            }
            return output;
        }

        internal List<string> GenerateAppend(List<VisualElement> elements, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string>();
            foreach (VisualJoint joint in joints) {
                if (joint.type == VisualJoint.LEFT) {
                    demands.Add(Generator.Demand(elements, joint, precision));
                }
            }
            output.Add(CombineSingleDemands(precision, demands.ToArray()));
            return output;
        }

        internal List<string> GenerateDetach(List<VisualElement> elements, string precision) {
            List<string> output = new List<string>();
            string demand = Generator.Demand(elements, joints[GetFirstLeftJointIndex()], precision);
            foreach (VisualJoint joint in joints) {
                if (joint.type == VisualJoint.RIGHT) {
                    output.Add(demand + joint.component);
                }
            }
            return output;
        }

        internal List<string> GenerateVariable(List<VisualElement> elements, string precision) {
            List<string> output = new List<string>();
            List<string> demands = new List<string>();
            int firstLeftJointIndex = GetFirstLeftJointIndex();
            foreach (VisualJoint joint in joints) {
                if (joint.type == VisualJoint.LEFT) {
                    demands.Add(Generator.Demand(elements, joint, precision));
                }
            }
            if (joints.Count == 2) {
                output.Add(demands[0]);
            } else {
                for (int i = 0; i < firstLeftJointIndex; ++i) {
                    if (joints[joints.Count - 1].Connected()) {
                        output.Add(demands[firstLeftJointIndex - 1] + joints[i].component);
                    } else if (i != firstLeftJointIndex - 1) {
                        output.Add(demands[i]);
                    } else {
                        string val = precision + joints[firstLeftJointIndex - 1].size + "(" + demands[0];
                        for (int j = 1; j < demands.Count - 1; ++j) {
                            val += ", " + demands[j];
                        }
                        val += ")";
                        val += joints[i].component;
                        output.Add(val);
                    }
                }
            }
            return output;
        }

        internal List<string> GenerateValue(string precision) {
            List<string> output = new List<string>();
            for (int i = 0; i < Values.Count; ++i) {
                if (!Values[i].ToLower().Contains("e") && !Values[i].ToLower().Contains(".")) {
                    Values[i] += ".0";
                }
            }
            if (joints.Count == 1) {
                output.Add(Values[0]);
            } else {
                for (int i = 0; i < joints.Count - 1; ++i) {
                    output.Add(Values[i]);
                }
                string val = precision + (joints.Count - 1) + "(" + Values[0];
                for (int i = 1; i < joints.Count - 1; ++i) {
                    val += ", " + Values[i];
                }
                val += ")";
                output.Add(val);
            }
            return output;
        }

        internal List<string> GenerateVarLoop() {
            List<string> output = new List<string>();
            foreach (VisualJoint joint in joints) {
                if (joint.type == VisualJoint.RIGHT) {
                    output.Add("var" + Values[0] + joint.component);
                }
            }
            return output;
        }

        internal List<string> GenerateConst() {
            return GenerateConst(name);
        }

        internal List<string> GenerateConst(string constans) {
            List<string> output = new List<string> {
                constans
            };
            return output;
        }

        internal List<string> GenerateInput(int index) {
            List<string> output = new List<string>();
            foreach (VisualJoint joint in joints) {
                if (Values[index] == "-2") {
                    output.Add("0.0");
                } else {
                    output.Add("_" + Utils.UniqueToName(properties, int.Parse(Values[index])) + joint.component);
                }
            }
            return output;
        }

        internal List<string> GenerateFunction(List<VisualElement> elements, int destinedSize, string precision, string function) {
            List<string> output = new List<string>();
            List<string> demands = new List<string>();
            for (int i = 1; i < joints.Count; ++i) {
                demands.Add(Generator.Demand(elements, joints[i], destinedSize, precision));
            }
            string val = function + "(" + demands[0];
            for (int i = 1; i < demands.Count; ++i) {
                val += ", " + demands[i];
            }
            val += ")";
            output.Add(val);
            return output;
        }

        internal List<string> GenerateMaster(List<VisualElement> elements, string precision, string masterName) {
            List<string> output = new List<string>();
            foreach (VisualJoint joint in joints) {
                if (joint.Connected()) {
                    output.Add(masterName + joint.component + " = " + Generator.Demand(elements, joint, precision) + ";");
                }
            }
            return output;
        }

        internal string CombineSingleDemands(string precision, string[] demands) {
            string val = demands[0];
            for (int i = 1; i < demands.Length; ++i) {
                val += ", " + demands[i];
            }
            return precision + demands.Length + "(" + val + ")";
        }

        internal int GetFirstLeftJointIndex() {
            for (int i = 0; i < joints.Count; ++i) {
                if (joints[i].type == VisualJoint.LEFT) {
                    return i;
                }
            }
            return -1;
        }

        internal void AddJoint(int type, int size, string name = "", string tooltip = "", string component = "") {
            joints.Add(new VisualJoint(type, size, name, tooltip, component));
        }

        internal void AddJointsGroup(int type, params string[] naming) {
            int startingJointsCount = joints.Count;
            int newJoints = naming.Length / 2;
            for (int i = 0; i < newJoints; ++i) {
                int size = 1;
                string component = "";
                if (i == newJoints - 1 && newJoints != 1) {
                    size = i;
                } else {
                    component = Utils.components[i];
                }
                AddJoint(type, size, naming[i * 2], naming[i * 2 + 1], component);
            }
            int lastIndex = joints.Count - 1;
            if (type == VisualJoint.LEFT) {
                for (int i = startingJointsCount; i < lastIndex; ++i) {
                    joints[i].prohibitions.Add(lastIndex);
                    joints[lastIndex].prohibitions.Add(i);
                }
            }
        }

        internal void AddJoints(int size, int type, string inputName = "", string inputTooltip = "Input", string outputTooltip = "Output") {
            size = size == 1 ? 0 : size;
            string[] letters = { "X", "Y", "Z", "W" };
            List<string> naming = new List<string>();
            if (type == VisualJoint.RIGHT) {
                for (int i = 0; i < size; ++i) {
                    naming.Add(letters[i] + " Value");
                    naming.Add(outputTooltip + " " + letters[i]);
                }
                naming.Add("Output");
                naming.Add(outputTooltip + " value");
            } else {
                for (int i = 0; i < size; ++i) {
                    naming.Add(letters[i] + " " + inputName);
                    naming.Add(inputTooltip + " " + letters[i]);
                }
                naming.Add(inputName);
                naming.Add(inputTooltip + " value");
            }
            AddJointsGroup(type, naming.ToArray());
        }

        internal void LoadTextures() {
            top = Utils.CreateRoundedTexture(384, 80, 40, true, true, false, false);
            blurry = Resources.Load("UI/Bottom") as Texture2D;
            blurryLine = BlurryShadow();
            back = CreateBackground();
        }

        internal void CalculateHeight() {
            position.height = 20 * (joints.Count + jointsOffset) + 10;
        }

        internal void OnHeightChange() {
            CalculateHeight();
            LoadTextures();
        }

        public virtual void Show() {
            if (top != null && blurry != null && blurryLine != null && back != null) {
                if (selected) {
                    EditorGUI.DrawRect(new Rect(position.x - 4, position.y - 4, position.width + 8, position.height + 8), sele);
                } else {
                    GUI.DrawTexture(new Rect(position.x - 5, position.y - 5, position.width + 10, 20), blurry);
                    GUI.DrawTexture(new Rect(position.x - 5, position.y + 15, position.width + 10, position.height - 30), blurryLine);
                    GUI.DrawTexture(new Rect(position.x - 5, position.y + position.height + 5, position.width + 10, -20), blurry);
                }
                GUI.DrawTexture(new Rect(position.x, position.y, 96, 20), top);
                GUI.DrawTexture(new Rect(position.x, position.y + 20, 96, position.height - 20), back);
                DrawJoints();
            } else if (tint != Color.clear) {
                LoadTextures();
                Show();
            }
        }

        internal virtual void DrawJoints() {
            EditorGUI.LabelField(new Rect(position.x, position.y, position.width, 20), new GUIContent(name, Utils.GetTooltip(name)), SharedResources.visualElementStyles[0]);
            for (int i = 0; i < joints.Count; ++i) {
                if (joints[i].type == VisualJoint.LEFT) {
                    joints[i].coords = new Vector2(position.x - Utils.jointSize - Utils.jointHorizontalOffset, Utils.jointVerticalOffset + position.y + (i + jointsOffset) * (20));
                    joints[i].Show();
                } else {
                    joints[i].coords = new Vector2(position.x + position.width + Utils.jointHorizontalOffset, Utils.jointVerticalOffset + position.y + (i + jointsOffset) * (20));
                    joints[i].Show();
                }
                EditorGUI.LabelField(new Rect(position.x + 4, (position.y + (i + jointsOffset) * (20) + 2), (position.width - 8), 16), new GUIContent(joints[i].name, joints[i].tooltip), SharedResources.visualElementStyles[joints[i].type + 1]);
            }
        }

        internal Texture2D CreateBackground() {
            Texture2D output = new Texture2D(192, (int) position.height * 2 - 40, TextureFormat.RGBA32, false, true) { wrapMode = TextureWrapMode.Clamp };
            Color color = PlayerSettings.colorSpace == ColorSpace.Linear ? tint.linear : tint;
            float maxDistance = Utils.distance(0, 0, 191, output.height - 1);
            Color dark = new Color(0.1f, 0.1f, 0.1f);
            for (int y = 0; y < 40; ++y) {
                for (int x = 0; x < 192; ++x) {
                    Color gradient = Color.Lerp(dark, Color.white, Utils.distance(x, y, 0, 0) / maxDistance);
                    gradient = PlayerSettings.colorSpace == ColorSpace.Linear ? gradient.linear : gradient;
                    int temp = 39 - y;
                    output.SetPixel(x, y, ((
                        top.GetPixel(x * 2, temp * 2) +
                        top.GetPixel(x * 2, temp * 2 + 1) +
                        top.GetPixel(x * 2 + 1, temp * 2) +
                        top.GetPixel(x * 2 + 1, temp * 2 + 1)
                    ) / 4) * color * gradient);
                }
            }
            for (int y = 40; y < output.height; ++y) {
                for (int x = 0; x < 192; ++x) {
                    Color gradient = Color.Lerp(dark, Color.white, Utils.distance(x, y, 0, 0) / maxDistance);
                    gradient = PlayerSettings.colorSpace == ColorSpace.Linear ? gradient.linear : gradient;
                    output.SetPixel(x, y, color * gradient);
                }
            }
            output.Apply();
            return output;
        }

        internal Texture2D BlurryShadow() {
            Texture2D output = new Texture2D(192, 1, TextureFormat.RGBA32, false, true) { wrapMode = TextureWrapMode.Clamp };
            for (int x = 0; x < 192; ++x) {
                output.SetPixel(x, 0, blurry.GetPixel(x, 0));
            }
            output.Apply();
            return output;
        }

        internal void AddArithmeticInput() {
            AddJoint(VisualJoint.LEFT, 0, "Value", "Input value");
        }

        internal void HandleArithmeticExpansion() {
            while (joints.Count < 3) {
                AddArithmeticInput();
                OnHeightChange();
            }
            while (joints.Count > 3 && !joints[joints.Count - 1].Connected() && !joints[joints.Count - 2].Connected()) {
                joints.RemoveAt(joints.Count - 1);
                OnHeightChange();
            }
            if (joints[joints.Count - 1].Connected()) {
                AddArithmeticInput();
                OnHeightChange();
            }
            Values[0] = joints.Count.ToString(CultureInfo.InvariantCulture);
        }

        internal void HandleInputs(string propertyName, int index = 2) {
            options.Clear();
            names.Clear();
            options.Add("-2");
            names.Add("ZERO (0)");
            names.AddRange(properties.Where(x => x.name == propertyName).Select(x => x.Values[0]));
            options.AddRange(properties.Where(x => x.name == propertyName).Select(x => x.unique.ToString()));
            Values[index] = options[EditorGUI.Popup(new Rect(position.x, position.y + 1 * 20 + 2, position.width, 16), Utils.IndexIfAvailable(options, Values[index]), names.ToArray())];
        }

        internal void HandleValue(int size) {
            for (int i = 0; i < size; ++i) {
                Values[i] = EditorGUI.FloatField(new Rect(position.x, position.y + (i + 1) * 20 + 2, position.width, 16), float.Parse(Values[i], CultureInfo.InvariantCulture)).ToString(CultureInfo.InvariantCulture);
            }
        }
    }
}