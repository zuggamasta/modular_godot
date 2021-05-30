using Godot;
using System;

public class GUI : MarginContainer
{
    
    // Called when the node enters the scene tree for the first time.
    [Export]
    PackedScene[] scenes;

    [Export]
    public int activeScene =0;
    int lastScene = -1;

    String lastSceneID;
    
    public override void _Ready()
    {       
            var newNode = (Node2D)scenes[activeScene].Instance();
            this.AddChild(newNode);
            lastSceneID = newNode.Name;
            GD.Print(lastSceneID);

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }


    private void _on_Button1_button_up(){
        Input.ActionPress("ui_accept");
    }

    private void _on_Button2_button_up(){
        activeScene--;
        if(activeScene<0) activeScene = scenes.Length-1;
        loadNextScene();
    }

    private void _on_Button3_button_up(){
        activeScene++;
        if(activeScene>=scenes.Length) activeScene = 0;
        loadNextScene();
    }

    private void loadNextScene(){
        var newNode = (Node)scenes[activeScene].Instance();
        GetNode(lastSceneID).QueueFree();
        this.AddChild(newNode);
        lastSceneID = newNode.Name;
        GD.Print(lastSceneID);
    }


}
