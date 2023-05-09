using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySimpleController : MonoBehaviour
{
    [SerializeField] private int damage;//Daño de enemigo
    [SerializeField] private int score = 50;//El puntaje que soltara al matarlo
    public event Action<int, HealthBarController> onCollision;/*Este evento se activara en el caso de que el objeto
    entre en collision, en este caso es el player*/
    private void Start() {
        //Aqui creo que se esta mandando para suscribirnos en el evento de Damagemanager
        DamageManager.instance.SubscribeFunction(this);
        //aqui se esta suscribiendo al metodo onDeath de codigo Healthbar
        GetComponent<HealthBarController>().onDeath += OnDeath;
    }

    private void OnDeath(){
        //En el caso de que muera el enemigo se esta llamando a la animacion
        GetComponent<AnimatorController>().SetDie();
        //Se actualiza el porcentaje de puntuacion
        GuiManager.instance.UpdateText(score);
        //Y se destruye el objeto durante un segundo despues 
        Destroy(this.gameObject, 1.5f);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        /*En aqui se esta diciendo que si chocamos con un objeto con el tag player
        y al mismo tiempo tenga el cogido HealthBar.. se le enviara la variable damage para que se actualice su barra
        de vida*/
        if(other.CompareTag("Player")){
            if(other.GetComponent<HealthBarController>()){
                onCollision?.Invoke(damage,other.GetComponent<HealthBarController>());
            }
        }
    }
}
