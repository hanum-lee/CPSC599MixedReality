# CPSC 599 Mixed Reality Assignment 1: Unity Chan
## How to use
* Open scene A1 in the Scenes folder.
* Set Vuforia camera settings through the ARCamera to select the camera you would like to use.
* Capture the Vuforia Astronaut default image with the camera.
* Character will move automatically.
* There is 3 voice command you can use to control this, "Resume", "Pause", "Jump"
  * Resume : Resume the character movement before final reaching destination
  * Pause : Stops the character movement before reaching final destination
  * Jump : Make the character jump before reaching final destination
* Tilting the camera will make the character faster or slower
  * Tilting left : Decreases the speed of the character until 0
  * Tilting right: Increase the speed of the character so it can move 2 times faster at max speed


[Working video](https://youtu.be/c-7l25H1n9k)
## Bug...?
* The increment of the speed using tilting feature should work if the orientation of the picture is same as in the video.

This sample project uses the free character from https://assetstore.unity.com/packages/3d/characters/humanoids/character-pack-free-sample-79870
