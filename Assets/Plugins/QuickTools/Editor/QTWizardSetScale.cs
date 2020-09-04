//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

using UnityEditor;
using UnityEngine;

namespace QuickTools
{
    public class QtWizardSetScale : ScriptableWizard
    {
        //public int method = 0;
        //public string[] methodStrings = new string[] { "Defined", "Random" };

        public int setScaleOrigin;
        public string[] setScaleStrings = new string[] {"World", "Parent"};
        public bool xAxis, yAxis, zAxis, loaded, oldGui;
        public Vector3 axis;
        public int translationType;
        public string[] translationTypes = new string[] {"Absolute", "Range"};
        public int isRelative;
        public string[] relativeStrings = new string[] {"Absolute", "Incremental"};
        
        // Used for random range
        private Vector3 _randomMin;
        private Vector3 _randomMax;

        private const float RAND_MIN = 0f;
        private const float RAND_MAX = 10f;

        private void Load()
        {
            xAxis = QtUtility.LoadBool("QT_Scale_xAxis");
            yAxis = QtUtility.LoadBool("QT_Scale_yAxis");
            zAxis = QtUtility.LoadBool("QT_Scale_zAxis");

            //method = QTEditorTools.LoadInt("QT_SetScaleMethod");
            setScaleOrigin = QtUtility.LoadInt("QT_SetScaleOrigin");

            axis = new Vector3(QtUtility.LoadFloat("QT_Scale_axis.x"), QtUtility.LoadFloat("QT_Scale_axis.y"),
                QtUtility.LoadFloat("QT_Scale_axis.z"));
            
            
            // Random  Scaleition MIN
            _randomMin = new Vector3(QtUtility.LoadFloat("QT_Scale_randomMin.x"), QtUtility.LoadFloat("QT_Scale_randomMin.y"), QtUtility.LoadFloat("QT_Scale_randomMin.z"));
            
            // Random  Scaleition MAX
            _randomMax =  new Vector3(QtUtility.LoadFloat("QT_Scale_randomMax.x"), QtUtility.LoadFloat("QT_Scale_randomMax.y"), QtUtility.LoadFloat("QT_Scale_randomMax.z"));
            
            // If these are both zero, lets change that.
            if (_randomMin == Vector3.zero && _randomMax == Vector3.zero)
            {
                _randomMin = new Vector3(0.5f, 0.5f, 0.5f);
                _randomMax = new Vector3(2f, 2f, 2f);
            }
            
            translationType = QtUtility.LoadInt("QT_SetScaleTranslationType");
            isRelative = QtUtility.LoadInt("QT_SetScaleIsRelative");

            loaded = true;
        }

