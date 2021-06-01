using Godot;
using System;
using System.Collections.Generic;

public class SlideLoader : Node2D
{
    public Vector2 window = new Vector2(1920f,1080f);

    List<String> filepath = new List<String>();
    List<Sprite> slides = new List<Sprite>();
    bool confirmed;

    Camera2D camera;
    float zoom = 1;
    float cameraZoom = 1;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        camera = GetNode<Camera2D>("/root/MainScene/Camera2D");
    }

  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("ui_cancel")){
            GetNode<FileDialog>("MarginContainer/VBoxContainer/FileDialog").Popup_(new Rect2(window.x/4,window.y/4,window.x/2f,window.y/2f));
            GetNode<FileDialog>("MarginContainer/VBoxContainer/FileDialog").CurrentPath ="res://Assets/";
        }

        if(filepath != null && confirmed){
            foreach(String fp in filepath){
                GD.Print(fp);
                var texture = ResourceLoader.Load(fp) as Texture;
                var newNode = new TextureRect();  
                newNode.Texture = texture;   
                GetNode<HBoxContainer>("MarginContainer/VBoxContainer/HBoxContainer").AddChild(newNode);               
                GD.Print(newNode);
            }
            
            confirmed = !confirmed;
            filepath.Clear();
        }

        if(Input.IsActionJustPressed("ui_left")){
            this.Translate(Vector2.Left*-window.x);  
        }
        if(Input.IsActionJustPressed("ui_right")){
            this.Translate(Vector2.Left*+window.x);  
        }
        if(Input.IsActionJustPressed("right_click")){
            if(zoom <= 1f){
                zoom = 2f;
            }else{
                zoom = 1f;
            }
        }

        if(cameraZoom < zoom){
            cameraZoom += delta;
        }
        if(cameraZoom> zoom){
            cameraZoom -= delta;
        }
        camera.Zoom = Vector2.One*cameraZoom;
    }

    public void _on_FileDialog_files_selected(String[] path)
    {
        foreach(String st in path){
            filepath.Add(st);
        }
        GD.Print(filepath);
        
        return;
    }

     public void _on_FileDialog_confirmed()
    {
        GD.Print("confirmed");
        confirmed = true;
        return;
    }
}
