//-------------------------------------------
//            	QuickTools
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------
//
//    This file is heavily over commented.
//    So that I can follow the logic over time.
//    A lot of breaks have been taken
//
//    It's a real mess, could be optimized.
//    Reset for instance, could be merged with
//    the set functions.. no need to double up.

using UnityEditor;
using UnityEngine;

namespace QuickTools
{
    public class QtFunctions : MonoBehaviour
    {
        private static readonly GUIStyle QtButton;
        private static readonly GUIStyle QtLabelBold;
        private static readonly GUIStyle QtLargeLabel;

        private static readonly GUIStyle DefButton;
        private static readonly GUIStyle DefLabelBold;
        private static readonly GUIStyle DefLargeLabel;


        private static int _posOrigin;
        private static int _rotOrigin;
        private static int _scaleOrigin; 
        private static readonly string[] OriginStrings = {"World", "Parent"};

        static QtFunctions()
        {
            // Assign defaults
            DefButton = new GUIStyle(GUI.skin.button);
            var defLargeLabel = new GUIStyle(GUI.skin.label);
            DefLargeLabel = defLargeLabel;
            DefLabelBold = defLargeLabel;

            DefLargeLabel.margin = new RectOffset(4, 4, 4, 6);
            DefLargeLabel.fontSize = 12;

            DefLabelBold.fontStyle = FontStyle.Normal;

            // Button skin
            QtButton = new GUIStyle(GUI.skin.button) {margin = new RectOffset(7, 7, 4, 4)};


            // Label Skin
            QtLargeLabel = new GUIStyle(GUI.skin.label)
            {
                margin = new RectOffset(7, 7, 4, 6), fontSize = 12, fontStyle = FontStyle.Bold
            };

            // Label Bold Skin
            QtLabelBold = new GUIStyle(GUI.skin.label)
                {margin = new RectOffset(7, 7, 4, 4), fontStyle = FontStyle.Normal};
            
            _rotOrigin = 0;
            _rotOrigin = 0;
            _scaleOrigin = 0;
        }
        
        /// <summary>
        ///     Draws the Set position buttons
        ///     Functions included.
        /// </summary>
        public static void SetFunctions(bool val, bool val2, int type)
        {
            GUIStyle btnStyle;
            GUIStyle lblLarge;
            if (!val)
            {
                btnStyle = DefButton;
                lblLarge = DefLargeLabel;
            }
            else
            {
                btnStyle = QtButton;
                lblLarge = QtLargeLabel;
            }

            switch (type)
            {
                case 0 when val2:
                    GUILayout.Space(3);
                    GUILayout.Label("Position", lblLarge);
                    break;
                case 0:
                    GUILayout.Space(6);
                    break;
                case 1 when val2:
                    GUILayout.Label("Rotation", lblLarge);
                    break;
                case 1:
                    GUILayout.Space(6);
                    break;
                case 2 when val2:
                    GUILayout.Label("Scale", lblLarge);
                    break;
                case 2:
                    GUILayout.Space(6);
                    break;
            }

            var oldGui = GUI.enabled;
            var selection = QtUtility.GetSelection;

            GUI.enabled = selection.Length != 0;

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Set", btnStyle))
                {
                    switch (type)
                    {
                        case 0:
                            EditorWindow.GetWindow<QtWizardSetPosition>(true, "QuickTools: Set Position");
                            break;
                        case 1:
                            EditorWindow.GetWindow<QtWizardSetRotation>(true, "QuickTools: Set Rotation");
                            break;
                        case 2:
                            EditorWindow.GetWindow<QtWizardSetScale>(true, "QuickTools: Set Scale");
                            break;
                    }
                }
                    
                if (GUILayout.Button("Repeat", btnStyle))
                    switch (type)
                    {
                        case 0:
                            RepeatPosition();
                            break;
                        case 1:
                            RepeatRotation();
                            break;
                        case 2:
                            RepeatScale();
                            break;
                    }
            }
            EditorGUILayout.EndHorizontal();

            GUI.enabled = oldGui;
        }