        private void OnGUI()
        {
            oldGui = GUI.enabled;

            if (!loaded)
                Load();

            minSize = new Vector2(300, 335);


            GUILayout.Label("Axis of Scaleation");
            GUILayout.Space(5);

            EditorGUILayout.BeginHorizontal();
            {
                xAxis = GUILayout.Toggle(xAxis, "X axis");
                yAxis = GUILayout.Toggle(yAxis, "Y axis");
                zAxis = GUILayout.Toggle(zAxis, "Z axis");
            }
            EditorGUILayout.EndHorizontal();

            GUILayout.Space(10);
            GUILayout.Label("Value Mode");
            GUILayout.Space(5);
            translationType = GUILayout.Toolbar(translationType, translationTypes);

            GUILayout.Space(10);

            if (translationType == 0)
            {
                GUILayout.Label("Scale");
                axis = EditorGUILayout.Vector3Field("", axis);
                minSize = maxSize = new Vector2(300, 335);
                GUILayout.Space(15);
            }
            else
            {
                if (xAxis)
                {
                    GUILayout.Label("X Axis");
                    EditorGUILayout.BeginHorizontal();
                    {
                        _randomMin.x = EditorGUILayout.FloatField(_randomMin.x, GUILayout.Width(50f));
                        EditorGUILayout.MinMaxSlider(ref _randomMin.x, ref _randomMax.x, RAND_MIN, RAND_MAX);
                        _randomMax.x = EditorGUILayout.FloatField(_randomMax.x, GUILayout.Width(50f));
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(15);
                }

                if (yAxis)
                {
                    GUILayout.Label("Y Axis");
                
                    EditorGUILayout.BeginHorizontal();
                    {
                        _randomMin.y = EditorGUILayout.FloatField(_randomMin.y, GUILayout.Width(50f));
                        EditorGUILayout.MinMaxSlider(ref _randomMin.y, ref _randomMax.y, RAND_MIN, RAND_MAX);
                        _randomMax.y = EditorGUILayout.FloatField(_randomMax.y, GUILayout.Width(50f));
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(15);
                }

                if (zAxis)
                {
                    GUILayout.Label("Z Axis");
                
                    EditorGUILayout.BeginHorizontal();
                    {
                        _randomMin.z = EditorGUILayout.FloatField(_randomMin.z, GUILayout.Width(50f));
                        EditorGUILayout.MinMaxSlider(ref _randomMin.z, ref _randomMax.z, RAND_MIN, RAND_MAX);
                        _randomMax.z = EditorGUILayout.FloatField(_randomMax.z, GUILayout.Width(50f));
                    }
                    EditorGUILayout.EndHorizontal();
                    GUILayout.Space(15);
                }
                
                // Set dynamic window size
                var yMin = 282;
                yMin += (xAxis) ? 53 : 0;
                yMin += (yAxis) ? 53 : 0;
                yMin += (zAxis) ? 53 : 0;
                yMin = Mathf.Clamp(yMin, 335, (335 + (3* 53)));
                minSize = maxSize = new Vector2(300, yMin);
            }
            
            GUILayout.Label("Operation Mode");
            GUILayout.Space(5);
            isRelative = GUILayout.Toolbar(isRelative, relativeStrings);

            /*
        GUILayout.Space(10);
        GUILayout.Label("Transform orientation");
        orientation = GUILayout.Toolbar(orientation, orientationStrings);
        */

            GUILayout.Space(10);
            GUILayout.Label("Space");
            setScaleOrigin = GUILayout.Toolbar(setScaleOrigin, setScaleStrings);


            if (QtUtility.GetSelection.Length == 0)
                GUI.enabled = false;
            else
                GUI.enabled = true;

            QtUtility.DrawSeparator();
            GUILayout.Space(2);

            if (GUILayout.Button("Apply"))
            {
                Save(true);

                Close();
            }

            GUI.enabled = oldGui;

            if (QtUtility.GetSelection.Length == 0)
                EditorGUILayout.HelpBox("No Objects selected!", MessageType.Error);
            else if(!xAxis && !yAxis && !zAxis) 
                EditorGUILayout.HelpBox("No axis of transform enabled!", MessageType.Error);
            else
                EditorGUILayout.HelpBox("Apply to all selected objects", MessageType.Info);
        }

        private void OnInspectorUpdate()
        {
            Repaint();
        }

        private void Save(bool perform)
        {
            // Axis
            QtUtility.SaveBool("QT_Scale_xAxis", xAxis);
            QtUtility.SaveBool("QT_Scale_yAxis", yAxis);
            QtUtility.SaveBool("QT_Scale_zAxis", zAxis);
            
            // Scaleition
            QtUtility.SaveFloat("QT_Scale_axis.x", axis.x);
            QtUtility.SaveFloat("QT_Scale_axis.y", axis.y);
            QtUtility.SaveFloat("QT_Scale_axis.z", axis.z);
            
            // Random  Scaleition MIN
            QtUtility.SaveFloat("QT_Scale_randomMin.x", _randomMin.x);
            QtUtility.SaveFloat("QT_Scale_randomMin.y", _randomMin.y);
            QtUtility.SaveFloat("QT_Scale_randomMin.z", _randomMin.z);
            
            // Random  Scaleition MAX
            QtUtility.SaveFloat("QT_Scale_randomMax.x", _randomMax.x);
            QtUtility.SaveFloat("QT_Scale_randomMax.y", _randomMax.y);
            QtUtility.SaveFloat("QT_Scale_randomMax.z", _randomMax.z);
            
            // Origin
            QtUtility.SaveInt("QT_SetScaleOrigin", setScaleOrigin);

            QtUtility.SaveInt("QT_SetScaleTranslationType", translationType);
            QtUtility.SaveInt("QT_SetScaleIsRelative", isRelative);

            // We're performing the action,
            // Since this is all saved in QT_* strings, we can call "redo" since its basically the same thing.
            if (perform)
                QtFunctions.RepeatScale();
        }

        private void OnDestroy()
        {
            Save(false);
        }
    }
}