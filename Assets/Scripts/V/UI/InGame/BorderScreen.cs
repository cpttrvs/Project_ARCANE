using UnityEngine;
using System;
using System.Collections;

public class BorderScreen : MonoBehaviour {

    public float value = 0;
    public bool vertical = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        float x = 0;
        float y = 0;

        if (vertical)
        {
            x = collider.transform.position.x;
            y = value;
        }  
        else
        {
            y = collider.transform.position.y;
            x = value;
        }
            
        if(collider.gameObject.name == "Player1" || collider.gameObject.name == "Player2")
        {
            collider.transform.position = new Vector2(x, y);
        }
        else
        {
            Debug.Log(collider.gameObject.GetComponent<Spell>().GetType().Name);
            if(collider.gameObject.GetComponent<Spell>().GetType().Name == "Boomerang"
                || collider.gameObject.GetComponent<Spell>().spellName == "Defrag")
            {
                /*
                Boomerang b = collider.gameObject.GetComponent<Boomerang>();
                Vector2 orientation = b.getOrientation();
                Debug.Log(b.getOrientation());
                Vector2 newOrientation = new Vector2(0, 0);
                if (x < 0 || x > 0)
                {
                    newOrientation = new Vector2(-orientation.x, orientation.y);
                }
                else if (y < 0 || y > 0)
                {
                    newOrientation = new Vector2(orientation.x, -orientation.y);

                }
                b.setDirection(newOrientation);
                Debug.Log(b.getOrientation());
                */
            } else
            {
                Destroy(collider.gameObject);
            }
        }
            
    }
}
