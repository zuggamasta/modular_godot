using Godot;
using System;

public class GUI : MarginContainer
{
    

    public override void _Ready()
    {       
        //Load NextScene

    }

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

    }


    private void _on_Button1_button_up(){
        Input.ActionPress("ui_cancel");
    }

    private void _on_Button2_button_up(){
        Input.ActionPress("ui_left");
    }

    private void _on_Button3_button_up(){
        Input.ActionPress("ui_right");

    }

        private void _on_Button4_button_up(){
        Input.ActionPress("ui_accept");

    }

    private void _on_Button5_button_up(){
        Input.ActionPress("ui_toggle");

    }



}
