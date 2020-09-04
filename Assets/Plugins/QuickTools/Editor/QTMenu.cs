//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

using UnityEditor;

namespace QuickTools
{
    /// <summary>
    /// This script is used for adding the QuickTools menu to Unity Editor
    /// </summary>
    public static class QtMenu
    {
        public static QtuiPopup Popup;

        /// <summary>
        /// Adds the "Open Window" to Unity Editor
        /// </summary>     
        [MenuItem("Window/QuickTools/Open Window", false, 2300)]
        public static void CreateWindow()
        {
            EditorWindow.GetWindow<Qtui>(false, "QuickTools", true);
        }
    }
}