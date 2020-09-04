//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

using UnityEngine;
using UnityEditor;

namespace QuickTools
{
    
    public class QtUtility : MonoBehaviour
    {
        private static float _popupWidth;

        //static float popupHeight;

        private static GUISkin QTSkin = Resources.Load("QTSkin") as GUISkin;

        /// <summary>
        /// Returns a blank 1x1 2DTexture
        /// </summary>
        private static Texture2D BlankTexture => EditorGUIUtility.whiteTexture;

        public static GUISkin GetSkin()
        {
            return QTSkin;
        }

        /// <summary>
        /// Save the specified string into player prefs.
        /// </summary>
        public static void SaveString(string field, string val)
        {
            if (string.IsNullOrEmpty(val))
                EditorPrefs.DeleteKey(field);
            else
                EditorPrefs.SetString(field, val);
        }

        /// <summary>
        /// Load the specified string from player prefs.
        /// </summary>
        public static string LoadString(string field)
        {
            var s = EditorPrefs.GetString(field);
            return string.IsNullOrEmpty(s) ? "" : s;
        }

        /// <summary>
        /// Load the specified string from player prefs.
        /// </summary>
        public static int LoadInt(string field)
        {
            return EditorPrefs.GetInt(field);
        }

        /// <summary>
        /// Save the specified string into player prefs.
        /// </summary>
        public static void SaveInt(string field, int val)
        {
            EditorPrefs.SetInt(field, val);
        }

        /// <summary>
        /// Load the specified string from player prefs.
        /// </summary>
        public static float LoadFloat(string field)
        {
            return EditorPrefs.GetFloat(field);
        }

        /// <summary>
        /// Save the specified string into player prefs.
        /// </summary>
        public static void SaveFloat(string field, float val)
        {
            EditorPrefs.SetFloat(field, val);
        }

        /// <summary>
        /// Load the specified string from player prefs.
        /// </summary>
        public static bool LoadBool(string field)
        {
            return EditorPrefs.GetBool(field);
        }

        /// <summary>
        /// Load the specified string from player prefs.
        /// </summary>
        public static void SaveBool(string field, bool val)
        {
            EditorPrefs.SetBool(field, val);
        }

