using Godot;
using System;

namespace Parent{


public class ParentManager : Node
{
    public static void KeepTransform(Node2D child, Node2D newParent){
        var oldPos = child.GlobalPosition;
        child.GetParent().RemoveChild(child);
        newParent.AddChild(child);
        child.GlobalPosition = oldPos;
    }

}

}