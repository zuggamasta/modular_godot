using Godot;
using System;

public class DrawInstancer : Node2D
{

    [Export]
    public String instancePath = "res://Prefabs/Draw.tscn";
    PackedScene instance;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("ui_toggle")){
            instance = (PackedScene)ResourceLoader.Load(instancePath);
            var newNode = (Node2D)instance.Instance();
            this.AddChild(newNode);
        }

        if(Input.IsActionJustPressed("ui_delete")&&GetChildCount()>0){
            GetChild(GetChildCount()-1).QueueFree();
        }

        if(Input.IsActionJustPressed("ui_cancel")){
            foreach(Node2D node in this.GetChildren()){
                node.QueueFree();
            }
        }
    }
}
