using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class Custom : VisualElement {

        public const string tooltip = "Custom function (C + LMB)";

        private CustomData data = new CustomData();

        public Custom() {
            name = "Custom";
            tint = Utils.ElementsCustom;
            jointsOffset = 3;
            Values.Add("EMPTY"); // path
            Values.Add(""); // function name
            Values.Add(""); // output size
            Values.Add(""); // list of input sizes
            Values.Add(""); // list of input names
            CalculateHeight();
        }

        public override void ReadCustomData() {
            if (Values[0] != "EMPTY") {
                data = DataAccess.CustomRead(System.IO.File.ReadAllText(Values[0]));
            } else {
                data = new CustomData();
            }
            Values[1] = data.Name;
            Values[2] = (data.OutputSize - 1).ToString(CultureInfo.InvariantCulture);
            Values[3] = "";
            for (int i = 0; i < data.InputSize.Count; ++i) {
                if (i != 0) {
                    Values[3] += ",";
                }
                Values[3] += data.InputSize[i].ToString(CultureInfo.InvariantCulture);
            }
            Values[4] = "";
            for (int i = 0; i < data.InputName.Count; ++i) {
                if (i != 0) {
                    Values[4] += ",";
                }
                Values[4] += data.InputName[i];
            }
        }

        public override void Arrange() {
            if (Values[0] != "EMPTY") {
                ReadCustomData();
            }
            joints.Clear();
            if (joints.Count > 0 && Values[0] == "EMPTY") {
                joints.Clear();
            }
            if (joints.Count == 0 && Values[1] != "") {
                if (data.OutputSize > 0) {
                    AddJoints(data.OutputSize, VisualJoint.RIGHT);
                }
                for (int i = 0; i < data.InputSize.Count; ++i) {
                    AddJoints(data.InputSize[i], VisualJoint.LEFT, data.InputName[i]);
                }
            }
        }

        public override void Show() {
            base.Show();
            string newFunction;
            EditorGUI.LabelField(new Rect(position.x, position.y + 1 * (20) + 2, position.width, 20), new GUIContent(Values[0], "HLSL Function path"), Values[0] == "EMPTY" ? SharedResources.visualElementStyles[4] : SharedResources.visualElementStyles[2]);
            if (GUI.Button(new Rect(position.x, position.y + 2 * (20) + 2, position.width * (Values[0] != "EMPTY" && Values[0] != null && Values[0] != "" ? 0.75f : 1), 20), new GUIContent("Select", "Select function"))) {
                selected = false;
                newFunction = EditorUtility.OpenFilePanel("Open PPU function", "Assets/Post Processing Ultimate/Functions", "hlsl");
                if (newFunction != null && newFunction != "") {
                    Values[0] = newFunction;
                    Values[0] = Values[0].Substring(Values[0].IndexOf("Assets/"));
                    Arrange();
                    OnHeightChange();
                }
            }
            if (Values[0] != "EMPTY" && Values[0] != null && Values[0] != "" && GUI.Button(new Rect((position.x + position.width * 0.75f), position.y + 2 * (20) + 2, position.width * 0.25f, 20), new GUIContent("X", "Remove function"))) {
                selected = false;
                data = new CustomData();
                Values[0] = "EMPTY";
                for (int i = 1; i < Values.Count; ++i) {
                    Values[i] = "";
                }
                Arrange();
                OnHeightChange();
            }
        }

        public override List<string> Generate(List<VisualElement> elements, int destinedSize, string precision) {
            List<string> output = new List<string>();
            List<string> jointsDemands = new List<string>();
            List<List<string>> groupedJoints = new List<List<string>>();
            List<VisualJoint> leftJoints = new List<VisualJoint>();
            for (int i = 0; i < joints.Count; ++i) {
                if (joints[i].type == VisualJoint.LEFT) {
                    leftJoints.Add(joints[i]);
                }
            }
            for (int i = 0; i < leftJoints.Count; ++i) {
                if (leftJoints[i].Connected()) {
                    jointsDemands.Add(Generator.Demand(elements, leftJoints[i], precision));
                } else {
                    jointsDemands.Add("0.0");
                }
            }
            List<string> ins = Values[3].Split(',').ToList();
            List<string> finalInputs = new List<string>();
            List<int> lastInputs = new List<int>();
            for (int i = 0; i < ins.Count; ++i) {
                finalInputs.Add("");
                lastInputs.Add(0);
            }
            int amount;
            int x = 0;
            for (int i = 0; i < ins.Count; ++i) {
                groupedJoints.Add(new List<string>());
                amount = int.Parse(ins[i]) + (ins[i] == "1" ? 0 : 1);
                for (int j = 0; j < amount; ++j) {
                    groupedJoints[groupedJoints.Count - 1].Add(jointsDemands[x]);
                    if (j == amount - 1) {
                        lastInputs[i] = x;
                    }
                    ++x;
                }
            }
            for (int i = 0; i < ins.Count; ++i) {
                if (ins[i] == "1") {
                    finalInputs[i] = groupedJoints[i][0];
                } else {
                    int parsedIn = int.Parse(ins[i]);
                    if (leftJoints[lastInputs[i]].Connected()) {
                        finalInputs[i] = groupedJoints[i][4];
                    } else {
                        finalInputs[i] = "half" + ins[i] + "(" + groupedJoints[i][0];
                        for (int j = 1; j < parsedIn; ++j) {
                            finalInputs[i] += ", " + groupedJoints[i][j];
                        }
                        finalInputs[i] += ")";
                    }
                }
            }
            string function = Values[1] + "(";
            for (int i = 0; i < ins.Count; ++i) {
                if (i != 0) {
                    function += ", ";
                }
                function += finalInputs[i];
            }
            function += ")";
            for (int i = 0; i < joints.Count; ++i) {
                if (joints[i].type == VisualJoint.RIGHT) {
                    output.Add(function + joints[i].component);
                }
            }
            return output;
        }
    }
}