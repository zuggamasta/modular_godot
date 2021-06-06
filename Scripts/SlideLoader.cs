using Godot;
using System;
using System.Collections.Generic;

public class SlideLoader : Node2D
{
    public Vector2 window = new Vector2(1920f,1080f);

    // File path and slide data
    List<String> filepath = new List<String>();
    List<Sprite> slides = new List<Sprite>();

    HBoxContainer slideContainer;
    bool confirmed;
    int activeSlide =0;
    FileDialog filePopup;
    float slideAnimator = 0f;

    // Camera data
    Camera2D camera;
    float zoom = 1;
    float cameraZoom = 1;

    //  ###############################  //
    //  # SLIDES ARE EXPECTED TO HAVE #  //
    //  # A RESOLUTION OF 1920x1080px #  //
    //  ###############################  //

    public override void _Ready()
    {
        // assing nodes
        camera = GetNode<Camera2D>("/root/MainScene/Camera2D");
        filePopup = GetNode<FileDialog>("MarginContainer/VBoxContainer/FileDialog");
        slideContainer = GetNode<HBoxContainer>("MarginContainer/VBoxContainer/HBoxContainer");
    }

    public override void _Process(float delta)
    {
        LoadSlides();
        MoveSlides(delta);      
        CameraZoom(delta);
        
    }
    // Handle slide ui and loading/instancing 
    public void LoadSlides(){

        if(Input.IsActionJustPressed("ui_cancel")){
            // Center popup
            filePopup.Popup_(new Rect2(window.x/4,window.y/4,window.x/2f,window.y/2f));
            filePopup.CurrentPath ="res://Assets/";

            // If available, delete all old slide nodes
            foreach(TextureRect texRec in slideContainer.GetChildren()){
                texRec.QueueFree();
            }
        }

        if(filepath != null && confirmed){
            // instance a texturerect for all seperate paths found
            foreach(String fp in filepath){
                GD.Print(fp);
                var texture = ResourceLoader.Load(fp) as Texture;
                var newNode = new TextureRect();  
                newNode.Texture = texture;   
                slideContainer.AddChild(newNode);               
                // For debugging
                //GD.Print(newNode);
            }
            confirmed = !confirmed;
            filepath.Clear();
        }
    }

    // Handle slide input and current position 
    public void MoveSlides(float delta){
        if(Input.IsActionJustPressed("ui_left")){
            activeSlide--;
            slideAnimator =1f;
        }

        if(Input.IsActionJustPressed("ui_right")){
            activeSlide++;
            slideAnimator=1f;
        }

        if(slideAnimator>0){
            this.Position= Position.LinearInterpolate(Vector2.Left*window*activeSlide, 1-slideAnimator);
            slideAnimator-=(delta*0.1f);
            
        }
        if(slideAnimator<0.10f){
            Position = Vector2.Left*window*activeSlide;
            slideAnimator = 0f;
        } 
    }
    
    //  Handle camera zoom and animation
    public void CameraZoom(float delta){
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

    // Signals
    public void _on_FileDialog_files_selected(String[] path)
    {
        filepath.Clear();
        foreach(String st in path){
            filepath.Add(st);
        }
        // for debugging
        //GD.Print(filepath);
        
        return;
    }

     public void _on_FileDialog_confirmed()
    {
        // always reset active slide to the first slide
        activeSlide =0;
        // for debugging
        //GD.Print("confirmed");
        confirmed = true;
        return;
    }

}
