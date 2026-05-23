using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    internal static class DataAccess {

        internal static string Save(List<List<VisualElement>> allElements, Settings settings, List<VisualProperty> properties, RenderQueue renderQueue, bool functionEditor) {
            string project = "//";
            project += JsonUtility.ToJson(settings);
            if (!functionEditor) {
                for (int i = 0; i < properties.Count; ++i) {
                    properties[i].position.y = 0; //ReorderableList handle this by itself
                    project += "|" + JsonUtility.ToJson(properties[i]);
                }
                project += "|" + JsonUtility.ToJson(renderQueue);
            }
            for (int i = 0; i < allElements.Count; ++i) {
                project += "\n//";
                for (int j = 0; j < allElements[i].Count; ++j) {
                    project += "\\\t" + allElements[i][j].name;
                    project += "\\\t" + allElements[i][j].position.x.ToString(CultureInfo.InvariantCulture) + "\\\t" + allElements[i][j].position.y.ToString(CultureInfo.InvariantCulture) + "\\\t" + allElements[i][j].position.width + "\\\t" + allElements[i][j].position.height;
                    for (int k = 0; k < allElements[i][j].Values.Count; ++k) {
                        project += "\\\t\t/" + allElements[i][j].Values[k];
                    }
                    for (int k = 0; k < allElements[i][j].joints.Count; ++k) {
                        bool worthSaving = allElements[i][j].joints[k].Connected() && allElements[i][j].joints[k].type == VisualJoint.LEFT;
                        project += "\\\t\t" + worthSaving;
                        if (worthSaving) {
                            project += "\\\t\t\t" + allElements[i][j].joints[k].connectedSerial[0] + "\\\t\t\t" + allElements[i][j].joints[k].connectedSerial[1];
                        } else {
                            project += "\\\t\t\tnull";
                            project += "\\\t\t\tnull";
                        }
                    }
                }
            }
            if (functionEditor) {
                project += "\n//";
                CustomData customData = new CustomData {
                    Name = settings.Values[1],
                    OutputSize = int.Parse(allElements[0][0].Values[0]) + 1
                };
                foreach (VisualProperty property in properties) {
                    customData.InputName.Add(property.Values[0]);
                    customData.InputSize.Add(int.Parse(property.name.Substring(5)));
                }
                project += JsonUtility.ToJson(customData);
            }
            project += "\n\n";
            return project;
        }

        internal static Settings SettingsRead(string database) {
            database = database.Substring(2, database.IndexOf("}") - 1);
            return JsonUtility.FromJson<Settings>(database);
        }

        internal static CustomData CustomRead(string database) {
            CustomData custom = null;
            List<string> lines;
            if (database != "") {
                lines = database.Split('\n').Skip(2).ToList();
                database = lines[0].Substring(2);
                custom = JsonUtility.FromJson<CustomData>(database);
            }
            return custom;
        }

        internal static List<VisualProperty> PropertiesRead(string database) {
            List<VisualProperty> basics = new List<VisualProperty>();
            List<VisualProperty> properties = new List<VisualProperty>();
            List<string> lines;
            if (database.IndexOf('|') > 0) {
                database = database.Substring(database.IndexOf('|'), database.IndexOf('\n') - database.IndexOf('|'));
                lines = database.Split('|').Skip(1).ToList();
                for (int i = 0; i < lines.Count - 1; ++i) {
                    basics.Add(JsonUtility.FromJson<VisualProperty>(lines[i]));
                }
                for (int i = 0; i < basics.Count; ++i) {
                    properties.Add(Utils.CreateProperty(basics[i].name + "Field"));
                    properties[i].position = basics[i].position;
                    properties[i].selected = basics[i].selected;
                    properties[i].Values = basics[i].Values;
                    properties[i].unique = basics[i].unique;
                }
            }
            return properties;
        }

        internal static RenderQueue RenderRead(string database) {
            RenderQueue renderQueue = new RenderQueue();
            List<string> lines;
            if (database.IndexOf('|') > 0) {
                database = database.Substring(database.IndexOf('|'), database.IndexOf('\n') - database.IndexOf('|'));
                lines = database.Split('|').ToList();
                renderQueue = JsonUtility.FromJson<RenderQueue>(lines[lines.Count - 1]);
            }
            return renderQueue;
        }

        internal static List<List<VisualElement>> ElementsRead(string database, bool functionEditor) {
            List<List<VisualElement>> elements = new List<List<VisualElement>>();
            string[] data;
            database = database.Replace("\r", "");
            database = database.Substring(0, database.IndexOf("\n\n"));
            data = database.Split('\n').Skip(1).ToArray();
            if (functionEditor) {
                elements.Add(Elements(data[0]));
            } else {
                foreach (string line in data) {
                    elements.Add(Elements(line));
                }
            }
            return elements;
        }

        internal static List<VisualElement> Elements(string database) {
            List<VisualElement> elements = new List<VisualElement>();
            List<string> lines = database.Split('\\').ToList();
            List<int> newElements = new List<int>();
            string[] arithmetic = { "Add", "Av", "Sub", "Mul", "Div", "Mod" };
            for (int i = 1; i < lines.Count; ++i) {
                if (i == 1 || (lines[i][1] != '\t' && lines[i - 1].Length > 2 && (lines[i - 1][2] == '\t' || lines[i - 1][2] == '/'))) {
                    string name = lines[i].Substring(1);
                    if (name.Contains("Custom") && name != "Custom") {
                        name = "Custom";
                    }
                    elements.Add(Utils.Create(name));
                    newElements.Add(i);
                }
            }
            for (int i = 0; i < elements.Count; ++i) {
                elements[i].position.x = float.Parse(lines[newElements[i] + 1].Substring(1), CultureInfo.InvariantCulture);
                elements[i].position.y = float.Parse(lines[newElements[i] + 2].Substring(1), CultureInfo.InvariantCulture);
                for (int j = 0; j < elements[i].Values.Count; ++j) {
                    elements[i].Values[j] = lines[newElements[i] + 5 + j].Substring(3);
                }
                if (elements[i].name.Contains("Custom")) {
                    elements[i].Arrange();
                    elements[i].CalculateHeight();
                }
                if (elements[i].name == "Output") {
                    elements[i].Arrange();
                }
                if (arithmetic.Contains(elements[i].name)) {
                    elements[i].joints.RemoveRange(1, elements[i].joints.Count - 1);
                    for (int j = 1; j < (int) float.Parse(elements[i].Values[0], CultureInfo.InvariantCulture); ++j) {
                        elements[i].AddArithmeticInput();
                    }
                    elements[i].CalculateHeight();
                }
            }
            for (int i = 0; i < elements.Count; ++i) {
                for (int j = 0; j < elements[i].joints.Count; ++j) {
                    if (!lines[newElements[i] + 6 + elements[i].Values.Count + j * 3].Contains("null") && elements[i].joints[j].type == 0) {
                        if (((int) float.Parse(lines[newElements[i] + 7 + elements[i].Values.Count + j * 3].Substring(3), CultureInfo.InvariantCulture)) < elements[(int) float.Parse(lines[newElements[i] + 6 + elements[i].Values.Count + j * 3].Substring(3), CultureInfo.InvariantCulture)].joints.Count) {
                            elements[i].joints[j].connectedSerial[0] = (int) float.Parse(lines[newElements[i] + 6 + elements[i].Values.Count + j * 3].Substring(3), CultureInfo.InvariantCulture);
                            elements[i].joints[j].connectedSerial[1] = (int) float.Parse(lines[newElements[i] + 7 + elements[i].Values.Count + j * 3].Substring(3), CultureInfo.InvariantCulture);
                        }
                    }
                }
            }
            return elements;
        }
    }
}