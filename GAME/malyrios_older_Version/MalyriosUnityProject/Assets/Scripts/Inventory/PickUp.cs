using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{

    Transform player;
    float distance;
    bool showText;
    [SerializeField] TextMeshProUGUI tmpText;
    public Item item;
    SpriteRenderer spriteRenderer;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = item.icon;
    }

    // Update is called once per frame
    void Update()
    {
        

        distance = Vector2.Distance(gameObject.transform.position, player.position);//Mathf.Abs(gameObject.transform.position.x - player.position.x);
        if (distance < 0.15f)
        {
            ShowPickUpDialog();
            showText = true;

            //Pick item Up, if player interacts with it
            if (Player.interactInput)
            {
                PickUpItem();
            }

        }else if(showText && distance >= 0.3f){
             tmpText.gameObject.SetActive(false);
             showText = false;
        }
        
    }

    void ShowPickUpDialog()
    {
      
        tmpText.text = "Pick Up " + item.name;
        tmpText.gameObject.SetActive(true);
    }


    void PickUpItem()
    {

        bool wasPickedUp = Inventory.instance.Add(item);
        if (wasPickedUp){
            ButtonScript.receivedInteractInput = false;
            Destroy(gameObject);
            tmpText.gameObject.SetActive(false);
        }
    }

}
