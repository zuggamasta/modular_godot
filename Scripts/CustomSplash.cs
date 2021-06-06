using Godot;
using System;

public class CustomSplash : Node2D
{
    
    float animator = 0f;

    [Export]
    float length = 1f;

    [Export]
    Curve curve;
    Camera2D camera;
    TextureRect splash;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera = GetChild<Camera2D>(0);
        splash = GetNode<TextureRect>("MarginContainer/VBoxContainer/HBoxContainer/Splash");
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        
        if(animator<length){
            camera.Zoom = Vector2.One * curve.Interpolate(animator/length);
            splash.Modulate = Color.Color8(255,255,255,(byte)(animator/length*255f));
        }

        if(animator>=length+1f){
            GetTree().ChangeScene("res://Scenes/MainScene.tscn");
        }

        animator+=delta;
    }
}
