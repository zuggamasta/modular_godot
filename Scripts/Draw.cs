using Godot;
using System;
using System.Collections.Generic;

public class Draw : Node2D
{

    List<Vector2> linePoints = new List<Vector2>();

    Line2D line;
    [Export]
    float threshold = 5f;
    Vector2 last;
    Vector2 current;

    float animator;
    bool active;

    bool done;
    public override void _Ready()
    {
        line = GetNode<Line2D>("Line2D");
    }
    public override void _Process(float delta)
    {
        if(!done)GatherPoints();
        
        if(done&&animator<=0f){
            animator = delta*3f;
            LastToFirst(linePoints);
        }
        if(animator>0){
            animator-=delta;
        }
        RenderCurve();
    }

    public void GatherPoints(){
        if(Input.IsActionJustPressed("left_click")){
            last = GetGlobalMousePosition();
            active=true;
        }
        if(Input.IsActionPressed("left_click")&&active){
            current = GetGlobalMousePosition();
            if(Mathf.Abs(last.x-current.x)>threshold){
                linePoints.Add(current);
                last = current;
            }
        }
        if(Input.IsActionJustReleased("left_click")){
            active = false;
            done =true;
        }
    }

    public void RenderCurve(){
        if(linePoints.Count!=0){
            if(!done){
                line.Points = linePoints.ToArray();
            }else{
                line.Points = linePoints.ToArray();
            }
        }
    }

    public List<Vector2> LastToFirst(List<Vector2> list){
        var array = list.ToArray();
        int index = array.Length-1;
        Vector2 last = array[index];
        for(int i =index ; i > 0 ; i--){
            array[i].y = array[i-1].y;
            if(i == 1)        array[0].y=last.y;

        }
        list.Clear();
        foreach(Vector2 a in array){
            list.Add(a);
        }
        return list;
    }
}
