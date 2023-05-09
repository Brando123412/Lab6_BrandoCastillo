using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GuiManager : MonoBehaviour
{
    public static GuiManager instance {get; private set;}/*Este SINGLETONS nos ayuda a actualizar la variable de
    Score cada vez que se elimine un enemigo*/
    [SerializeField] private TMP_Text scoreText;//Texto Score
    private int scoreTotal = 0;//Score inicial
    /*Pegunta si hay algun Score o instancia en el juego si lo hay lo elimina y toma su propia instancia*/
    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }

        instance = this;
    }
    //Esto va a actualizar el score cada vez que lo llamen y recibira un parametro para sumar al score actual
    public void UpdateText(int pointsGained){
        scoreTotal += pointsGained;
        scoreText.text = string.Format("Score: {0} (+ {1})", scoreTotal, pointsGained);
    }
}
