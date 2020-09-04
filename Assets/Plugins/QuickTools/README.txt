//-------------------------------------------
//            QuickTools v0.3
//     Copyright © 2020 Kenneth Vassbakk
//          kennethvassbakk.com
//-------------------------------------------

QuickTools is a collection transform tools that can be performed on a selection of objects.

USAGE:
Window/QuickTools/Open Window
or through the floating window used with CTRL+Q

FUNCTIONALITY:

Position: 
    Reset:
        Reset position axis' based on space [World,Parent]
    
    Set:  
        Set position of selection based on [World,Parent,Local] space.
        Either incremental or absolute.
        
        Value based on absolute value or range.
        
        
Rotation:
    Reset:
        Reset rotation axis' based on space [World,Parent]
    
    Set:
        Set the  rotation of the object based on the rotation axis of [World, Parent, Local]
        Either based on absolute value or range.
        
        Note:
            This does NOT rotate AROUND the parent, it rotates in regards to the same transform handle as its parent.
            Meaning, if the parent object is offset in rotation to its child, the child will temporarily
            copy the parents rotation, and rotate around the same axis. Think of it as a reset rotation "with offset"
            
        Note2:
            Random rotation is very useful for placement of barrels, trees, etc in a level,
            ensuring they are not all at the same rotation.
        
Scale:
    Reset: 
        Reset position axis' based on space [World,Parent]
        
    Set
        Set the scale of the object based on axis, in the space of [World, Parent]
        Either on absolute value, or range.
        Incremental or absolute value.
        
        Note: 
            Not quite sure about this one,
            currently its scaling the object based on its localScale (which is the only accessible)
            However, should scale work in the axis of the desired space?
            For instance: Rotate a box inside a parent which is not rotated.
                Should the box then scale like that with a World Space? :shrug: