using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static bool receivedAttackInput;
    public static bool receivedOpenInventoryInput;
    public static bool receivedInteractInput;
    public static bool receivedJumpInput;
    public static bool receivedDodgeInput;
    public static bool receivedAbility1_input;
    public static bool onInteractClick;


    Button buttons;

    void Start()
    {
    buttons = GetComponent<Button>();
    receivedAttackInput = false;
    receivedInteractInput = false;
    receivedJumpInput = false;
    receivedDodgeInput = false;
    receivedOpenInventoryInput = false;
    receivedAbility1_input = false;
    onInteractClick = false;
    buttons.gameObject.SetActive(true);
       
    }

    public void OnInteractClick(){

        if(!onInteractClick){
            onInteractClick = true;
        }else{
            onInteractClick = false;
        }
    }


    //Open Inventory Button
/*     public void OpenInventoryButton()
    {
        print("reciOpenInv:"+receivedOpenInventoryInput );
        receivedOpenInventoryInput = true;
       
    } */


    //Attack Button
    public void ClickAttackButton()
    {
        receivedAttackInput = true;
        Invoke("StopAttackInput", 0.1f);
    }
    void StopAttackInput()
    {
        receivedAttackInput = false;
    }
    
    //Interact Button
    public void ClickInteractButton()
    {
        receivedInteractInput = true;
    }
    public void ReleaseInteractButton()
    {
        receivedInteractInput = false;
    }

    //Dodge Button
    public void ClickDodgeButton()
    {
        receivedDodgeInput = true;
        Invoke("StopDodgeInput", 0.1f);
    }
    void StopDodgeInput()
    {
        receivedDodgeInput = false;
    }

    //Jump Button
    public void ClickJumpButton()
    {
        receivedJumpInput = true;
    }
    public void ReleaseJumpButton()
    {
        receivedJumpInput = false;
    }


    //Ability 1 Button
    public void ClickAbility1Button()
    {
        receivedAbility1_input = true;
    }
    public void ReleaseAbility1Button()
    {
        receivedAbility1_input = false;
    }


}
