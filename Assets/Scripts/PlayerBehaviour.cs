using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    private GameStateController gameController;
    

    private SimpleController controls;    

    // Start is called before the first frame update
    void Start()
    {

        gameController = GameObject.FindWithTag("GameController").GetComponent<GameStateController>();

        controls = GetComponent<SimpleController>();


    }


    


    // Update is called once per frame
    void Update()
    {
        
        
    }

  


    IEnumerator Invincibility(float duration)
    {
        gameController.MakeInvincible();

        while (duration > 0)
        {
            duration -= Time.deltaTime;

            yield return null;
        }

        gameController.MakeVulnerable();
    }


    public void SpeedUp(float value)
    {
        controls.m_MovePower *= value;
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Floor")
        {            

            GameStateController.lives--;

            StartCoroutine("Invincibility", 2f);
        }        
        
        
        /*if (collision.gameObject.GetComponent<BasicCollectible>())
        {
            myPoints += collision.gameObject.GetComponent<BasicCollectible>().points;
            
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.GetComponent<SpeedPowerUp>())
        {
            controls.m_MovePower *= collision.gameObject.GetComponent<SpeedPowerUp>().speedModifier;

            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "MovementModifier")
        {
            controls.invertMovement = true;

            Destroy(collision.gameObject);
        }

        else
        {            
            lives--;
            if (collision.gameObject.tag != "Environment")
            {
                Destroy(collision.gameObject);
            }
        }*/
    }

    
}
