//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

using JetBrains.Annotations;
using UnityEngine;
using UnityEditor;

namespace QuickTools
{
    public class QtuiPopup : EditorWindow
    {
        private static bool _mLoaded;
        private static GameObject[] _go;

        private static QtuiPopup _instance;
        private static bool _positionSet;

        public const int PopupWidth = 120;
        private const int PopupHeight = 529;

        // Styles
        private static GUIStyle _qtTitleBox;
        private static GUIStyle _qtLabel;
        private static GUIStyle _qtLabelBold;
        private static GUIStyle _qtLargeLabel;
        private static GUIStyle _qtToggle;
        
        [UsedImplicitly] private Rect _rectangle;

        /// <summary>
        /// Adds the "Open Popup" to Unity Editor.
        /// </summary>
        [MenuItem("Window/QuickTools/Open Popup _%q", false,  2500)]
        public static void Init()
        {
            if (_instance) return;
            _instance = CreateInstance<QtuiPopup>();
            
            // Initialize, set size to 1x1 to avoid a flicker
            _instance.position = new Rect(0, 0, 1, 1);
            
            _instance.ShowPopup();
        }

        private static void Load()
        {
            //Box skin
            _qtTitleBox = new GUIStyle(GUI.skin.box) {margin = new RectOffset(0, 0, 0, 0), stretchWidth = true};


            if (EditorGUIUtility.isProSkin) _qtTitleBox.normal.textColor = Color.white;

            _qtTitleBox.normal.background = null;
            _qtTitleBox.border = new RectOffset(0, 0, 0, 0);
            _qtTitleBox.padding = new RectOffset(0, 0, 4, 0);
            _qtTitleBox.fontStyle = FontStyle.Bold;

            // Button skin
            //var guiStyle = new GUIStyle(GUI.skin.button) {margin = new RectOffset(7, 7, 4, 4)};
        }

        private void OnDestroy()
        {
            _instance = null;
            _positionSet = false;
            _mLoaded = false;
        }


        private void OnGUI()
        {
            if (!_mLoaded)
            {
                Load();
                _mLoaded = true;
            }

            if (!_positionSet && _instance)
            {
                _rectangle = _instance.position = new Rect(Event.current.mousePosition.x - 5,
                    Event.current.mousePosition.y - 5,
                    PopupWidth, PopupHeight);

                _positionSet = true;
            }

            QtUtility.DrawPopupBorder(PopupWidth, PopupHeight);

            GUILayout.Label("QuickTools", _qtTitleBox);

            DrawButtons();
        }

        private void OnInspectorUpdate()
        {
            if (mouseOverWindow != this) Close();

            Repaint();
        }


        private static void DrawButtons()
        {
            // Position

            // Set position functions
            QtFunctions.SetFunctions(true, true, 0);

            // Reset position functions
            QtFunctions.ResetFunctions(true, 0);

            // Draw a separator
            QtUtility.DrawPopupSeparator();

            // Rotation

            // Set position functions
            QtFunctions.SetFunctions(true, true, 1);

            // Reset position functions
            QtFunctions.ResetFunctions(true, 1);

            // Draw a seperator
            QtUtility.DrawPopupSeparator();

            // Scale

            // Set position functions
            QtFunctions.SetFunctions(true, true, 2);

            // Reset position functions
            QtFunctions.ResetFunctions(true, 2);
            
        }
    }
}