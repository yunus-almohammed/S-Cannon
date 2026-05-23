using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {
    public class VisualEditor : EditorWindow, ISerializationCallbackReceiver {

        internal VisualElement focusedElement;
        internal VisualJoint focusedJoint;
        internal Vector2 mouseCoords;
        internal Texture2D background;
        internal bool copyTime = false;
        internal VisualJoint tempJoint = new VisualJoint(VisualJoint.TEMP_JOINT, 0) { serial = new int[] { VisualJoint.TEMP_JOINT, VisualJoint.TEMP_JOINT } };
        internal bool selecting;
        internal Rect selectingBox = new Rect(0, 0, 0, 0);
        internal EventType lastEvent;
        internal float scale = 1.0f;
        internal string[] options = new string[] { "Half", "Float" };
        internal string[] targets = new string[] { "After Stack", "Before Stack", "Before Transparent" };
        internal string path;
        internal string oldPath;
        internal string state;
        internal string lastDir;
        internal int lineColor = 5;
        internal Matrix4x4 defaultGUI;
        internal float offset;
        internal Vector2 contextPosition = new Vector2(0, 0);
        internal Vector2 contextScroll = new Vector2(0, 0);
        internal Vector2 toolbarScroll = new Vector2(0, 0);
        internal string search = "Search...";
        internal string search2 = "Search...";
        internal bool context;
        internal int lastHeight = 64;
        internal bool output;
        internal List<Texture2D> toolbarTextures = new List<Texture2D>();
        internal List<VisualJoint> loose = new List<VisualJoint>();
        internal List<Vector3> points = new List<Vector3>();
        internal bool scroll;
        internal List<Vector2> distance = new List<Vector2>() { Vector2.zero, Vector2.zero, Vector2.zero };
        internal float pointsDistance;
        internal RenderQueue renderQueue = new RenderQueue();
        internal List<VisualProperty> properties = new List<VisualProperty>();
        internal ReorderableList inputs;
        internal string[] propNames;
        internal int newProp = 0;
        internal int toolbarHeight = 9;
        internal int propertiesHeight = 0;
        internal int pass = 0;
        internal int currentPass = 0;
        internal List<List<VisualElement>> elements = new List<List<VisualElement>>();
        internal Settings settings = new Settings();
        internal List<KeyCode> pressed = new List<KeyCode>();
        internal Dictionary<int, Vector2> offsets = new Dictionary<int, Vector2>();
        internal GUIStyle toolbarStyle = new GUIStyle();
        internal GUIStyle centeredStyle = new GUIStyle();
        internal UserSettings uSets;
        internal bool searching;
        internal string serialization;
        internal List<ContextFoldout> contextFoldouts = new List<ContextFoldout>();
        internal bool isFunctionEditor;
        internal int recordedStatesPosition;
        internal List<string> recordedStates = new List<string>();
        internal List<List<Texture2D>> arrows = new List<List<Texture2D>>();
        internal Texture2D line;

        internal virtual void Awake() {
            renderQueue.properties = properties;
            renderQueue.elements = elements;
            LoadSettings();
            if (path == null) {
                New();
            }
            toolbarTextures.Add(Resources.Load("UI/Bin") as Texture2D);
            toolbarTextures.Add(Resources.Load("UI/New") as Texture2D);
            toolbarTextures.Add(Resources.Load("UI/Open") as Texture2D);
            toolbarTextures.Add(Resources.Load("UI/SaveAs") as Texture2D);
            toolbarTextures.Add(Resources.Load("UI/Save") as Texture2D);
            toolbarStyle.alignment = TextAnchor.MiddleCenter;
            toolbarStyle.clipping = TextClipping.Clip;
            centeredStyle.alignment = TextAnchor.MiddleCenter;
            centeredStyle.normal.textColor = Color.white;
            background = Resources.Load("Backgrounds/BigBackground") as Texture2D;
            if (elements[pass].Count == 0) {
                New();
            }
            SerializeJoints();
            List<ContextFoldout> common = Utils.GetFoldersAndFiles("Visual Elements/Common");
            List<ContextFoldout> additional = Utils.GetFoldersAndFiles("Visual Elements/" + (isFunctionEditor ? "Function" : "Shader") + " Editor Only");
            contextFoldouts = Utils.MergeContextFoldouts(common, additional);
            propNames = Utils.GetFileNames("Visual Properties/" + (isFunctionEditor ? "Function" : "Shader") + " Editor Only");
            for (int i = 0; i < propNames.Length; ++i) {
                propNames[i] = propNames[i].Substring(0, propNames[i].IndexOf("Field"));
            }
            AssetDatabase.Refresh();
        }

        internal void New() {
            elements.Clear();
            elements.Add(new List<VisualElement>());
            pass = 0;
            currentPass = 0;
            AddStartingElements();
            CheckConnections();
            properties.Clear();
            renderQueue.passes.Clear();
            renderQueue.passes.Add(new VisualPass(renderQueue));
            OrganizePasses();
            path = null;
            recordedStates.Clear();
            recordedStatesPosition = 0;
            RecordState();
        }

        internal void AddStartingElements() {
            if (isFunctionEditor) {
                AddElement("Output");
                elements[pass][0].position.x = 272 + 1 * 300;
                elements[pass][0].position.y = 48;
                AddElement("Input1");
                elements[pass][1].position.x = 272;
                elements[pass][1].position.y = 48;
                SerializeJoints();
                Connect(elements[pass][0].joints[0], elements[pass][1].joints[0]);
                settings.Values[1] = "MyFunction";
                settings.Values[2] = "_MainTex";
            } else {
                AddElement("CameraOutput");
                elements[pass][0].position.x = 272 + 2 * 300;
                elements[pass][0].position.y = 48;
                AddElement("CameraInput");
                elements[pass][1].position.x = 272 + 1 * 300;
                elements[pass][1].position.y = 48;
                AddElement("StereoUV");
                elements[pass][2].position.x = 272;
                elements[pass][2].position.y = 48;
                SerializeJoints();
                Connect(elements[pass][1].joints[3], elements[pass][0].joints[3]);
                Connect(elements[pass][2].joints[2], elements[pass][1].joints[6]);
                settings.Values[1] = Utils.shaderName + "/MyShader";
            }
        }

        internal void Open() {
            GUIUtility.keyboardControl = 0;
            oldPath = path;
            if (isFunctionEditor) {
                path = EditorUtility.OpenFilePanel("Open PPU function", lastDir, "hlsl");
            } else { 
                path = EditorUtility.OpenFilePanel("Open PPU shader", lastDir, "shader");
            }
            path = path == "" ? null : path;
            if (path != null) {
                lastDir = path.Substring(0, path.LastIndexOf('/') + 1);
                try {
                    OnBeforeSerialize();
                    string database = System.IO.File.ReadAllText(path);
                    LoadData(database);
                    RecordState();
                    if (!isFunctionEditor) {
                        pass = 0;
                        currentPass = 0;
                    }
                } catch {
                    path = oldPath;
                    OnAfterDeserialize();
                    Debug.Log("Invalid file");
                    return;
                }
                oldPath = path;
            } else {
                path = oldPath;
            }
        }

        internal void SaveAs() {
            SerializeJoints();
            oldPath = path;
            string fileName = settings.Values[1].Contains("/") ? settings.Values[1].Substring(settings.Values[1].LastIndexOf('/') + 1) : settings.Values[1];
            if (isFunctionEditor) {
                path = EditorUtility.SaveFilePanel("Save PPU Function", lastDir, fileName, "hlsl");
            } else {
                path = EditorUtility.SaveFilePanel("Save PPU Shader", lastDir, fileName, "shader");
            }
            if (path != null && path != "") {
                Generator.Generate(elements, settings, properties, path, renderQueue, isFunctionEditor);
                oldPath = path;
                lastDir = path.Substring(0, path.LastIndexOf('/') + 1);
            } else {
                path = oldPath;
            }
        }

        internal void Save() {
            SerializeJoints();
            if (path != null && path != "") {
                Generator.Generate(elements, settings, properties, path, renderQueue, isFunctionEditor);
            }
        }

        internal void SelectionHandle() {
            switch (Event.current.type) {
                case EventType.MouseUp:
                selecting = false;
                selectingBox = new Rect(0, 0, 0, 0);
                break;
            }
        }

        // Should be performed only on left type joints
        internal VisualJoint ConnectedTo(VisualJoint joint, int index) {
            int x = joint.connectedSerial[0];
            int y = joint.connectedSerial[1];
            if (x >= 0 && x < elements[index].Count && y >= 0 && y < elements[index][x].joints.Count) {
                return elements[index][x].joints[y];
            } else if (joint.ConnectedToTempJoint()) {
                return tempJoint;
            }
            return null;
        }

        internal bool IsAboveElement(VisualElement element) {
            return mouseCoords.x >= element.position.x && mouseCoords.x <= element.position.x + element.position.width && mouseCoords.y >= element.position.y && mouseCoords.y <= element.position.y + element.position.height;
        }

        internal bool IsAboveJoint(VisualJoint joint) {
            float leftBorder = joint.type == 0 ? joint.coords.x : joint.coords.x - Utils.jointHorizontalOffset;
            float rightBorder = joint.type == 0 ? joint.coords.x + Utils.jointSize + Utils.jointHorizontalOffset : joint.coords.x + Utils.jointSize;
            float upperBorder = joint.coords.y + Utils.jointSize + Utils.jointVerticalOffset;
            float lowerBorder = joint.coords.y - Utils.jointVerticalOffset;
            return mouseCoords.x >= leftBorder && mouseCoords.x <= rightBorder && mouseCoords.y >= lowerBorder && mouseCoords.y <= upperBorder;
        }

        internal void MouseDrag() {
            if (Event.current.button == 0) {
                if (mouseCoords.x > contextPosition.x - 5 && mouseCoords.x < (contextPosition.x + 133) / scale && mouseCoords.y > contextPosition.y + 26 && mouseCoords.y < (contextPosition.y + 217) / scale) {
                    contextPosition.x += Event.current.delta.x;
                    contextPosition.y += Event.current.delta.y;
                } else if (focusedElement != null) {
                    focusedElement.position.x += Event.current.delta.x;
                    focusedElement.position.y += Event.current.delta.y;
                }
                if (focusedJoint != null) {
                    //Disconnecting
                    if (focusedJoint.type == VisualJoint.LEFT && focusedJoint.Connected() && !focusedJoint.ConnectedToTempJoint()) {
                        VisualJoint used = ConnectedTo(focusedJoint, pass);
                        Disconnect(focusedJoint, used);
                        RecordState();
                        focusedJoint = used;
                    }
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        for (int j = 0; j < elements[pass][i].joints.Count; ++j) {
                            if (IsAboveJoint(elements[pass][i].joints[j])) {
                                if (focusedJoint.serial[0] != i && focusedJoint.type != elements[pass][i].joints[j].type) {
                                    lineColor = 5;
                                    if (elements[pass][i].joints[j].type == VisualJoint.LEFT) {
                                        loose.Add(elements[pass][i].joints[j]);
                                        for (int k = 0; k < elements[pass][i].joints[j].prohibitions.Count; ++k) {
                                            if (loose.Count < elements[pass][i].joints[j].prohibitions.Count + 1) {
                                                loose.Add(elements[pass][i].joints[elements[pass][i].joints[j].prohibitions[k]]);
                                            }
                                        }
                                    } else {
                                        for (int k = 0; k < focusedJoint.prohibitions.Count; ++k) {
                                            if (loose.Count < focusedJoint.prohibitions.Count + 1) {
                                                loose.Add(elements[pass][focusedJoint.serial[0]].joints[elements[pass][focusedJoint.serial[0]].joints[focusedJoint.serial[1]].prohibitions[k]]);
                                            }
                                        }
                                    }
                                }
                                break;
                            } else {
                                lineColor = -1;
                                loose.Clear();
                            }
                        }
                        if (lineColor == 5) {
                            break;
                        }
                    }
                    tempJoint.coords = mouseCoords;
                    if (!tempJoint.Connected()) {
                        Connect(focusedJoint, tempJoint);
                    }
                }
                if (focusedElement == null && focusedJoint == null && mouseCoords.y > 0 && !(mouseCoords.x > contextPosition.x && mouseCoords.x < contextPosition.x + 128 / scale && mouseCoords.y > contextPosition.y && mouseCoords.y < contextPosition.y + 212 / scale)) {
                    selectingBox.width = -1 * (selectingBox.x - mouseCoords.x);
                    selectingBox.height = -1 * (selectingBox.y - mouseCoords.y);
                    selecting = true;
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        if (selectingBox.Overlaps(elements[pass][i].position, true)) {
                            elements[pass][i].selected = true;
                        } else {
                            elements[pass][i].selected = false;
                        }
                    }
                }
                lastEvent = EventType.MouseDrag;
                if (!(NothingSelected() || copyTime || selecting) && uSets.SwitchMMB) {
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        if (elements[pass][i].selected && elements[pass][i] != focusedElement) {
                            elements[pass][i].position.x += Event.current.delta.x;
                            elements[pass][i].position.y += Event.current.delta.y;
                        }
                    }
                }
            } else if (Event.current.button == 2) {
                if (NothingSelected() || copyTime || uSets.SwitchMMB) {
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        elements[pass][i].position.x += Event.current.delta.x;
                        elements[pass][i].position.y += Event.current.delta.y;
                    }
                } else {
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        if (elements[pass][i].selected) {
                            elements[pass][i].position.x += Event.current.delta.x;
                            elements[pass][i].position.y += Event.current.delta.y;
                        }
                    }
                }
            }
        }

        internal void MouseMove() {
            if (copyTime) {
                foreach (KeyValuePair<int, Vector2> offset in offsets) {
                    elements[pass][offset.Key].position.x = offset.Value.x + mouseCoords.x;
                    elements[pass][offset.Key].position.y = offset.Value.y + mouseCoords.y;
                }
            }
        }

        internal void MouseDown() {
            if (Event.current.button == 0) {
                Shortcut();
                selectingBox.x = mouseCoords.x;
                selectingBox.y = mouseCoords.y;
                selecting = false;
                if (!(mouseCoords.x > contextPosition.x && mouseCoords.x < contextPosition.x + 128 / scale && mouseCoords.y > contextPosition.y && mouseCoords.y < contextPosition.y + 212 / scale)) {
                    context = false;
                }
                for (int i = 0; i < elements[pass].Count; ++i) {
                    if (IsAboveElement(elements[pass][i])) {
                        focusedElement = elements[pass][i];
                    } else {
                        for (int j = 0; j < elements[pass][i].joints.Count; ++j) {
                            if (IsAboveJoint(elements[pass][i].joints[j])) {
                                focusedJoint = elements[pass][i].joints[j];
                            }
                        }
                    }
                }
                lastEvent = EventType.MouseDown;
            }
        }

        internal void MouseUp() {
            if (Event.current.button == 0) {
                if (mouseCoords.x > contextPosition.x && mouseCoords.x < contextPosition.x + 128 / scale && mouseCoords.y > contextPosition.y && mouseCoords.y < contextPosition.y + 21 / scale && search == "Search...") {
                    search = "";
                } else if (search == "") {
                    search = "Search...";
                }
                lineColor = -1;
                loose.Clear();
                if (lastEvent != EventType.MouseDrag) {
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        elements[pass][i].selected = IsAboveElement(elements[pass][i]);
                    }
                }
                if (tempJoint.Connected()) {
                    Disconnect(tempJoint, focusedJoint);
                }
                for (int i = 0; i < elements[pass].Count; ++i) {
                    for (int j = 0; j < elements[pass][i].joints.Count; ++j) {
                        if (focusedJoint != null) {
                            if (IsAboveJoint(elements[pass][i].joints[j])) {
                                Connect(focusedJoint, elements[pass][i].joints[j]);
                                if (elements[pass][i].joints[j].type == VisualJoint.LEFT) {
                                    for (int k = 0; k < elements[pass][i].joints[j].prohibitions.Count; ++k) {
                                        Disconnect(elements[pass][i].joints[elements[pass][i].joints[j].prohibitions[k]]);
                                    }
                                } else {
                                    for (int k = 0; k < focusedJoint.prohibitions.Count; ++k) {
                                        Disconnect(elements[pass][focusedJoint.serial[0]].joints[focusedJoint.prohibitions[k]]);
                                    }
                                }
                                RecordState();
                                break;
                            }
                        }
                    }
                }
                AssetDatabase.Refresh();
                focusedJoint = null;
                focusedElement = null;
                CheckConnections();
                lastEvent = EventType.MouseUp;
                if (copyTime) {
                    copyTime = false;
                    for (int i = 0; i < elements[pass].Count; ++i) {
                        elements[pass][i].selected = false;
                    }
                    RecordState();
                }
                if (uSets.AutoSave) {
                    Save();
                }
            }
        }

        internal void MouseLeaveWindow() {
            selecting = false;
        }

        internal void ContextClick() {
            if (!copyTime) {
                context = true;
                contextPosition = mouseCoords;
            }
            if (copyTime) {
                copyTime = false;
                DeleteSelected();
            }
        }

        internal void ScrollWheel() {
            if (!(mouseCoords.x > contextPosition.x && mouseCoords.x < contextPosition.x + 128 / scale && mouseCoords.y > contextPosition.y && mouseCoords.y < contextPosition.y + 212 / scale) || !context) {
                scroll = true;
                if (Event.current.delta.y > 0) {
                    scale -= 0.1f;
                } else {
                    scale += 0.1f;
                }
                scale = Mathf.Clamp(Utils.RoundWithSteps(scale, 0.1f), 0.2f, 2.0f);
            }
        }

        internal void DragUpdated() {
            string expected = isFunctionEditor ? ".hlsl" : ".shader";
            if (DragAndDrop.paths[0].Contains(expected)) {
                DragAndDrop.visualMode = DragAndDropVisualMode.Generic;
            }
        }

        internal void DragExited() {
            string expected = isFunctionEditor ? ".hlsl" : ".shader";
            if (DragAndDrop.paths[0].Contains(expected)) {
                path = DragAndDrop.paths[0];
                if (path != null) {
                    if (!isFunctionEditor) {
                        pass = 0;
                        currentPass = 0;
                    }
                    oldPath = path;
                    string database = System.IO.File.ReadAllText(path);
                    LoadData(database);
                    RecordState();
                }
            }
        }

        internal void KeyDown() {
            if (Event.current.keyCode != KeyCode.None && (pressed.Count == 0 || (pressed.Count > 0 && pressed[pressed.Count - 1] != Event.current.keyCode))) {
                pressed.Add(Event.current.keyCode);
            }
            switch (Event.current.keyCode) {
                case KeyCode.Delete:
                    DeleteSelected();
                    RecordState();
                break;
            }
        }

        internal void KeyUp() {
            for (int i = 0; i < pressed.Count; ++i) {
                if (Event.current.keyCode == KeyCode.Delete && uSets.AutoSave) {
                    Save();
                }
                if (pressed[i] == Event.current.keyCode) {
                    pressed.RemoveAt(i);
                    break;
                }
            }
        }

        internal void CopyElements() {
            List<int[]> serials = new List<int[]>();
            Dictionary<int, int> indexMapping = new Dictionary<int, int>();
            int countBeforeCopying = elements[pass].Count;
            for (int i = 1; i < countBeforeCopying; ++i) {
                if (elements[pass][i].selected) {
                    VisualElement element = elements[pass][i];
                    element.selected = false;
                    elements[pass].Add(Utils.Create(element.name));
                    int index = elements[pass].Count - 1;
                    VisualElement newElement = elements[pass].Last();
                    newElement.selected = true;
                    newElement.position = element.position;
                    newElement.Values.Clear();
                    for (int j = 0; j < element.Values.Count; ++j) {
                        newElement.Values.Add(element.Values[j]);
                    }
                    indexMapping.Add(i, index);
                    newElement.joints.Clear();
                    for (int j = 0; j < element.joints.Count; ++j) {
                        VisualJoint joint = element.joints[j];
                        newElement.joints.Add(new VisualJoint(joint.type, joint.size, joint.name, joint.tooltip, joint.component));
                        newElement.joints.Last().prohibitions = new List<int>(joint.prohibitions);
                        newElement.joints.Last().serial = new int[] { index , j };
                        if (joint.type == VisualJoint.LEFT && joint.Connected()) {
                            serials.Add(new int[] { i, j, joint.connectedSerial[0], joint.connectedSerial[1] });
                        }
                    }
                }
            }
            foreach (int[] serial in serials) {
                if (indexMapping.ContainsKey(serial[2])) {
                    Connect(elements[pass][indexMapping[serial[0]]].joints[serial[1]], elements[pass][indexMapping[serial[2]]].joints[serial[3]]);
                }
            }
            if (elements[pass].Count > countBeforeCopying) {
                VisualElement furthermostElement = elements[pass][countBeforeCopying];
                for (int i = countBeforeCopying + 1; i < elements[pass].Count; ++i) {
                    if (elements[pass][i].position.x < furthermostElement.position.x) {
                        furthermostElement = elements[pass][i];
                    }
                }
                offsets.Clear();
                for (int i = countBeforeCopying; i < elements[pass].Count; ++i) {
                    VisualElement thisElement = elements[pass][i];
                    offsets.Add(i, new Vector2(thisElement.position.x - furthermostElement.position.x, thisElement.position.y - furthermostElement.position.y));
                }
                copyTime = true;
            }
        }

        internal void HandleNonMouseEvents() {
            switch (Event.current.type) {
                case EventType.Repaint:
                    return;
                case EventType.KeyDown:
                    KeyDown();
                    break;
                case EventType.KeyUp:
                    KeyUp();
                    break;
                case EventType.ValidateCommand:
                    switch (Event.current.commandName) {
                        case "Copy":
                            CopyElements();
                            break;
                    }
                    break;
            }
            Repaint();
        }

        internal void HandleMouseEvents() {
            mouseCoords.x = Event.current.mousePosition.x;
            mouseCoords.y = Event.current.mousePosition.y;
            if (mouseCoords.x * scale < 228 * uSets.ToolbarScale && Event.current.type != EventType.MouseMove) {
                return;
            }
            switch (Event.current.type) {
                case EventType.Repaint:
                    return;
                case EventType.MouseDrag:
                    MouseDrag();
                break;
                case EventType.MouseMove:
                    MouseMove();
                    if (!copyTime) {
                        return;
                    }
                break;
                case EventType.MouseDown:
                    MouseDown();
                break;
                case EventType.MouseUp:
                    MouseUp();
                break;
                case EventType.MouseLeaveWindow:
                    MouseLeaveWindow();
                break;
                case EventType.ContextClick:
                    ContextClick();
                break;
                case EventType.ScrollWheel:
                    ScrollWheel();
                break;
                case EventType.DragUpdated:
                    DragUpdated();
                break;
                case EventType.DragExited:
                    try {
                        DragExited();
                    } catch {
                        Debug.Log("Wrong file");
                    }
                break;
            }
            Repaint();
        }

        private void RecordState() {
            string currentState = DataAccess.Save(elements, settings, properties, renderQueue, isFunctionEditor);
            if (recordedStates.Count == 0 || !currentState.Equals(recordedStates[recordedStatesPosition])) {
                if (recordedStatesPosition < recordedStates.Count - 1) {
                    recordedStates.RemoveRange(recordedStatesPosition + 1, recordedStates.Count - recordedStatesPosition - 1);
                }
                recordedStates.Add(currentState);
                recordedStatesPosition = recordedStates.Count - 1;
            }
            if (uSets.AutoSave) {
                Save();
            }
        }

        internal void DeleteSelected() {
            List<int> deleted = new List<int>();
            Dictionary<int, int> elementsMapping = new Dictionary<int, int>();
            for (int i = 1; i < elements[pass].Count; ++i) {
                if (elements[pass][i].selected) {
                    deleted.Add(i);
                }
            }
            for (int i = 0; i < elements[pass].Count; ++i) {
                if (!elements[pass][i].selected) {
                    elementsMapping.Add(i, GetElementIndexAfterDeletionOfPreviousElements(i, deleted));
                }
                for (int j = 0; j < elements[pass][i].joints.Count; ++j) {
                    if (elements[pass][i].joints[j].type == VisualJoint.LEFT && elements[pass][i].joints[j].Connected()) {
                        if (deleted.Contains(elements[pass][i].joints[j].connectedSerial[0])) {
                            Disconnect(elements[pass][i].joints[j]);
                        }
                    }
                }
            }
            foreach (VisualElement element in elements[pass]) {
                foreach (VisualJoint joint in element.joints) {
                    if (joint.type == VisualJoint.LEFT && joint.Connected()) {
                        joint.connectedSerial[0] = elementsMapping[joint.connectedSerial[0]];
                    }
                }
            }
            foreach (int index in deleted) {
                elements[pass].RemoveAt(GetElementIndexAfterDeletionOfPreviousElements(index, deleted));
            }
            foreach (int val in elementsMapping.Values) {
                foreach (VisualJoint joint in elements[pass][val].joints) {
                    joint.serial[0] = val;
                }
            }
            CheckConnections();
        }

        internal int GetElementIndexAfterDeletionOfPreviousElements(int index, List<int> deleted) {
            int difference = 0;
            foreach (int deletedIndex in deleted) {
                if (deletedIndex < index) {
                    ++difference;
                }
            }
            return index - difference;
        }

        internal bool NothingSelected() {
            for (int i = 0; i < elements[pass].Count; ++i) {
                if (elements[pass][i].selected) {
                    return false;
                }
            }
            return true;
        }

        internal void CheckConnections() {
            for (int i = 0; i < elements.Count; ++i) {
                CheckConnections(i);
            }
        }

        internal void CheckConnections(int index) {
            // Disconnecting all left type joints connected with other left type joints
            for (int i = 0; i < elements[index].Count; ++i) {
                for (int j = 0; j < elements[index][i].joints.Count; ++j) {
                    if (elements[index][i].joints[j].type == VisualJoint.LEFT && elements[index][i].joints[j].Connected() && ConnectedTo(elements[index][i].joints[j], index).type == VisualJoint.LEFT) {
                        DisconnectWithoutChecking(elements[index][i].joints[j], ConnectedTo(elements[index][i].joints[j], index));
                    }
                }
            }
            // Disconnecting all right joints
            for (int i = 0; i < elements[index].Count; ++i) {
                for (int j = 0; j < elements[index][i].joints.Count; ++j) {
                    if (elements[index][i].joints[j].type == VisualJoint.RIGHT) {
                        DisconnectWithoutChecking(elements[index][i].joints[j]);
                    }
                }
            }
            // Setting right type joints to connected state
            for (int i = 0; i < elements[index].Count; ++i) {
                for (int j = 0; j < elements[index][i].joints.Count; ++j) {
                    if (elements[index][i].joints[j].type == VisualJoint.LEFT && elements[index][i].joints[j].Connected()) {
                        VisualJoint connected = ConnectedTo(elements[index][i].joints[j], index);
                        if (connected != tempJoint) {
                            connected.connectedSerial[0] = VisualJoint.RIGHT_CONNECTED;
                            connected.connectedSerial[1] = VisualJoint.RIGHT_CONNECTED;
                        }
                    }
                }
            }
            // Setting state of right type joint connected with tempJoint to right connected
            if (index == pass && tempJoint.Connected()) {
                ConnectedTo(tempJoint, index).connectedSerial = new int[] { VisualJoint.TEMP_JOINT, VisualJoint.TEMP_JOINT };
            }
        }

        internal void DrawLine(Vector2 vec1, Vector2 vec2, int type1, int type2, float width) {
            Vector3[] vec = { Vector3.zero, Vector3.zero, Vector3.zero, Vector3.zero };
            vec[0] = new Vector3(vec1.x + Utils.jointSize / 2, vec1.y + Utils.jointSize / 2, 0);
            vec[1] = new Vector3(vec2.x + Utils.jointSize / 2, vec2.y + Utils.jointSize / 2, 0);
            //When making bezier curve there are lines with rectangles at the end, here are coords of those rectangles
            float horizontalDistance = Mathf.Abs(vec[0].x - vec[1].x);
            horizontalDistance /= Mathf.Pow(1 + horizontalDistance / 3000, 2);
            float verticalDistance = Mathf.Abs(vec[0].y - vec[1].y);
            verticalDistance /= Mathf.Pow(1 + verticalDistance / 1000, 2);
            float rectanglesOffset = Mathf.Max(horizontalDistance, verticalDistance) / 1.4f;
            vec[2] = new Vector3(vec[0].x + rectanglesOffset, vec[0].y, 0);
            vec[3] = new Vector3(vec[1].x - rectanglesOffset, vec[1].y, 0);
            points = Handles.MakeBezierPoints(vec[0], vec[1], vec[2], vec[3], 32).ToList();
            for (int i = 0; i < points.Count - 1; ++i) {
                Vector3[] twoPoints = new Vector3[2];
                Handles.color = Color.Lerp(Utils.sizeColors[type2], Utils.sizeColors[type1], i / 32f);
                twoPoints[0] = points[i];
                twoPoints[1] = points[i + 1];
                if (line == null) {
                    line = SharedResources.Line();
                }
                Handles.DrawAAPolyLine(line, width, twoPoints);
            }
            Handles.color = Color.white;
        }

        internal void Connect(VisualJoint joint1, VisualJoint joint2) {
            joint1.connectedSerial[0] = joint2.serial[0];
            joint1.connectedSerial[1] = joint2.serial[1];
            joint2.connectedSerial[0] = joint1.serial[0];
            joint2.connectedSerial[1] = joint1.serial[1];
            CheckConnections();
        }

        internal void Disconnect(VisualJoint joint1, VisualJoint joint2) {
            Disconnect(joint1);
            Disconnect(joint2);
        }

        internal void Disconnect(VisualJoint joint) {
            DisconnectWithoutChecking(joint);
            CheckConnections();
        }

        internal void DisconnectWithoutChecking(VisualJoint joint1, VisualJoint joint2) {
            DisconnectWithoutChecking(joint1);
            DisconnectWithoutChecking(joint2);
        }

        internal void DisconnectWithoutChecking(VisualJoint joint) {
            joint.connectedSerial[0] = VisualJoint.NOT_CONNECTED;
            joint.connectedSerial[1] = VisualJoint.NOT_CONNECTED;
        }

        internal void SerializeJoints() {
            for (int i = 0; i < elements.Count; ++i) {
                for (int j = 0; j < elements[i].Count; ++j) {
                    for (int k = 0; k < elements[i][j].joints.Count; ++k) {
                        VisualJoint current = elements[i][j].joints[k];
                        current.serial[0] = j;
                        current.serial[1] = k;
                    }
                }
            }
            CheckConnections();
        }

        internal void DrawBeziers() {
            int arrowColor;
            for (int i = 0; i < elements[pass].Count; ++i) {
                for (int j = 0; j < elements[pass][i].joints.Count; ++j) {
                    if (elements[pass][i].joints[j].type == VisualJoint.LEFT && elements[pass][i].joints[j].Connected() && !elements[pass][i].joints[j].ConnectedToTempJoint()) {
                        DrawLine(ConnectedTo(elements[pass][i].joints[j], pass).coords, elements[pass][i].joints[j].coords, elements[pass][i].joints[j].size, ConnectedTo(elements[pass][i].joints[j], pass).size, 8);
                    }
                }
            }
            if (tempJoint.Connected()) {
                if (lineColor != -1) {
                    arrowColor = lineColor;
                } else {
                    arrowColor = focusedJoint.size;
                }
                if (arrows.Count == 0 || arrows[0] == null || arrows[0].Count == 0) {
                    arrows = SharedResources.VisualEditorArrows();
                }
                if (focusedJoint.type == VisualJoint.LEFT) {
                    DrawLine(tempJoint.coords - (new Vector2(6, 6)), focusedJoint.coords, arrowColor, arrowColor, 8);
                    if (arrows[0][arrowColor] == null) {
                        arrows = SharedResources.VisualEditorArrows();
                    }
                    GUI.DrawTexture(new Rect(tempJoint.coords.x - Utils.jointSize / 2, tempJoint.coords.y - Utils.jointSize / 2, Utils.jointSize, Utils.jointSize), arrows[0][arrowColor]);
                } else {
                    DrawLine(focusedJoint.coords, tempJoint.coords - (new Vector2(6, 6)), arrowColor, arrowColor, 8);
                    if (arrows[1][arrowColor] == null) {
                        arrows = SharedResources.VisualEditorArrows();
                    }
                    GUI.DrawTexture(new Rect(tempJoint.coords.x - Utils.jointSize / 2, tempJoint.coords.y - Utils.jointSize / 2, Utils.jointSize, Utils.jointSize), arrows[1][arrowColor]);
                }
            }
            for (int i = 0; i < loose.Count; ++i) {
                if (loose[i] != null && loose[i].Connected()) {
                    DrawLine(ConnectedTo(loose[i], pass).coords, loose[i].coords, 6, 6, 12);
                }
            }
        }

        internal void DrawElements() {
            for (int i = 0; i < elements[pass].Count; ++i) {
                elements[pass][i].Show();
            }
        }

        internal void CheckIfLastPropertyIsUnique() {
            for (int i = 0; i < properties.Count - 1; ++i) {
                if (properties[properties.Count - 1].unique == properties[i].unique) {
                    properties[properties.Count - 1].unique = Random.Range(1, 4096);
                    CheckIfLastPropertyIsUnique();
                    break;
                }
            }
        }

        internal void OrganizePasses() {
            if (elements.Count > 0) {
                if (pass >= elements.Count) {
                    pass = elements.Count - 1;
                }
                if (pass != currentPass) {
                    copyTime = false;
                    DeleteSelected();
                    currentPass = pass;
                    if (elements[pass].Count == 0) {
                        AddStartingElements();
                    }
                }
            }
        }

        internal void SetReorderableList() {
            inputs = new ReorderableList(properties, typeof(VisualProperty), true, false, false, false) {
                headerHeight = 0,
                footerHeight = 0,
                showDefaultBackground = false,
                drawHeaderCallback = (Rect rect) => {
                    EditorGUI.LabelField(rect, "Parameters");
                },
                drawElementCallback = (Rect rect, int index, bool isActive, bool isFocused) => {
                    if (properties.Count > index) {
                        properties[index].position.y = rect.y;
                        properties[index].Show();
                        if (GUI.Button(new Rect(0, properties[index].position.y + properties[index].position.height - 8, 212, 16), new GUIContent("DELETE", "Delete this input"))) {
                            properties.RemoveAt(index);
                            RecordState();
                        }
                    }
                },
                elementHeightCallback = (int index) => {
                    return properties.Count > index ? properties[index].position.height + 12 : 0;
                }
            };
        }

        internal void DrawToolbar() {
            if (uSets.ToolbarScale != 1.0f) {
                GUIUtility.ScaleAroundPivot(Vector2.one * uSets.ToolbarScale, Vector2.zero);
            }
            Color textColor = Utils.TextColor(EditorGUIUtility.isProSkin);
            toolbarStyle.normal.textColor = textColor;
            EditorGUI.DrawRect(new Rect(0, 0, 228, position.height), Utils.BackgroundColor(EditorGUIUtility.isProSkin));
            toolbarScroll = GUI.BeginScrollView(new Rect(0, 0, 228, position.height / uSets.ToolbarScale), toolbarScroll, new Rect(0, 0, 228, toolbarHeight), false, false, GUIStyle.none, GUIStyle.none);
            toolbarHeight = 9;
            //=========================================================================================
            if (GUI.Button(new Rect(8, toolbarHeight, 212, 36), new GUIContent(toolbarTextures[1], "Reset current window"))) {
                New();
            }
            if (GUI.Button(new Rect(8, toolbarHeight += 36, 212, 36), new GUIContent(toolbarTextures[2], "Open file"))) {
                Open();
            }
            if (GUI.Button(new Rect(8, toolbarHeight += 36, 212, 36), new GUIContent(toolbarTextures[3], "Write to a new file"))) {
                SaveAs();
            }
            if (GUI.Button(new Rect(8, toolbarHeight += 36, 212, 36), new GUIContent(toolbarTextures[4], "Overwrite current file"))) {
                if (path != null && path != "") {
                    Save();
                } else {
                    SaveAs();
                }
            }
            toolbarHeight += 36;
            //=========================================================================================
            toolbarHeight += 9;
            toolbarStyle.alignment = TextAnchor.MiddleCenter;
            EditorGUI.DrawRect(new Rect(8, toolbarHeight, 212, 3), textColor);
            toolbarHeight += 9;
            if (!(path == null || path == "")) {
                toolbarStyle.alignment = TextAnchor.MiddleRight;
            }
            string pathText = path == null || path == "" ? "NO FILE IS SELECTED" : path;
            EditorGUI.LabelField(new Rect(8, toolbarHeight, 212, 18), new GUIContent(pathText, pathText), toolbarStyle);
            toolbarHeight += 25;
            toolbarStyle.alignment = TextAnchor.MiddleLeft;
            //=========================================================================================
            EditorGUI.DrawRect(new Rect(8, toolbarHeight, 212, 3), textColor);
            toolbarHeight += 9;
            if (!isFunctionEditor) {
                EditorGUI.LabelField(new Rect(8, toolbarHeight, 96, 18), new GUIContent("EVENT:", "Destined Injection Point"), toolbarStyle);
                settings.Values[0] = EditorGUI.Popup(new Rect(96, toolbarHeight, 124, 16), int.Parse(settings.Values[0]), targets).ToString(CultureInfo.InvariantCulture);
                toolbarHeight += 18;
            }
            EditorGUI.LabelField(new Rect(8, toolbarHeight, 96, 18), new GUIContent("NAME:", "Name of shader/function"), toolbarStyle);
            settings.Values[1] = EditorGUI.TextField(new Rect(96, toolbarHeight, 124, 16), settings.Values[1]).Replace(" ", "");
            EditorGUI.LabelField(new Rect(8, toolbarHeight += 18, 96, 18), new GUIContent("PRECISION:", "Destined precision"), toolbarStyle);
            settings.Values[3] = EditorGUI.Popup(new Rect(96, toolbarHeight, 124, 16), int.Parse(settings.Values[3]), options).ToString(CultureInfo.InvariantCulture);
            if (!isFunctionEditor) {
                EditorGUI.LabelField(new Rect(8, toolbarHeight += 18, 96, 18), new GUIContent("PASS:", "Select shader pass (subshader) for editing"), toolbarStyle);
                pass = EditorGUI.Popup(new Rect(96, toolbarHeight, 124, 16), pass, renderQueue.passOptions.ToArray());
                toolbarHeight += 18;
                if (renderQueue.passOptions.Count == 1) {
                    if (GUI.Button(new Rect(8, toolbarHeight, 212, 16), new GUIContent("CREATE PASS", "Create new pass (subshader)"))) {
                        CreatePass();
                    }
                } else {
                    if (GUI.Button(new Rect(8, toolbarHeight, 98, 16), new GUIContent("REMOVE PASS", "Delete current pass (subshader)"))) {
                        RemovePass();
                    }
                    if (GUI.Button(new Rect(122, toolbarHeight, 98, 16), new GUIContent("CREATE PASS", "Create new pass (subshader)"))) {
                        CreatePass();
                    }
                }
            }
            toolbarHeight += 25;
            //=========================================================================================
            if (!isFunctionEditor) {
                EditorGUI.DrawRect(new Rect(8, toolbarHeight, 212, 3), textColor);
                toolbarHeight += 9;
                toolbarStyle.alignment = TextAnchor.MiddleCenter;
                EditorGUI.LabelField(new Rect(8 + 28, toolbarHeight, 65, 16), new GUIContent("INPUT", "Input texture to be processed"), toolbarStyle);
                EditorGUI.LabelField(new Rect(8 + 28 + 69, toolbarHeight, 65, 16), new GUIContent("OUTPUT", "Testure with result"), toolbarStyle);
                EditorGUI.LabelField(new Rect(8 + 28 + 138, toolbarHeight, 46, 16), new GUIContent("PASS", "Which pass (subshader) should be used"), toolbarStyle);
                toolbarHeight += 18;
                if (renderQueue.passes.Count == 1) {
                    renderQueue.passes[0].position.y = toolbarHeight;
                    GUI.enabled = false;
                    GUI.Button(new Rect(8, toolbarHeight + 5, 24, 24), new GUIContent("", toolbarTextures[0], "Delete this pass from the queue"));
                    GUI.enabled = true;
                    toolbarHeight += 36;
                } else {
                    for (int i = 0; i < renderQueue.passes.Count; ++i) {
                        renderQueue.passes[i].position.y = toolbarHeight;
                        if (GUI.Button(new Rect(8, toolbarHeight + 5, 24, 24), new GUIContent("", toolbarTextures[0], "Delete this pass from the queue"))) {
                            renderQueue.passes.RemoveAt(i);
                            RecordState();
                            break;
                        }
                        toolbarHeight += 36;
                    }
                }
                renderQueue.Show();
                if (GUI.Button(new Rect(8, toolbarHeight, 212, 16), new GUIContent("ADD PASS", "Add pass to the render queue"))) {
                    renderQueue.passes.Add(new VisualPass(renderQueue));
                    OrganizePasses();
                    RecordState();
                }
                toolbarHeight += 25;
            }
            //=========================================================================================
            EditorGUI.DrawRect(new Rect(8, toolbarHeight, 212, 3), textColor);
            toolbarHeight += 9;
            if (inputs == null || inputs.count != properties.Count) {
                SetReorderableList();
            }
            GUILayout.BeginArea(new Rect(8, toolbarHeight, 212, propertiesHeight));
            inputs.DoLayoutList();
            GUILayout.EndArea();
            propertiesHeight = 0;
            for (int i = 0; i < properties.Count; ++i) {
                propertiesHeight += (int) properties[i].position.height + 8 + 4 + 2;
            }
            toolbarHeight += propertiesHeight + 4;
            newProp = EditorGUI.Popup(new Rect(8, toolbarHeight, 98, 16), newProp, propNames);
            if (GUI.Button(new Rect(122, toolbarHeight, 98, 16), new GUIContent("ADD INPUT", "Add global variable"))) {
                properties.Add(Utils.CreateProperty(propNames[newProp] + "Field"));
                CheckIfLastPropertyIsUnique();
                RecordState();
            }
            toolbarHeight += 24;
            if (isFunctionEditor && path != null && path.Contains(".")) {
                toolbarStyle.wordWrap = true;
                EditorGUI.LabelField(new Rect(8, toolbarHeight, 212, 96), "Please keep in mind that changing a signature of a function " +
                    "that is already used in shaders requires rebulding all that shaders manually and changing " +
                    "connections to the Custom nodes holding the function", toolbarStyle);
                toolbarStyle.wordWrap = false;
                toolbarHeight += 96 + 16;
            }
            GUI.EndScrollView();
        }
        
        private void CreatePass() {
            elements.Add(new List<VisualElement>());
            pass = elements.Count - 1;
            OrganizePasses();
            RecordState();
            recordedStates.RemoveRange(recordedStates.Count - 4, 3);
            recordedStatesPosition = recordedStates.Count - 1;
        }

        private void RemovePass() {
            elements.RemoveAt(pass);
            pass = 0;
            OrganizePasses();
            RecordState();
        }

        internal void DrawSelection() {
            if (selecting && !context) {
                EditorGUI.DrawRect(selectingBox, new Color(1, 1, 1, 0.5f));
            }
        }

        internal void DrawContext(Vector2 coords) {
            if (!context) {
                contextPosition = new Vector2(-128, 0);
                foreach (ContextFoldout contextFoldout in contextFoldouts) {
                    contextFoldout.isActive = false;
                }
            }
            if (context && Event.current.type != EventType.ContextClick) {
                Color textColor = Utils.TextColor(EditorGUIUtility.isProSkin);
                GUIStyle foldout = Utils.PrepareTextColors(new GUIStyle(EditorStyles.foldout), textColor);
                float visibleHeight = Mathf.Clamp(position.height - coords.y, 19, 196);
                GUI.DrawTexture(new Rect(coords.x - 10, coords.y - 10, 148, 232), Resources.Load("Backgrounds/Context") as Texture2D);
                search2 = search;
                search = GUI.TextField(new Rect(coords.x, coords.y, 128, 16), search);
                if (search != search2) {
                    foreach (ContextFoldout contextFoldout in contextFoldouts) {
                        contextFoldout.isActive = false;
                    }
                    contextScroll.y = 0;
                }
                searching = search != "Search..." && search != "";
                contextScroll = GUI.BeginScrollView(new Rect(coords.x, coords.y + 16, 128, visibleHeight), contextScroll, new Rect(0, 0, 128, lastHeight), false, false, GUIStyle.none, GUIStyle.none);
                lastHeight = 16 + 18 * contextFoldouts.Count;
                EditorGUI.DrawRect(new Rect(contextScroll.x, contextScroll.y, 128, visibleHeight), Utils.BackgroundColor(EditorGUIUtility.isProSkin));
                CheckSearch();
                foreach (ContextFoldout contextFoldout in contextFoldouts) {
                    if (contextFoldout.isVisible) { 
                        contextFoldout.isActive = EditorGUILayout.Foldout(contextFoldout.isActive, contextFoldout.name, foldout);
                        if (contextFoldout.isActive) { 
                            foreach (ContextButton contextButton in contextFoldout.buttons) {
                                if (contextButton.isVisible) {
                                    if (GUILayout.Button(new GUIContent(contextButton.name, Utils.GetTooltip(contextButton.name)), GUILayout.Width(122))) {
                                        AddElement(contextButton.name);
                                        context = false;
                                    }
                                    lastHeight += 21;
                                }
                            }
                        }
                    }
                }
                GUI.EndScrollView();
            }
        }

        internal void CheckSearch() {
            foreach (ContextFoldout contextFoldout in contextFoldouts) {
                contextFoldout.isVisible = true;
                if (searching) {
                    contextFoldout.isActive = true;
                    bool isAnyButtonUseful = false;
                    foreach (ContextButton contextButton in contextFoldout.buttons) {
                        contextButton.isVisible = Utils.ContainsEachOther(contextButton.name, search);
                        if (contextButton.isVisible) {
                            isAnyButtonUseful = true;
                        }
                        contextButton.isVisible = contextFoldout.isActive && contextButton.isVisible;
                    }
                    if (!isAnyButtonUseful) {
                        contextFoldout.isVisible = false;
                        contextFoldout.isActive = false;
                    }
                } else {
                    foreach (ContextButton contextButton in contextFoldout.buttons) {
                        contextButton.isVisible = true;
                    }
                }
            }
        }

        internal void BeforeScroll() {
            scroll = false;
            distance[0] = new Vector2(scale * mouseCoords.x - elements[pass][0].position.x, scale * mouseCoords.y - elements[pass][0].position.y);
            distance[2] = new Vector2(scale, 0);
        }

        internal void AfterScroll() {
            if (scroll) {
                scroll = false;
                distance[1] = new Vector2(scale * mouseCoords.x - elements[pass][0].position.x, scale * mouseCoords.y - elements[pass][0].position.y);
                distance[0] = (distance[1] - distance[0]) / scale;
                for (int i = 0; i < elements[pass].Count; ++i) {
                    elements[pass][i].position = new Rect(elements[pass][i].position.x - distance[0].x, elements[pass][i].position.y - distance[0].y, elements[pass][i].position.width, elements[pass][i].position.height);
                }
                contextPosition /= scale / distance[2].x;
            }
        }

        public void OnBeforeSerialize() {
            SerializeJoints();
            serialization = DataAccess.Save(elements, settings, properties, renderQueue, isFunctionEditor);
        }

        internal void AddElement(string element) {
            elements[pass].Add(Utils.Create(element));
            VisualElement last = elements[pass].Last();
            last.position.x = mouseCoords.x;
            last.position.y = mouseCoords.y;
            last.selected = false;
            last.renderQueue = renderQueue;
            last.properties = properties;
            last.allElements = elements;
            SerializeJoints();
            RecordState();
        }

        internal void LoadData(string database) {
            renderQueue.passes.Clear();
            renderQueue = DataAccess.RenderRead(database);
            renderQueue.passes.ForEach(x => x.SetRenderQueue(renderQueue));
            settings = DataAccess.SettingsRead(database);
            if (isFunctionEditor) {
                properties = Utils.ConvertCustomToProperties(DataAccess.CustomRead(database));
                for (int i = 0; i < properties.Count; ++i) { 
                    properties[i].unique = i + 1;
                }
            } else {
                properties = DataAccess.PropertiesRead(database);
            }
            renderQueue.properties = properties;
            // Backward compatibility
            List<VisualProperty> intProperties = properties.Where(x => x.name == "Int" || x.name == "IntSlider").ToList();
            List<int> uniques = properties
                .Select(x => x.unique).ToList();
            foreach (VisualPass pass in renderQueue.passes) {
                pass.position.x = 8;
                if (pass.Variable != 0 && !uniques.Contains(pass.Variable)) {
                    pass.Variable = intProperties[pass.Variable - 1].unique;
                }
            }
            //
            elements = DataAccess.ElementsRead(database, isFunctionEditor);
            renderQueue.elements = elements;
            elements.ForEach(x => x.ForEach(y => y.renderQueue = renderQueue));
            elements.ForEach(x => x.ForEach(y => y.properties = properties));
            elements.ForEach(x => x.ForEach(y => y.allElements = elements));
            if (isFunctionEditor) {
                foreach (VisualElement element in elements[pass]) {
                    if (element.name.Contains("Input")) {
                        foreach (VisualProperty property in properties) {
                            if (element.Values[0] == property.Values[0]) {
                                element.Values[0] = property.unique.ToString();
                            }
                        }
                    }
                }
            }
            SerializeJoints();
            OrganizePasses();
        }

        public void OnAfterDeserialize() {
            if (serialization != null && !state.Contains("Drag")) {
                LoadData(serialization);
            }        
        }

        internal void Shortcut() {
            if (pressed.Count == 1 && SharedResources.shortcut.ContainsKey(pressed[0])) {
                AddElement(SharedResources.shortcut[pressed[0]]);
            } else if (pressed.Count == 2) {
                KeyCode tempKey = KeyCode.None;
                if (pressed[0] == KeyCode.LeftAlt) {
                    tempKey = pressed[1];
                } else if (pressed[1] == KeyCode.LeftAlt) {
                    tempKey = pressed[0];
                }
                if (SharedResources.shortcut.ContainsKey(tempKey)) {
                    AddElement(SharedResources.shortcutAlt[tempKey]);
                }
            }
        }

        internal void Refresh() {
            for (int i = 0; i < elements.Count; ++i) {
                for (int j = 0; j < elements[i].Count; ++j) {
                    if (elements[i][j].name.Contains("Custom")) {
                        elements[i][j].ReadCustomData();
                        elements[i][j].Arrange();
                    }
                }
            }
            CheckConnections();
            AssetDatabase.ImportAsset("Assets/Post Processing Ultimate/Shaders", ImportAssetOptions.ImportRecursive);
        }

        internal void DrawLowerRightCorner() {
            if (GUI.Button(new Rect(position.width - 242, position.height - 20, 32, 16), new GUIContent("<")) && recordedStatesPosition > 0) {
                --recordedStatesPosition;
                LoadData(recordedStates[recordedStatesPosition]);
            }
            EditorGUI.LabelField(new Rect(position.width - 206, position.height - 20, 64, 16), new GUIContent((recordedStatesPosition + 1) + " / " + recordedStates.Count), centeredStyle);
            if (GUI.Button(new Rect(position.width - 136, position.height - 20, 32, 16), new GUIContent(">")) && recordedStatesPosition < recordedStates.Count - 1) {
                ++recordedStatesPosition;
                LoadData(recordedStates[recordedStatesPosition]);
            }
            if (GUI.Button(new Rect(position.width - 100, position.height - 20, 96, 16), new GUIContent("SETTINGS"))) {
                SettingsWindow window = (SettingsWindow) GetWindow(typeof(SettingsWindow));
                window.position = new Rect(UnityEngine.Screen.currentResolution.width / 2 - (window.minSize.x / 2), UnityEngine.Screen.currentResolution.height / 2 - (window.minSize.y / 2), 0, 0);
                window.Show();
            }
        }

        internal void Deselect() {
            if (selecting && selectingBox.x == 0 && selectingBox.y == 0) {
                selecting = false;
                for (int i = 0; i < elements[pass].Count; ++i) {
                    if (elements[pass][i] != focusedElement) {
                        elements[pass][i].selected = false;
                    }
                }
            }
        }

        public void LoadSettings() {
            try {
                uSets = JsonUtility.FromJson<UserSettings>(System.IO.File.ReadAllText("Assets/Post Processing Ultimate/Editor/config.usets"));
            } catch {
                uSets = new UserSettings();
            }
        }

        internal void OnGUI() {
            state = Event.current.type.ToString();
            if (Event.current.type == EventType.MouseEnterWindow || Event.current.type == EventType.MouseLeaveWindow) {
                CheckConnections();
            }
            if (Event.current.type != EventType.Layout) {
                Deselect();
                if (serialization != null) {
                    serialization = null;
                }
                defaultGUI = GUI.matrix;
                GUI.DrawTextureWithTexCoords(new Rect(228, position.height, position.width - 228, -position.height), background, new Rect(0, 0, (position.width - 228) / 128.0f, position.height / 128.0f));
                GUI.EndGroup();
                SelectionHandle();
                BeforeScroll();
                GUI.BeginGroup(new Rect(0, offset * scale, position.width / scale, position.height / scale));
                GUIUtility.ScaleAroundPivot(Vector2.one * scale, new Vector2(0, offset));
                HandleNonMouseEvents();
                HandleMouseEvents();
                AfterScroll();
                DrawBeziers();
                DrawElements();
                DrawSelection();
                GUI.matrix = defaultGUI;
                GUI.EndGroup();
                GUI.BeginGroup(new Rect(0, offset, position.width, position.height));
            }
            DrawContext(contextPosition * scale);
            DrawLowerRightCorner();
            GUI.BeginGroup(new Rect(0, 0, position.width, position.height));
            defaultGUI = GUI.matrix;
            DrawToolbar();
            GUI.matrix = defaultGUI;
            GUI.EndGroup();
        }
    }
}