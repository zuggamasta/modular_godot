using Godot;
using System;

namespace Parent{


public class ParentManager : Node
{
    // manage new parent and keep transform
    public static void KeepTransform(Node2D child, Node2D newParent){
        var oldPos = child.GlobalPosition;
        child.GetParent().RemoveChild(child);
        newParent.AddChild(child);
        child.GlobalPosition = oldPos;
    }

    // manage new parent and dont keep transform (godot default behavior)
    public static void DontKeepTransform(Node2D child, Node2D newParent){
        child.GetParent().RemoveChild(child);
        newParent.AddChild(child);
    }

}

}