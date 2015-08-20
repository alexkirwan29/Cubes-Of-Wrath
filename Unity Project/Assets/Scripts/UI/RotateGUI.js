function OnGUI () {
   var pos : Vector3 = new Vector3(100, 200, 0); //position for matrix
   var quat : Quaternion = Quaternion.identity; //rotation for matrix
   quat.eulerAngles = Vector3(0, 0, 10); //set the rotation to something - rotate around z!
   GUI.matrix = Matrix4x4.TRS(pos, quat, Vector3.one); //Apply the matrix 
   GUI.Label(Rect(0, 0, 200, 20), "Some Text"); //notice how the rect starts at 0/0 and the matrix handles the position!
}