using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePopup: MonoBehaviour {

[SerializeField] float textRisingUpSpeed = 0f;
[SerializeField] float disapperarTimer = 0f;
TextMeshPro damageText;
private Color textColor;

private void Start() {
    damageText = GetComponent<TextMeshPro>();
    textColor = damageText.color;
}
private void Update() {
    transform.position += new Vector3(0,textRisingUpSpeed) * Time.deltaTime;
    disapperarTimer -= Time.deltaTime;
    if(disapperarTimer <0 ){
        //Start disappearing
        float disappearSpeed = 3f;
        textColor.a -= disappearSpeed * Time.deltaTime;
        damageText.color = textColor;
        if(textColor.a < 0){
            Destroy(gameObject);
        }
    }
}

}