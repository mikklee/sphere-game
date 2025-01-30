using UnityEngine;

/* 
This class manages the 90deg rotations of a camera ("CamCube") around an object as well as the camera offset.
Authors: Michael T. Lee, Edvard Blunck
Year: 2016
Sources for inspiration:

*/


internal class CamScript
{
    //Variables
    public Vector3 initPosVector;
    float x = 0;
    float y = 0;
    float z = 0;
    private Vector3 CamOffsetBack;
    private Vector3 CamOffsetForward;
    private Vector3 CamOffsetRight;
    private Vector3 CamOffsetLeft;
    private Quaternion rotBack;
    private Quaternion rotForward;
    private Quaternion rotRight;
    private Quaternion rotLeft;
    private Quaternion rotUpsideDown;
    private string initialPos;
    public Vector3 cameraFollowOffset;
    public Quaternion initCamCubeRotation;
    //

    //Constructor.
    //Calculates all sides from given specified side.
    //@Param camera: Position of the camera "cube" (Pseudo-GameObject which is parent to the camera)
    //@Param gObject: Position of the game object to be followed
    //@Param rotation: Rotation of the camera "cube" 
    //@Param position: Specified starting position of the camera (Must be set manually)
    public CamScript(Vector3 camera, Vector3 gObject, Quaternion rotation, string position)
    {
        initialPos = position;
        cameraFollowOffset = camera - gObject;
        initCamCubeRotation = rotation;
        initPosVector = camera - gObject;
        y = initPosVector.y; //Height of cameraCube relative to gameObject
        switch (position) //Convert to "back-offset"
        {
            case "back":
                x = initPosVector.x;
                z = initPosVector.z;
                rotBack = rotation;
                rotLeft = makeRotationY(rotation, 90f);
                rotForward = makeRotationY(rotation, 180f);
                rotRight = makeRotationY(rotation, -90f);
                break;
            case "forward":
                x = initPosVector.x;
                z = -initPosVector.z;
                rotForward = rotation;
                rotRight = makeRotationY(rotation, 90);
                rotBack = makeRotationY(rotation, 180f);
                rotLeft = makeRotationY(rotation, -90f);

                break;
            case "right":
                x = initPosVector.z;
                z = -initPosVector.x;
                rotRight = rotation;
                rotBack = makeRotationY(rotation, 90);
                rotLeft = makeRotationY(rotation, 180f);
                rotForward = makeRotationY(rotation, -90f);
                break;
            case "left":
                x = initPosVector.z;
                z = initPosVector.x;
                rotLeft = rotation;
                rotBack = makeRotationY(rotation, -90);
                rotRight = makeRotationY(rotation, 180f);
                rotForward = makeRotationY(rotation, 90f);
                break;
        }
        rotUpsideDown = makeRotationZ(rotation, 180);
        CamOffsetBack = new Vector3(x, y, z);
        CamOffsetForward = new Vector3(x, y, -z);
        CamOffsetRight = new Vector3(-z, y, x);
        CamOffsetLeft = new Vector3(z, y, x);
}
    //Returns the camera offset (Distance between followed object and camera)
    //@Param string side: The requested side in string form. Eg. "Right".
    public Vector3 getCamOffset(string side) {
        switch (side) {
            case "forward": return CamOffsetForward;
            case "right": return CamOffsetRight;
            case "back": return CamOffsetBack;
            case "left": return CamOffsetLeft;
        }
        return Vector3.zero; //What happened here?
    }

    //Returns the rotation of a requested side.
    //@Param string side: The requested side in string form. Eg. "Right".
    public Quaternion getCamRotation(string side)
    {
        switch (side)
        {
            case "forward": return rotForward;
            case "right": return rotRight;
            case "back": return rotBack;
            case "left": return rotLeft;
        }
        return Quaternion.identity; //Someone lives in extradimensional spaces
    }

    public Quaternion getRotationUpsideDown() {
        return rotUpsideDown;
    }
    
    //Returns an Euler-angle rotation of an object, around it's y-axis. 
    //@Param Quaternion initial: The original rotation to change from
    //@Param float degrees: Degrees to rotate around the y-axis. 
    public Quaternion makeRotationY(Quaternion initial, float degrees) {
        return initial *= Quaternion.Euler(0, degrees, 0);
    }

    //Returns an Euler-angle rotation of an object, around it's z-axis. 
    //@Param Quaternion initial: The original rotation to change from
    //@Param float degrees: Degrees to rotate around the z-axis. 
    public Quaternion makeRotationZ(Quaternion initial, float degrees)
    {
        return initial *= Quaternion.Euler(0, 0, degrees);
    }

}//EOF