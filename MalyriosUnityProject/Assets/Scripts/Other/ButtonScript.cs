using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    public static bool AttackInput;
    public static bool OpenInventoryInput;
    public static bool InteractInput;
    public static bool JumpInput;
    public static bool DodgeInput;
    public static bool Ability1Input;
    public static bool onInteractClick;


    Button buttons;

    void Start()
    {
    buttons = GetComponent<Button>();
    AttackInput = false;
    InteractInput = false;
    JumpInput = false;
    DodgeInput = false;
    OpenInventoryInput = false;
    Ability1Input = false;
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

    //Attack Button
    public void ClickAttackButton()
    {
        AttackInput = true;
        
    }
    void StopAttackInput()
    {
        AttackInput = false;
    }
    
    //Interact Button
    public void ClickInteractButton()
    {
        InteractInput = true;
    }
    public void ReleaseInteractButton()
    {
        InteractInput = false;
    }

    //Dodge Button
    public void ClickDodgeButton()
    {
        DodgeInput = true;
    }
    void StopDodgeInput()
    {
        DodgeInput = false;
    }

    //Ability 1 Button
    public void ClickAbility1Button()
    {
        Ability1Input = true;
    }
    public void ReleaseAbility1Button()
    {
        Ability1Input = false;
    }


}