        /// <summary>
        /// Draw the reset Functions
        /// </summary>
        /// <param name="val"></param>
        /// <param name="type"></param>
        public static void ResetFunctions(bool val, int type)
        {
            GUIStyle btnStyle;
            GUIStyle lblBold;
            if (!val)
            {
                btnStyle = DefButton;
                lblBold = DefLabelBold;
            }
            else
            {
                btnStyle = QtButton;
                lblBold = QtLabelBold;
            }

            GUILayout.Label("Reset", lblBold);

            var oldGui = GUI.enabled;

            var selection = QtUtility.GetSelection;

            GUI.enabled = selection.Length != 0;

            if (GUILayout.Button("All axis", btnStyle)) DoReset(type, 0);

            GUILayout.BeginHorizontal();
            {
                if (val)
                    GUILayout.Space(7);

                if (GUILayout.Button("X", DefButton)) DoReset(type, 1);
                if (GUILayout.Button("Y", DefButton)) DoReset(type, 2);
                if (GUILayout.Button("Z", DefButton)) DoReset(type, 3);

                if (val)
                    GUILayout.Space(3);
            }
            GUILayout.EndHorizontal();

            switch (type)
            {
                case 0:
                {
                    GUILayout.Label("Space", lblBold);
                    if (val)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(8);
                        _posOrigin = GUILayout.Toolbar(_posOrigin, OriginStrings,
                            GUILayout.MaxWidth(QtuiPopup.PopupWidth - 16));
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        _posOrigin = GUILayout.Toolbar(_posOrigin, OriginStrings);
                    }

                    break;
                }
                case 1:
                {
                    GUILayout.Label("Space", lblBold);
                    if (val)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(8);
                        _rotOrigin = GUILayout.Toolbar(_rotOrigin, OriginStrings,
                            GUILayout.MaxWidth(QtuiPopup.PopupWidth - 16));
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        _rotOrigin = GUILayout.Toolbar(_rotOrigin, OriginStrings);
                    }

                    break;
                }
                case 2:
                {
                    GUILayout.Label("Space", lblBold);
                    if (val)
                    {
                        EditorGUILayout.BeginHorizontal();
                        GUILayout.Space(8);
                        _scaleOrigin = GUILayout.Toolbar(_scaleOrigin, OriginStrings,
                            GUILayout.MaxWidth(QtuiPopup.PopupWidth - 16));
                        EditorGUILayout.EndHorizontal();
                    }
                    else
                    {
                        _scaleOrigin = GUILayout.Toolbar(_scaleOrigin, OriginStrings);
                    }

                    break;
                }
            }

            GUI.enabled = oldGui;

            if (!val)
                GUILayout.Space(5f);
        }


        /// <summary>
        ///     Performs the rests.
        /// </summary>
        /// <param name="type">0 Position, 1 Rotation, 2 Scale</param>
        /// <param name="axis">Axis, 0 = All, 1 = x, 2 = y, 3 = z</param>
        private static void DoReset(int type, int axis)
        {
            // Type: 0 = Position. 1 = rotation, 2 = Scale
            // Axis: 0 = all, 1 = x, 2 = y, 3 = z
            // Reset Position

            if (QtUtility.GetSelection.Length == 0)
                return;

            switch (type)
            {
                // We're doing position.
                case 0:
                {
                    var origin = _posOrigin;

                    foreach (var o in QtUtility.GetSelection)
                    {
                        var sel = (GameObject) o;
                        var position = sel.gameObject.transform.position;
                        
                        var x = axis == 1 || axis == 0 ? 0f : position.x;
                        var y = axis == 2 || axis == 0 ? 0f : position.y;
                        var z = axis == 3 || axis == 0 ? 0f : position.z;
                        
                        // Is the parent our pivot? and do we actually have a parent?
                        if (origin == 1 && sel.gameObject.transform.parent != null)
                        {
                            var parentPos = sel.gameObject.transform.parent.transform.position;
                            x = axis == 1 || axis == 0 ? parentPos.x : position.x;
                            y = axis == 2 || axis == 0 ? parentPos.y : position.y;
                            z = axis == 3 || axis == 0 ? parentPos.z : position.z;
                        }
                        
                        // Set our new position
                        position = new Vector3(x, y, z);
                        Undo.RegisterCompleteObjectUndo(sel.gameObject.transform, "Reset Position");
                        sel.gameObject.transform.position = position;
                    }

                    break;
                }
                
                // We're doing rotation
                case 1:
                {
                    var origin = _rotOrigin;
                    
                    foreach (var o in QtUtility.GetSelection)
                    {
                        var sel = (GameObject) o;
                        var rotation = sel.gameObject.transform.rotation;
                        
                        // If we're using the parents transform, we're using localRotation, replace the rotation var.
                        if (origin == 1 && sel.gameObject.transform.parent != null)
                            rotation = sel.gameObject.transform.localRotation;
                        
                        var x = axis == 1 || axis == 0 ? 0f : rotation.x;
                        var y = axis == 2 || axis == 0 ? 0f : rotation.y;
                        var z = axis == 3 || axis == 0 ? 0f : rotation.z;
                        
                        //  Set our new rotation
                        rotation = Quaternion.Euler(new Vector3(x, y, z));
                        Undo.RegisterCompleteObjectUndo(sel.gameObject.transform, "Reset Rotation");
                        
                        // If we're doing parents origin, use localRotation.
                        if (origin == 1 && sel.gameObject.transform.parent != null)
                            sel.gameObject.transform.localRotation = rotation;
                        else
                            sel.gameObject.transform.rotation = rotation;
                    }

                    break;
                }
                
                // We're doing scale.
                case 2:
                {
                    var origin = _scaleOrigin;

                    foreach (var o in QtUtility.GetSelection)
                    {
                        var sel = (GameObject) o;

                        var localScale = sel.gameObject.transform.localScale;
                        var x = axis == 1 || axis == 0 ? 1f : localScale.x;
                        var y = axis == 2 || axis == 0 ? 1f : localScale.y;
                        var z = axis == 3 || axis == 0 ? 1f : localScale.z;

                        localScale = new Vector3(x, y, z);
                        Undo.RegisterCompleteObjectUndo(sel.gameObject.transform, "Reset Scale");
                        
                        // If we're resetting based on the parents scale, we need to take that into account.
                        // This is a little counterintuitive. Since we are using localScale, we need to
                        // reverse what you're expecting, so if we're based on the world; we do this.
                        if (origin == 0 && sel.gameObject.transform.parent != null)
                        {
                            var parent = sel.gameObject.transform.parent.transform.localScale;
                            var sX = (axis == 1 || axis == 0) ? 1 / parent.x : 1f; 
                            var sY = (axis == 2 || axis == 0) ? 1 / parent.y : 1f; 
                            var sZ = (axis == 3 || axis == 0) ? 1 / parent.z : 1f;
                            var parentScale = new Vector3(sX, sY, sZ);
                            localScale = Vector3.Scale(localScale, parentScale);
                        
                        }
                        
                        // Finally set the scale
                        sel.gameObject.transform.localScale = localScale;
                    }

                    break;
                }
            }
        }

        /// <summary>
        /// Sets the object's transform position,
        /// and saves an undo state for it.
        /// </summary>
        /// <param name="o">The Object</param>
        /// <param name="position">THe Position</param>
        /// <param name="pivot">The Transform pivot. 0 = World, 1 = parent, 2 = Local</param>
        private static void SetPosition(GameObject o, Vector3 position, int pivot)
        {
            // 2 is the same as 1, so lets just change that.
            pivot = (pivot == 2) ? 1 : pivot;
            
            switch (pivot)
            {
                case 0:
                    o.gameObject.transform.position = position;
                    break;
                case 1:
                    o.gameObject.transform.localPosition = position;
                    break;
            }
        }

        /// <summary>
        /// Perform the position set,
        /// based on parameters saved in the QTWizardSetPosition.
        /// </summary>
        public static void RepeatPosition()
        {
            // Booleans of axis we're manipulating
            var bAxisX = QtUtility.LoadBool("QT_Pos_xAxis");
            var bAxisY = QtUtility.LoadBool("QT_Pos_yAxis");
            var bAxisZ = QtUtility.LoadBool("QT_Pos_zAxis");
            
            // values of axis
            var axisX = QtUtility.LoadFloat("QT_Pos_axis.x");
            var axisY = QtUtility.LoadFloat("QT_Pos_axis.y");
            var axisZ = QtUtility.LoadFloat("QT_Pos_axis.z");

            // The Action origin. 0 == World | 1 == Parent | 2 == Local
            var setPosOrigin = QtUtility.LoadInt("QT_SetPosOrigin");
            
            // Are we doing incremental? 0 == false | 1 == true
            var isRelative = QtUtility.LoadInt("QT_SetPosIsRelative");
            
            // Random  Position MIN
            var randomMin = new Vector3(QtUtility.LoadFloat("QT_Pos_randomMin.x"), QtUtility.LoadFloat("QT_Pos_randomMin.y"), QtUtility.LoadFloat("QT_Pos_randomMin.z"));
            
            // Random  Position MAX
            var randomMax =  new Vector3(QtUtility.LoadFloat("QT_Pos_randomMax.x"), QtUtility.LoadFloat("QT_Pos_randomMax.y"), QtUtility.LoadFloat("QT_Pos_randomMax.z"));
            
            // our translation type
            var translationType = QtUtility.LoadInt("QT_SetPosTranslationType");

            foreach (var o in QtUtility.GetSelection)
            {    
                // Are we using random position?
                if (translationType == 1)
                {
                    // If that's the case, its time to randomize the values!
                    axisX = Random.Range(randomMin.x, randomMax.x);
                    axisY = Random.Range(randomMin.y, randomMax.y);
                    axisZ = Random.Range(randomMin.z, randomMax.z);
                }
                
                // Register an undo state!
                Undo.RegisterFullObjectHierarchyUndo(o, "Set Position");
                
                var sel = (GameObject) o;

                var parentScale = Vector3.one;

                GameObject localParent = null;
                GameObject localOldParent = null;
                
                // Set up parent parameters
                // based on the position origin.
                switch (setPosOrigin)
                {
                    case 1:
                    {
                        if (sel.transform.parent != null)
                        {
                            var parent = sel.gameObject.transform.parent;
                            parentScale = parent.localScale;
                        }

                        break;
                    }
                    case 2:
                    {
                        // We're creating a parent to be the object of our selection.
                        // We're doing this to able to do the transform based on local space.
                        // This might be a really dumb solution to the issue.. but hey, it works.
                        localParent = new GameObject("QuickToolsTemp");
                        localParent.transform.position = sel.transform.position;
                        localParent.transform.rotation = sel.transform.rotation;
                        localParent.transform.localScale = sel.transform.localScale;

                        // If we already have a parent, the new object needs to be a child of that as well!
                        if (sel.transform.parent != null)
                        {
                            var parent1 = sel.transform.parent;
                            localOldParent = parent1.gameObject;
                            localParent.transform.parent = parent1;
                        }
                        
                        // Finally, set our object to be the child of it.
                        sel.transform.parent = localParent.transform;
                        break;
                    }
                }

                // Do we have a parent? If not, use vector3.zero as a "parent"
                var position = sel.transform.position;

                // We're using local transforms instead!
                // We we are doing local orientation
                if (setPosOrigin == 1 || setPosOrigin == 2)
                    position = sel.gameObject.transform.localPosition;

                // This is basic?
                var x = position.x;
                var y = position.y;
                var z = position.z;

                // Are we in relative mode?
                // If that's the case, we add the axis to the current axis.
                if (isRelative == 1)
                {
                    x = bAxisX ? axisX + position.x : x;
                    y = bAxisY ? axisY + position.y : y;
                    z = bAxisZ ? axisZ + position.z : z;
                }
                else
                {
                    x = bAxisX ? axisX : position.x;
                    y = bAxisY ? axisY : position.y;
                    z = bAxisZ ? axisZ : position.z;
                }
                
                // If we're in World position
                // And we want to position the object in world space
                // We have to take into account the parents scale to get it correct.
                if (setPosOrigin == 0)
                {
                    x = bAxisX ? parentScale.x * x : x;
                    y = bAxisY ? parentScale.y * y : y;
                    z = bAxisZ ? parentScale.z * z : z;
                }
                
                // Finally add up and set!
                var setPosition = new Vector3(x, y, z);
                SetPosition(sel, setPosition, setPosOrigin);
                
                // If we're doing it based on local
                // We have to make sure to reset the temp parent we set up!
                if (setPosOrigin != 2) continue;
                
                // Set parent back to the previous parent, or become batman.
                sel.transform.parent = localOldParent != null ? localOldParent.transform : null;
                DestroyImmediate(localParent);
            }
        }

        /// <summary>
        /// Sets the object's transform rotation,
        /// </summary>
        /// <param name="o">The Object</param>
        /// <param name="rotation">THe Position</param>
        /// <param name="pivot">The Transform pivot. 0 = World, 1 = parent, 2 = Local</param>
        /// <param name="incremental"></param>
        private static void SetRotation(GameObject o, Vector3 rotation, int pivot, bool incremental)
        {
            switch (pivot)
            {
                // We're moving with the world!
                case 0:
                    if (incremental)
                        o.transform.Rotate(rotation, Space.World);
                    else
                        o.gameObject.transform.rotation = Quaternion.Euler(rotation);
                    break;
                
                // We're doing it based off of the parent
                case 1:
                    // First, do we have a parent? If not, this is basically a space.self, so lets just relay that if its the case.
                    if (o.transform.parent != null)
                    {
                        // Basically, we're going to make a new GameObject, that is the same orientation as the parent
                        // and rotate that object, while this object is inside of it, then release it back to its parent.
                        var parentTrans = o.gameObject.transform.parent;
                        var tempObj = new GameObject("QuickTools Temp Object");
                        tempObj.transform.position = o.gameObject.transform.position;
                        
                        // For some reason our scale and position is changed during all this,
                        // So lets save it and restore it afterwards.
                        var originalScale = o.gameObject.transform.localScale;
                        var originalPosition = o.gameObject.transform.localPosition;
                        
                        // This is now identical to the parent.
                        tempObj.transform.SetParent(parentTrans);
                        tempObj.transform.localRotation = Quaternion.identity;
                        
                        // if We're doing incremental, we add our child to it.
                        if (incremental)
                        {    
                            o.gameObject.transform.SetParent(tempObj.transform);
                        }
                        
                        // and we call a SetRotation on that one.
                        SetRotation(tempObj, rotation, 2, incremental);
                        
                        // Now its time to return this GameObject to its proper state.
                        // If we changed it to begin with..
                        if (incremental)
                            o.transform.SetParent(parentTrans);
                        else
                            o.transform.localRotation = tempObj.transform.localRotation;
                        
                        // Destroy our temp!
                        DestroyImmediate(tempObj);
                        o.transform.localScale = originalScale;
                        o.transform.localPosition = originalPosition;
                        
                        break;
                    }
                    
                    SetRotation(o, rotation, 2, incremental);
                    break;

                // we're using ourselves!
                case 2:
                    if (incremental)
                        o.transform.Rotate(rotation, Space.Self);
                    else
                        o.transform.localRotation = Quaternion.Euler(rotation);
                    break;
                
            }
            
        }
        
        public static void RepeatRotation()
        {
            // Booleans of axis we're manipulating
            var bAxisX = QtUtility.LoadBool("QT_Rot_xAxis");
            var bAxisY = QtUtility.LoadBool("QT_Rot_yAxis");
            var bAxisZ = QtUtility.LoadBool("QT_Rot_zAxis");
            
            // values of axis
            var axisX = QtUtility.LoadFloat("QT_Rot_axis.x");
            var axisY = QtUtility.LoadFloat("QT_Rot_axis.y");
            var axisZ = QtUtility.LoadFloat("QT_Rot_axis.z");

            // The Action origin. 0 == World | 1 == Parent | 2 == Local
            var setRotOrigin = QtUtility.LoadInt("QT_SetRotOrigin");
            
            // Are we doing incremental? 0 == false | 1 == true
            var isRelative = QtUtility.LoadInt("QT_SetRotIsRelative") != 0;
            
            // Random  Rotition MIN
            var randomMin = new Vector3(QtUtility.LoadFloat("QT_Rot_randomMin.x"), QtUtility.LoadFloat("QT_Rot_randomMin.y"), QtUtility.LoadFloat("QT_Rot_randomMin.z"));
            
            // Random  Rotition MAX
            var randomMax =  new Vector3(QtUtility.LoadFloat("QT_Rot_randomMax.x"), QtUtility.LoadFloat("QT_Rot_randomMax.y"), QtUtility.LoadFloat("QT_Rot_randomMax.z"));
            
            // our translation type
            var rotationType = QtUtility.LoadInt("QT_SetRotTranslationType");
            
            foreach (var o in QtUtility.GetSelection)
            {    
                // Are we using random position?
                if (rotationType == 1)
                {
                    // If that's the case, its time to randomize the values!
                    axisX = Random.Range(randomMin.x, randomMax.x);
                    axisY = Random.Range(randomMin.y, randomMax.y);
                    axisZ = Random.Range(randomMin.z, randomMax.z);
                }
                
                // Register an undo state!
                Undo.RegisterFullObjectHierarchyUndo(o, "Set Rotation");
                
                var sel = (GameObject) o;
                
                // Do we have a parent? If not, use vector3.zero as a "parent"
                var rotation = sel.transform.rotation.eulerAngles;

                // We're using local transforms instead!
                if (setRotOrigin == 1 || setRotOrigin == 2)
                    rotation = sel.gameObject.transform.localRotation.eulerAngles;

                var x = rotation.x;
                var y = rotation.y;
                var z = rotation.z;
                
                //parentRot = sel.gameObject.transform.parent.localEulerAngles;
                // If we're doing relative, we're going off 0.
                if (isRelative)
                {
                    x = bAxisX ? axisX : 0f;
                    y = bAxisY ? axisY : 0f;
                    z = bAxisZ ? axisZ : 0f;
                }
                else
                {
                    x = bAxisX ? axisX : x;
                    y = bAxisY ? axisY : y;
                    z = bAxisZ ? axisZ : z;
                }
                
                // Finally add up and set!
                var setRotation = new Vector3(x, y, z);
                SetRotation(sel, setRotation, setRotOrigin, isRelative);
            }
        }

        public static void RepeatScale()
        {
            // Booleans of axis we're manipulating
            var bAxisX = QtUtility.LoadBool("QT_Scale_xAxis");
            var bAxisY = QtUtility.LoadBool("QT_Scale_yAxis");
            var bAxisZ = QtUtility.LoadBool("QT_Scale_zAxis");
            
            // values of axis
            var axisX = QtUtility.LoadFloat("QT_Scale_axis.x");
            var axisY = QtUtility.LoadFloat("QT_Scale_axis.y");
            var axisZ = QtUtility.LoadFloat("QT_Scale_axis.z");

            // The Action origin. 0 == World | 1 == Parent | 2 == Local
            var setScaleOrigin = QtUtility.LoadInt("QT_SetScaleOrigin");
            
            // Are we doing incremental? 0 == false | 1 == true
            var isRelative = QtUtility.LoadInt("QT_SetScaleIsRelative") != 0;
            
            // Random  Scaleition MIN
            var randomMin = new Vector3(QtUtility.LoadFloat("QT_Scale_randomMin.x"), QtUtility.LoadFloat("QT_Scale_randomMin.y"), QtUtility.LoadFloat("QT_Scale_randomMin.z"));
            
            // Random  Scaleition MAX
            var randomMax =  new Vector3(QtUtility.LoadFloat("QT_Scale_randomMax.x"), QtUtility.LoadFloat("QT_Scale_randomMax.y"), QtUtility.LoadFloat("QT_Scale_randomMax.z"));
            
            // our translation type
            var scaleType = QtUtility.LoadInt("QT_SetScaleTranslationType");

            foreach (var o in QtUtility.GetSelection)
            {
                // Are we using random scale?
                if (scaleType == 1)
                {
                    // If that's the case, its time to randomize the values!
                    axisX = Random.Range(randomMin.x, randomMax.x);
                    axisY = Random.Range(randomMin.y, randomMax.y);
                    axisZ = Random.Range(randomMin.z, randomMax.z);
                }
                
                // Register an undo state!
                Undo.RegisterFullObjectHierarchyUndo(o, "Set Scale");
                
                var sel = (GameObject) o;
                
                // Set up parent info.
                var parentObj = sel.gameObject.transform.parent;
                var parent = (parentObj != null)
                    ? parentObj.transform.localScale
                    : Vector3.one;

                // Set up basic
                var scale = sel.gameObject.transform.localScale;
                var localScale = (setScaleOrigin == 0) ? Vector3.Scale(scale, parent) : scale;

                // If we're doing relative stuff,
                // we need to add to our current.
                if (isRelative)
                {
                    axisX = (bAxisX) ? (localScale.x + axisX) : localScale.x;
                    axisY = (bAxisY) ? (localScale.y + axisY) : localScale.y;
                    axisZ = (bAxisZ) ? (localScale.z + axisZ) : localScale.z;
                }
                
                // add our changes!
                localScale.x = (bAxisX) ? axisX / parent.x : localScale.x / parent.x;
                localScale.y = (bAxisY) ? axisY / parent.y : localScale.y / parent.y;
                localScale.z = (bAxisZ) ? axisZ / parent.z : localScale.z / parent.z;
                
                // if we're based on the parent space, we need to scale it to match
                localScale = (setScaleOrigin == 1) ? Vector3.Scale(localScale, parent) : localScale;
                
                // Finally set the scale
                sel.gameObject.transform.localScale = localScale;
            }
        }
    }
}