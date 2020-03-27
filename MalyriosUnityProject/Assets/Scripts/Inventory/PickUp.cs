using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{

    Transform player;
    float distance;
    [SerializeField]
    TextMeshProUGUI tmpText;
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
        if (distance < 0.3f)
        {

            //Pick item Up, if player interacts with it
            if (Player.interactInput)
            {
                PickUpItem();
            }
        }
        
    }

    void ShowPickUpDialog()
    {
        print("shouldShowDialog");
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

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            ShowPickUpDialog();
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player"))
            tmpText.gameObject.SetActive(false);
    }
}
