# modular_godot

## A modular system to explain modular systems.

This project is a long weekend (dream) project come true. While planning out a small fun presentation on modular synthesis I wondered if I can make a custom tool to spice things up. Turns out that's absolutely possible.

The tool is build in and around the godot game engine, using the c#/mono branch. That said, this part of godot boots with a big warning that it's still experimentals, take this as a note of caution. It's currently setup to build to the web, but this will halve limited features (loading of assets)

### Building this Project
To build and run this project: First get [godotengine.org/download](https://godotengine.org/download) (it's free and open source). You'll also need to setup your system to support the c#/mono workflow with Godot. It's not that hard and there is a good guide for that available on the wiki ([c_sharp_basics](https://docs.godotengine.org/en/stable/getting_started/scripting/c_sharp/c_sharp_basics.html)).
After that it's not much more than opening this project in godot and hitting play in the editor. 

### Screenhsots
examples | gifs
------------ | -------------
One of the main features of this project is ability to draw node connections in the style of hanging cables.  | ![cables](https://user-images.githubusercontent.com/34277191/120923922-6763f280-c6d1-11eb-847e-853c7ab4c901.gif)
Another possibility is the drawing and animation of free hand signal lines. | ![drawing1](https://user-images.githubusercontent.com/34277191/121086111-0af0f800-c7e3-11eb-96da-c8e9887087a3.gif)
There is also the ability to load slides in the jpg or PNG format with 1920x1080p. Drawings and cables will stick to their respective page. | ![slides](https://user-images.githubusercontent.com/34277191/120924172-a47cb480-c6d2-11eb-9210-c7053f3d0623.gif)

### The Cable Setup 
The cable connections in this project are no cloth/physics or verlet simluations (Sorry for that). But I think they still turned out nice and move pretty belivable and coherently. As basis for the shape of the cable I am using the Sine function form 0 to Pi. That function is applied to give the cables their arc, another more accurate approach would be to use the Catenary.

For the animation of the cables each point is moving along a small circle, with the start position of the rotation offset by the distance from startpoint to endpoint.
![cable](https://user-images.githubusercontent.com/34277191/121775265-e1d7bb00-cb86-11eb-907e-b47a8922ef1a.png)
After left mouse is released the animation plays along but the radius is reduced for the lenght of the decay set in the varibales.

The Cables are Instanced with *Scritps/CableIncstancer.cs* and use the *Prefabs/Cable.tscn* and *Scripts/Cable.cs*



### Notes
This whole thing is still in a experimental phase, but if you're up for it take a look, handle with care and have fun!
