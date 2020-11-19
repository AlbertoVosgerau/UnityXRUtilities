In this project you will find tools, scripts and prefabs created in order to speed up my the creation of common tasks in UnityXR that I tough could be useful to start the ball rolling for projects that can already support the toggle between VR and non VR.

DATA
	The folder UnityXRUtilities/Data contains a ScriptableObject that stores the defult controllers for many devices. You can add any prefab to the list, but make sure its name is precisely the one unity registers for the device you want.
In order to add a controller to a scene, you need a XR Rig and the prefabs RegisterInputDevices and DeviceControllerModelCreator in the scene.

PREFABS

	The folder UnityXRUtilities/Prefabs contains the following folders and prefabs:


Input Models/Controllers:

It has the prefabs for all the devices controllers available. This was actually put together by Quentin Valembois from the youtube channel Velem, all credits to him.

Input Models/Hands:

It has the default hands models from Oculus inside.


UI:

- Radial Menu Canvas: This prefab implements a Radial UI that you can add to a controller and use the controller joystick to select. Just grab it to a scene and define XRNode as a hand.

Utils:

- RegisterInputDevices: This prefab register and keeps looking for controllers it right or left controllers are not found. It then registers ir as right or left controller into XRInputDevices, that you can access anywhere. All my scripts uses this method to easily acess controllers. For example, if you want to get an input from RightController after dragging this prefab to a scene, you can just call XRInputDevices from any script.

- DeviceControllerModelCreator: This prefab instantiates controllers based on the device, so you might get the real controller for a variety of devices with no effort. If you have a controller in particular that you want to add, just drag it to the database found in UnityXRUtilities/Data. You need also a XR Rig and the RegisterInputDevices on the scene.

- HandModelCreator: Instantiates hands to a XR Rig.

- InputModelsVisibilityHandler: When you have both DeviceControllerModelCreator and HandModelCreator in the scene, add this to handle which one you want to show. 


You can check the examples scenes folder for ideas of everything that is implemented as well as some prefabs made with standard Unity tools.

If you are importing the package and need to create a project, I do recommend the Valem Introduction to VR videos:
https://www.youtube.com/watch?v=NU_cLqYrYjo&list=PLrk7hDwk64-a_gf7mBBduQb3PEBYnG4fU

After you have a simple project set, since this project implements the capability to switch between VR (which uses the new Unity Input System) and non VR, you will need to go to PlayerSettings> Other Settings > Active Input Handling and switch it to Both.

If you want to add Steam VR support, as for now (November 2020), you have to add the XR module manually, my downloading and importing the package from here:
https://github.com/ValveSoftware/unity-xr-plugin/releases/tag/installer

If you have any doubts or tips, please feel free to contact me at:

alberto@argames.com.br