        /// <summary>
        /// 	Draws a visible horizontal seperator
        /// </summary>
        /// 
        public static void DrawSeparator()
        {
            GUILayout.Space(12f);

            if (Event.current.type != EventType.Repaint) return;
            var tex = BlankTexture;
            var rect = GUILayoutUtility.GetLastRect();
            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 8f, Screen.width, 4f), tex); // Draws base
            GUI.DrawTexture(new Rect(0f, rect.yMin + 8f, Screen.width, 1f), tex); // Draws top border
            GUI.DrawTexture(new Rect(0f, rect.yMin + 11f, Screen.width, 1f), tex); // Draws bot border
            GUI.color = Color.white;
        }

        ///	<summary>
        /// 	Draws a mini horizontal seperator
        /// </summary>
        public static void DrawMiniSeparator()
        {
            GUILayout.Space(8f);

            if (Event.current.type != EventType.Repaint) return;
            var tex = BlankTexture;
            var rect = GUILayoutUtility.GetLastRect();

            GUI.color = EditorGUIUtility.isProSkin
                ? new Color(0f, 0f, 0f, 0.4f)
                : new Color(255f, 255f, 255f, 0.15f);
            GUI.DrawTexture(new Rect(0f, rect.yMin + 3f, Screen.width, 1f), tex);
            GUI.color = Color.white;
        }

        /// <summary>
        /// 	Draws a visible horizontal seperator (For the popup)
        /// </summary>
        /// 
        public static void DrawPopupSeparator()
        {
            GUILayout.Space(12f);

            if (Event.current.type != EventType.Repaint) return;
            var tex = BlankTexture;
            var rect = GUILayoutUtility.GetLastRect();


            GUI.color = EditorGUIUtility.isProSkin
                ? new Color(255f, 255f, 255f, 0.05f)
                : new Color(255f, 255f, 255f, 0.25f);
        
            
            GUI.DrawTexture(new Rect(3f, rect.yMin + 9f, 1f, 2f), tex); // Draws left Border
            GUI.DrawTexture(new Rect(_popupWidth - 4f, rect.yMin + 9f, 1f, 2f), tex); // Draws right Border

            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            GUI.DrawTexture(new Rect(4f, rect.yMin + 8f, _popupWidth - 8f, 4f), tex); // Draws base
            GUI.DrawTexture(new Rect(4f, rect.yMin + 8f, _popupWidth - 8f, 1f), tex); // Draws top border
            GUI.DrawTexture(new Rect(4f, rect.yMin + 11f, _popupWidth - 8f, 1f), tex); // Draws bot border
            GUI.color = Color.white;
        }

        /// <summary>
        /// 	Draws a visible horizontal seperator
        /// </summary>
        /// 
        public static void DrawPopupSeparator(float val)
        {
            GUILayout.Space(val);

            if (Event.current.type != EventType.Repaint) return;
            var tex = BlankTexture;
            var rect = GUILayoutUtility.GetLastRect();
            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            GUI.DrawTexture(new Rect(4f, rect.yMin + 8f, _popupWidth - 8f, 4f), tex); // Draws base
            GUI.DrawTexture(new Rect(4f, rect.yMin + 8f, _popupWidth - 8f, 1f), tex); // Draws top border
            GUI.DrawTexture(new Rect(4f, rect.yMin + 11f, _popupWidth - 8f, 1f), tex); // Draws bot border
            GUI.color = Color.white;
        }

        public static void BeginArea(float height)
        {
            GUILayout.Space(10f);

            if (Event.current.type != EventType.Repaint) return;
            var rect = GUILayoutUtility.GetLastRect();
            GUILayout.BeginArea(new Rect(rect.xMin + 10f, rect.yMin, _popupWidth - 20, height));
        }

        /// <summary>
        /// 	Draws a visible border.
        /// </summary>
        public static void DrawBorder()
        {
            if (Event.current.type != EventType.Repaint) return;
            var tex = BlankTexture;
            var rect = GUILayoutUtility.GetLastRect();
            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            GUI.DrawTexture(new Rect(rect.xMin, rect.yMax, 1f, -rect.height), tex);
            GUI.DrawTexture(new Rect(rect.xMax, rect.yMax, 1f, -rect.height), tex);
            GUI.DrawTexture(new Rect(rect.xMin, rect.yMin, rect.width, 1f), tex);
            GUI.DrawTexture(new Rect(rect.xMin, rect.yMax, rect.width, 1f), tex);
            GUI.color = Color.white;
        }

        /// <summary>
        /// Draw popup borders!
        /// </summary>
        public static void DrawPopupBorder(float width, float height)
        {
            if (Event.current.type != EventType.Repaint) return;
            var tex = BlankTexture;
            //Rect rect = GUILayoutUtility.GetLastRect();
            GUI.color = new Color(0f, 0f, 0f, 0.25f);
            const float headerHeight = 24f;

            // Draw left side
            GUI.DrawTexture(new Rect(0f, 0f, 1f, height), tex); // Draws left Side);
            GUI.DrawTexture(new Rect(0f, 0f, 4f, height), tex); // Draws base
            GUI.DrawTexture(new Rect(3f, headerHeight - 3 , 1f, height - headerHeight), tex); // Draws Right Side

            // Draw Top Side
            GUI.DrawTexture(new Rect(1f, 0f, width - 2f, 1f), tex); // Draws Top Side
            GUI.DrawTexture(new Rect(4f, 0f, width - 8f, headerHeight - 2), tex); // Draws base Side
            GUI.DrawTexture(new Rect(4f, headerHeight - 3, width - 8f, 1f), tex); // Draws left Side

            // Draw Right side
            GUI.DrawTexture(new Rect(width - 4f, headerHeight - 3 , 1f, height - headerHeight), tex); // Draws left Side
            GUI.DrawTexture(new Rect(width - 4f, 0f, 4f, height), tex); // Draws base;
            GUI.DrawTexture(new Rect(width - 1f, 0f, 1f, height), tex); // Draws Right Side

            // Draw bottom side
            GUI.DrawTexture(new Rect(4f, height - 4f, width - 8f, 1f), tex); // Draws Top Side
            GUI.DrawTexture(new Rect(4f, height - 4f, width - 8f, 4f), tex); // Draws base Side
            GUI.DrawTexture(new Rect(1f, height - 1f, width - 2f, 1f), tex); // Draws left Side

            GUI.color = Color.white;

            _popupWidth = width;
            //popupHeight = height;
        }

        /// <summary>
        /// 	Checks if there is a selection
        /// </summary>
        public static Object[] GetSelection => Selection.GetFiltered(typeof(GameObject), SelectionMode.Editable);
    }
}