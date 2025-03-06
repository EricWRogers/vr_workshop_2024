using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUpdate : MonoBehaviour
{
   private Vector3 lasPos;
   private float magnatude;

   public List<ParticleSystem> fire;

    void FixedUpdate()
    {  
        magnatude = (transform.position - lasPos).magnitude;
        Debug.Log(magnatude);
        if (magnatude > 0){
            foreach (ParticleSystem _fire in fire){
                var emission = _fire.emission;
                emission.rateOverTime = magnatude * 1000;
            }
            magnatude = 0;
        }
        else {
            foreach (ParticleSystem _fire in fire){
                var emission = _fire.emission;
                emission.rateOverTime = 10;
            }
        }
        lasPos = transform.position;
    
    }

}