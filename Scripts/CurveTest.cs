using Godot;
using System;

public class CurveTest : Node2D
{
    
    [Export]
    public Curve testCurve;

    [Export]
    public float scale =256;
    [Export]
    public float amplitude =1;
    [Export]
    public float offset = 0;
    Line2D line;
    
    float animator;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        line = GetNode<Line2D>("Line2D");
        
    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {        
        for(int i = 0; i < line.Points.Length; i++){
            var xPos = (float)i/(float)line.Points.Length;
            var yPos = testCurve.Interpolate(xPos);
            line.SetPointPosition(i, new Vector2(xPos,yPos*amplitude+offset)*scale);
        }

        animator += delta/4;
        if(animator>1) animator = 0;

    
    }
}
