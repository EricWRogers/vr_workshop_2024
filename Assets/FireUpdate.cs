using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireUpdate : MonoBehaviour
{
   private Vector3 lasPos;
   private float magnatude;

   public List<ParticleSystem> fire;
    void Start()
    {
        
    }

    void FixedUpdate()
    {  
        magnatude = (transform.position - lasPos).magnitude;
        if (magnatude > 0){
            foreach (ParticleSystem _fire in fire){
                var emission = _fire.emission;
                emission.rateOverTime = magnatude * 50;
            }
        }
        else {
            foreach (ParticleSystem _fire in fire){
                var emission = _fire.emission;
                emission.rateOverTime = 10;
            }
        }

    
    }

}