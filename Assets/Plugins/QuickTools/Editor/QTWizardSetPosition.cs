//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

using UnityEditor;
using UnityEngine;

namespace QuickTools
{
    public class QtWizardSetPosition : ScriptableWizard
    {
        //public int method = 0;
        //public string[] methodStrings = new string[] { "Defined", "Random" };

        public int setPosOrigin;
        public string[] setPosStrings = new string[] {"World", "Parent", "Local"};
        public bool xAxis, yAxis, zAxis, loaded, oldGui;
        public Vector3 axis;
        public int translationType;
        public string[] translationTypes = new string[] {"Absolute", "Range"};
        public int isRelative;
        public string[] relativeStrings = new string[] {"Absolute", "Incremental"};
        
        // Used for random range
        private Vector3 _randomMin;
        private Vector3 _randomMax;

        private const float RAND_MIN = -50f;
        private const float RAND_MAX = 50f;

        private void Load()
        {
            xAxis = QtUtility.LoadBool("QT_Pos_xAxis");
            yAxis = QtUtility.LoadBool("QT_Pos_yAxis");
            zAxis = QtUtility.LoadBool("QT_Pos_zAxis");

            //method = QTEditorTools.LoadInt("QT_SetPosMethod");
            setPosOrigin = QtUtility.LoadInt("QT_SetPosOrigin");

            axis = new Vector3(QtUtility.LoadFloat("QT_Pos_axis.x"), QtUtility.LoadFloat("QT_Pos_axis.y"),
                QtUtility.LoadFloat("QT_Pos_axis.z"));
            
            
            // Random  Position MIN
            _randomMin = new Vector3(QtUtility.LoadFloat("QT_Pos_randomMin.x"), QtUtility.LoadFloat("QT_Pos_randomMin.y"), QtUtility.LoadFloat("QT_Pos_randomMin.z"));
            
            // Random  Position MAX
            _randomMax =  new Vector3(QtUtility.LoadFloat("QT_Pos_randomMax.x"), QtUtility.LoadFloat("QT_Pos_randomMax.y"), QtUtility.LoadFloat("QT_Pos_randomMax.z"));

            // If these are both Vector3.zero, we might have just started. lets initialize them to something
            // If these are both zero, lets change that.
            if (_randomMin == Vector3.zero && _randomMax == Vector3.zero)
            {
                _randomMin = new Vector3(-10f, -10f, -10f);
                _randomMax = new Vector3(10f, 10f, 10f);
            }
            
            translationType = QtUtility.LoadInt("QT_SetPosTranslationType");
            isRelative = QtUtility.LoadInt("QT_SetPosIsRelative");

            loaded = true;
        }

        private void OnGUI()
        {
            oldGui = GUI.enabled;

            if (!loaded)
                Load();

            minSize = new Vector2(300, 335);


            GUILayout.Label("Axis of translation");
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
                GUILayout.Label("Position");
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
            setPosOrigin = GUILayout.Toolbar(setPosOrigin, setPosStrings);


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
            QtUtility.SaveBool("QT_Pos_xAxis", xAxis);
            QtUtility.SaveBool("QT_Pos_yAxis", yAxis);
            QtUtility.SaveBool("QT_Pos_zAxis", zAxis);
            
            // Position
            QtUtility.SaveFloat("QT_Pos_axis.x", axis.x);
            QtUtility.SaveFloat("QT_Pos_axis.y", axis.y);
            QtUtility.SaveFloat("QT_Pos_axis.z", axis.z);
            
            // Random  Position MIN
            QtUtility.SaveFloat("QT_Pos_randomMin.x", _randomMin.x);
            QtUtility.SaveFloat("QT_Pos_randomMin.y", _randomMin.y);
            QtUtility.SaveFloat("QT_Pos_randomMin.z", _randomMin.z);
            
            // Random  Position MAX
            QtUtility.SaveFloat("QT_Pos_randomMax.x", _randomMax.x);
            QtUtility.SaveFloat("QT_Pos_randomMax.y", _randomMax.y);
            QtUtility.SaveFloat("QT_Pos_randomMax.z", _randomMax.z);
            
            // Origin
            QtUtility.SaveInt("QT_SetPosOrigin", setPosOrigin);

            QtUtility.SaveInt("QT_SetPosTranslationType", translationType);
            QtUtility.SaveInt("QT_SetPosIsRelative", isRelative);

            // We're performing the action,
            // Since this is all saved in QT_* strings, we can call "redo" since its basically the same thing.
            if (perform)
                QtFunctions.RepeatPosition();
        }

        private void OnDestroy()
        {
            Save(false);
        }
    }
}