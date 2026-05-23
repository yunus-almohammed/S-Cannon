using System.Collections.Generic;
using UnityEngine;

namespace DawidMoza.PostProcessingUltimate {

    public class VisualJoint : object {
        public int type;
        public int[] serial = { NOT_CONNECTED, NOT_CONNECTED };
        public int[] connectedSerial = { NOT_CONNECTED, NOT_CONNECTED };
        public List<int> prohibitions = new List<int>();
        public int size = 1;
        public string name;
        public string tooltip;
        public string component;
        internal Vector2 coords;
        internal List<List<Texture2D>> icons = new List<List<Texture2D>>();

        public const int RIGHT = 1;
        public const int LEFT = 0;
        public const int NOT_CONNECTED = -1;
        public const int TEMP_JOINT = -2;
        public const int RIGHT_CONNECTED = -3;

        public VisualJoint(int type, int size, string name = "", string tooltip = "", string component = "") {
            this.type = type;
            this.size = size;
            this.name = name;
            this.tooltip = tooltip;
            this.component = component;
        }

        public bool Connected() {
            return connectedSerial[0] != NOT_CONNECTED && connectedSerial[1] != NOT_CONNECTED;
        }

        public bool ConnectedToTempJoint() {
            return connectedSerial[0] == TEMP_JOINT && connectedSerial[1] == TEMP_JOINT;
        }

        public void Show() {
            if (icons.Count == 0 || icons[type][size] == null || icons[type][size + 5] == null) {
                icons = SharedResources.VisualJointIcons();
            }
            GUI.DrawTexture(new Rect(coords.x, coords.y, Utils.jointSize, Utils.jointSize), !Connected() ? icons[type][size] : icons[type][size + 5]);
        }
    }
}