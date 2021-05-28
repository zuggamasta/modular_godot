using Godot;
using System;

public class CurveSignal : Line2D
{
   
    [Export]
    public float scale =256;
    [Export]
    public float amplitude =1;
    [Export]
    public float offset = 0;
    
    [Export]
    Curve helper;

    float animator;
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {        
        for(int i = 0; i < this.Points.Length; i++){
            var xPos = (float)i/(float)this.Points.Length;
            var yPos = Mathf.Sin(xPos*4*Mathf.Pi-Mathf.Pi*2*animator)*0.5f;

            if(amplitude!=1){
                yPos = yPos*amplitude*helper.Interpolate(animator/4f);
            }else{
                yPos = yPos*amplitude;

            }

            if(offset!=0){
                yPos = yPos-offset*helper.Interpolate(animator/4f);
            }else{
                yPos = yPos-offset;
            }
            //yPos = yPos-offset;
            this.SetPointPosition(i, new Vector2(xPos,yPos)*scale);
        }

        animator += delta/2;
        if(animator>4) animator = 0;

    
    }
}
