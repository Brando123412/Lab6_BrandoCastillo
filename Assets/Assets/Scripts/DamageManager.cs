using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DamageManager : MonoBehaviour
{
    public static DamageManager instance {get; private set;}/*Este SINGLETONS nos ayuda ha hacer el calculo
    del daño segun quien sea el objeto que haga daño*/
    //En el awake estamos diciendo que la instancia tiene que ser su propia instancia nueva 
    private void Awake() {
        if(instance != null && instance != this){
            Destroy(this.gameObject);
        }

        instance = this;
    }
    /*public void SubscribeToEvent(Action <int, HealthBarController> currentAction){
        currentAction += DamageCalculation;
    }*/
    //Este logica es para cuando el daño sea a travez de la bala
    public void SubscribeFunction(BulletController enemy){
        enemy.onCollision += DamageCalculation;
    } 
    //Este logica es para cuando el daño sea a travez del enemigo
    public void SubscribeFunction(EnemySimpleController enemy){
        enemy.onCollision += DamageCalculation;
    }
    //Para cacular daño
    private void DamageCalculation(int damageTaken, HealthBarController healthBarController){
        healthBarController.UpdateHealth(-damageTaken);
    }
}
