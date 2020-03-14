using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public static bool receivedAttackInput;
    public static bool receivedJumpInput;
    public static bool receivedDodgeInput;


//Attack Button
public void ClickAttackButton(){
     receivedAttackInput = true;
     Invoke("StopAttackInput", 0.1f);
 }
 void StopAttackInput(){
     receivedAttackInput = false;
 }

//Dodge Button
public void ClickDodgeButton(){
     receivedDodgeInput = true;
     Invoke("StopDodgeInput", 0.1f);
 }
 void StopDodgeInput(){
     receivedDodgeInput = false;
 }

//Jump Button
 public void ClickJumpButton(){
     receivedJumpInput = true;
 }
public void ReleaseJumpButton(){
    receivedJumpInput = false;
}



}
