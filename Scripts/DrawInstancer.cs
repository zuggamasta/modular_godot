using Godot;
using System;

public class DrawInstancer : Node2D
{

    [Export]
    public String instancePath = "res://Prefabs/Draw.tscn";
    PackedScene instance;
    Node2D drawHolder;

    public override void _Ready()
    {
        drawHolder = GetNode<Node2D>("/root/MainScene/SlideLoader/DrawHolder");
    }

    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("ui_draw")){
            // instance line
            instance = (PackedScene)ResourceLoader.Load(instancePath);
            var newNode = (Node2D)instance.Instance();
            this.AddChild(newNode);
        }

        // if lines are present delete the last node
        if(Input.IsActionJustPressed("ui_delete")&&drawHolder.GetChildCount()>0){
            drawHolder.GetChild(drawHolder.GetChildCount()-1).QueueFree();
        }

        // if lines are present delete allnodes
        if(Input.IsActionJustPressed("ui_cancel")){
            foreach(Node2D node in this.GetChildren()){
                node.QueueFree();
            }
        }
    }
}
