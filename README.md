# 3D-Demo for Door Configuration
Key introduction for use : 
Up Arrow Key : Forward Straight Move
Down Arrow Key : Backward Straight Move
Left Arrow Key : Left Move
Right Arrow Key : Right Move
G Key : Grab Object
M Key : Move Handle Up/Down Side
Tab Key : Change Texture of the door (it will be done
after completed handle movement
functionality)

Left Button on Mouse and Mouse Movement Move your angle of vision (it will be done only
when you select hand and move your mouse)
Up Arrow Key + Left Button on Mouse : Move your vision angle Upside (only
applicable when you select hand)
Down Arrow Key + Left Button on Mouse : Move your vision angle downside (only
applicable when you select hand)

Script Overview:
CameraMovement : For camera Movement by user input.
DoorController : It's a main script which handles grab and drop object.
Hand Object: HandMovement.cs controls hand object movement.
Grab Item: Script handles grabbing an object functionality.
Drop Item: Script manages dropping an object functionality.

Process for Door Configuration:

Place Door Object: Position the door object in the scene.
Attach DoorController.cs: Attached this script to the door object.
DoorDropZone Setup:
Created a child object named DoorDropZone for dropping objects.
Added a box collider and set it as trigger.
Attached DropZone.cs script to DoorDropZone.
Setup Snap Position:
Inside DoorDropZone, added a child object for snap positioning.
Lock Object Setup:
Created a lock object.
Attached GrabItem.cs script to the lock object.
Added the lock object reference to the grabbableItem list in DropZone.cs.
Also, give reference of the door DropZone to GrabItem.cs for drop functionality.
Handle Configuration:
Similarly, setup grab handle object.
Attached necessary scripts (GrabItem.cs, etc.).
After attaching handle above lock, ensure collision with hand object and press M Key to move handle up and down.
Texture Change:
After completing handle movement, press Tab Key to change the texture of the door.
