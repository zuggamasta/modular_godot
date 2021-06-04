using Godot;
using System;

public class CableInstancer : Node2D
{
    [Export]
    public String instancePath = "res://Prefabs/Cable.tscn";
    PackedScene instance;

    public bool active;
    private Node2D highlight;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        highlight = GetNode<Node2D>("active");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(active){
            if(Input.IsActionJustPressed("left_click")){
                // define Save are for cables
                if(GetGlobalMousePosition().y > 74f){
                    instance = (PackedScene)ResourceLoader.Load(instancePath);
                    var newNode = (Node2D)instance.Instance();
                    this.AddChild(newNode);
                }
                
            }
            if(Input.IsActionJustPressed("ui_delete")&&GetChildCount()>0){
                    GetChild(GetChildCount()-1).QueueFree();
                
            }
            highlight.Visible = true;
        }else{
            highlight.Visible = false;
        }


        if(Input.IsActionJustPressed("ui_accept")){
            active = !active;
        }

        if(Input.IsActionJustPressed("ui_toggle")){
            active=false;
        }
        
    }
}
