# Navigation
Dongha Kang


*Navigating 3D models in Virtual Reality is a combination of First-Person view and teleportation. Head tracking works with most devices and lets you look around.*

---
## Types of Navigation
1. Teleportation

Simple code of teleportation.
```
if (transportTrigger && buttonReleased)
{
    transitionProgress = 0;
    currentLocation = transform.position;
    endLocation = transportLocation;
}

if (transitionProgress < 1)
{
    transitionProgress += Time.deltaTime / transitionTime;
    transform.position = Vector3.Lerp(currentLocation, endLocation, transitionProgress);
}
```

2. Walking
3. Turning / Rotation
4. Flying
5. Menu Tool


## Instruction
1. By pressing A button, hand directed steering is enabled.
   For convenience, only right hand's hand directed steering is working.

2. Left hand's joystick is for snap turn. While right hand's joystick is for
   moving forward/backward and rotating, left hand's joystick is for snap turn and moving forward/backward.

3. By Pressing down the joystick, the line renderer will be activated.
   if there is something that hits by the ray, line color will be changed to red and blue.
   If not, the ray color will be black. If the line color stays colorful and lets go,
   it will move really fast to the destination where the ray was hit.


*Environment link*
https://assetstore.unity.com/packages/3d/environments/landscapes/lowpoly-environment-pack-99479
