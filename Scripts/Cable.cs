using Godot;
using System;
using Parent;

public class Cable : Godot.Node2D
{
    //Nodes
    Sprite one;
    Sprite two;
    Line2D line;
    Node2D parent;

    // Cable Data
    bool isFixed = false;
    Vector2 initial;
    Vector2 currentPos;

    // Animation Data
    float decay;
    float animator;

    [Export]
    public float wiggleSpeed = 10;
    [Export]
    public float wiggleAmplitude =16;
    [Export]
    public float gravity = 256; // <--- max gravity in pixel
    
    public override void _Ready()
    {
        // assing nodes
        one = GetChild<Sprite>(0);
        two = GetChild<Sprite>(1);
        line = GetChild<Line2D>(2);
        parent = GetNode<Node2D>("/root/MainScene/SlideLoader/DrawHolder");

        // get initial position
        initial =  GetGlobalMousePosition();
        Position = initial;

        // set color offset to make cables distinguishable. Color is not random, but set by the position on screen.
        line.DefaultColor = Color.FromHsv(166f/360f
            +Mathf.Abs(GetGlobalMousePosition().x/1920f/2 +
            GetGlobalMousePosition().y/1080f/2)*0.12f
        ,0.76f,0.69f);
    }

    public override void _Process(float delta)
    {
        if(!Input.IsActionJustReleased("left_click") && !isFixed){
            var currentPos = GetLocalMousePosition();
            one.Position = currentPos;

            // Update all points in line
            for(int i = 1; i < line.Points.Length; i++){
                // relative x position of point in line
                float percent = ((float)i/(float)line.Points.Length);
                // pixel x position of point in line
                float xPos = 
                    (currentPos.x-0f)*percent
                    +Mathf.Sin(
                        animator*wiggleSpeed
                        +percent*2*Mathf.Pi)
                    *wiggleAmplitude;
                // pixel y position of point in line
                float yPos =
                    (Mathf.Sin(percent*Mathf.Pi))*gravity
                    +(currentPos.y-0f)
                    *percent+Mathf.Cos(
                        animator*wiggleSpeed
                        +percent*2*Mathf.Pi)
                    *wiggleAmplitude;

                var middlePos = new Vector2(xPos,yPos);
                line.SetPointPosition(i, middlePos);
            }
    
            line.SetPointPosition(line.Points.Length-1,currentPos);            
        }else{
            if(Input.IsActionJustReleased("left_click")&& !isFixed){
                decay= 1f;
            }
            // fix everything while the animation starts
            isFixed = true;
            ParentManager.KeepTransform(this,parent);

            
        }
        
        if(decay>0){
            var currentPos = one.Position;
            
            // Update all points in line
            for(int i = 1; i < line.Points.Length; i++){
                // relative x position of point in line
                float percent = ((float)i/(float)line.Points.Length);
                // pixel x position of point in line
                float xPos = 
                    (currentPos.x-0f)*percent
                    +Mathf.Sin(
                        animator*wiggleSpeed
                        +percent*2*Mathf.Pi)
                    *wiggleAmplitude*decay;
                // pixel y position of point in line
                float yPos =
                    (Mathf.Sin(percent*Mathf.Pi))*gravity
                    +(currentPos.y-0f)*percent
                    +Mathf.Cos(
                        animator*wiggleSpeed
                        +percent*2*Mathf.Pi)
                    *wiggleAmplitude*decay;
                    
                var middlePos = new Vector2(xPos,yPos);
                line.SetPointPosition(i, middlePos);
            }
            line.SetPointPosition(line.Points.Length-1,currentPos);
            decay-=delta;            

        }


        animator += delta;


    }


}
