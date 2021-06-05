using Godot;
using System;

public class CableInstancer : Node2D
{
    [Export]
    public String instancePath = "res://Prefabs/Cable.tscn";
    PackedScene instance;

    Node2D cableHolder;

    public bool active;
    private Node2D highlight;

    public override void _Ready()
    {
        highlight = GetNode<Node2D>("active");
        // not yet implemented
        cableHolder = GetNode<Node2D>("/root/MainScene/SlideLoader/CableHolder");
    }

    public override void _Process(float delta)
    {
        if(active){
            if(Input.IsActionJustPressed("left_click")){
                // define Save area for cables
                if(GetGlobalMousePosition().y > 74f){
                    
                    //instance Cable
                    instance = (PackedScene)ResourceLoader.Load(instancePath);
                    var newNode = (Node2D)instance.Instance();
                    this.AddChild(newNode);
                }
                
            }
            // check if cables are available, and mark for freeing cable holder is not yet used
            // due to this cables currently wont be makred for deletion
            if(Input.IsActionJustPressed("ui_delete")&&cableHolder.GetChildCount()>0){
                    cableHolder.GetChild(cableHolder.GetChildCount()-1).QueueFree();
                
            }
            // visual feedback
            highlight.Visible = true;
        }else{
            highlight.Visible = false;
        }

        // visual feedback toggle
        if(Input.IsActionJustPressed("ui_accept")){
            active = !active;
        }

        // visual feedback exit on load
        if(Input.IsActionJustPressed("ui_draw")){
            active=false;
        }
        
    }
}
