//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

using UnityEngine;
using UnityEditor;

namespace QuickTools
{
    public class Qtui : EditorWindow
    {
        private static bool _mLoaded;
        private static GameObject[] _go;

        private bool _showPosition = true;
        private bool _showRotation = true;
        private bool _showScale = true;

        private Rect _rect;
        private Vector2 _scrollPos;

        private static GUIStyle _foldOut;

        private static void Load()
        {
            _foldOut = new GUIStyle(EditorStyles.foldout)
                {fontStyle = FontStyle.Bold, contentOffset = new Vector2(3f, 0f)};
        }

        private void OnGUI()
        {
            minSize = new Vector2(110, 100);
            if (!_mLoaded)
            {
                Load();
                _mLoaded = true;
            }

            _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos, GUILayout.Width(position.width),
                GUILayout.Height(position.height));

            // Position
            GUILayout.Space(22);
            _rect = GUILayoutUtility.GetLastRect();
            _showPosition = EditorGUI.Foldout(new Rect(_rect.xMin + 3, _rect.yMin + 3, position.width - 6, 15),
                _showPosition, "Position", true, _foldOut);

            if (_showPosition)
            {
                // Set position functions
                QtFunctions.SetFunctions(false, false, 0);

                // Reset position functions
                QtFunctions.ResetFunctions(false, 0);
            }

            // Draw a seperator
            //QTEditorTools.DrawSeparator();
            QtUtility.DrawMiniSeparator();

            // Rotation
            GUILayout.Space(22);
            _rect = GUILayoutUtility.GetLastRect();
            _showRotation = EditorGUI.Foldout(new Rect(_rect.xMin + 3, _rect.yMin + 3, position.width - 6, 15),
                _showRotation, "Rotation", true, _foldOut);

            if (_showRotation)
            {
                // Set position functions
                QtFunctions.SetFunctions(false, false, 1);

                // Reset position functions
                QtFunctions.ResetFunctions(false, 1);
            }

            // Draw a seperator
            //QTEditorTools.DrawSeparator();
            QtUtility.DrawMiniSeparator();

            // Scale
            GUILayout.Space(22);
            _rect = GUILayoutUtility.GetLastRect();
            _showScale = EditorGUI.Foldout(new Rect(_rect.xMin + 3, _rect.yMin + 3, position.width - 6, 15), _showScale,
                "Scale", true, _foldOut);

            if (_showScale)
            {
                // Set position functions
                QtFunctions.SetFunctions(false, false, 2);

                // Reset position functions
                QtFunctions.ResetFunctions(false, 2);
            }

            // Draw a seperator
            //QTEditorTools.DrawSeparator();
            //QTUtility.DrawMiniSeparator();

            // Draw a seperator
            //QTEditorTools.DrawSeparator();
            
            EditorGUILayout.EndScrollView();
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }
    }
}