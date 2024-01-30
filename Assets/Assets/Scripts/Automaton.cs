using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Automaton : MonoBehaviour
{
    public GameObject WP1;
    public GameObject WP2;
    Vector3 _WP1;
    Vector3 _WP2;
    bool GoingToWP1 = true;
    // Start is called before the first frame update
    void OnEnable()
    {
        _WP1 = WP1.transform.position;
        _WP2 = WP2.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GoingToWP1)
        {
            Vector3 Dir = _WP1 - transform.position;
            Dir.y = 0.0f;

            if (Dir.magnitude<1.5f)
            {
                GoingToWP1 = false;
            }
            else
            {
                
                Dir.Normalize();

                transform.rotation = Quaternion.LookRotation(Dir, Vector3.up);
            }
        }
        else
        {
            Vector3 Dir = _WP2 - transform.position;
            Dir.y = 0.0f;

            if (Dir.magnitude < 1.5f)
            {
                GoingToWP1 = true;
            }
            else
            {
                Dir.Normalize();

                transform.rotation = Quaternion.LookRotation(Dir, Vector3.up);
            }
        }
        //Debug.DrawLine(_WP1, _WP1 + new Vector3(0.0f, 50.0f, 0.0f));
        //Debug.DrawLine(_WP2, _WP2 + new Vector3(0.0f, 50.0f, 0.0f));

        //Debug.Log("state: " + GoingToWP1);
    }

}
