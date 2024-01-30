using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuddleQTE : MonoBehaviour
{
    public int requiredClicks = 8;
    public float qteDuration = 3.0f;

    private int currentClicks = 0;
    private bool qteActive = false;
    public GameObject puddle;
    public GameObject qteObject;
    Vector3 puddleRotation = new Vector3(0, 0, 0);
    public bool isTriggered = false;
    public GameObject mark;
    Vector3 markRotation = new Vector3(0, 90, 0);
    public float maxDistance = 50.0f; // Maximum distance for starting QTE

    // Reference to the player script
    public MyCharacter playerScript;

    void Start()
    {
        playerScript = MyCharacter.Instance;
        if (playerScript == null)
        {
            Debug.LogError("MyCharacter脚本实例未找到！");
        }
        StartCoroutine(StartRandomQTE());
    }

    IEnumerator StartRandomQTE()
    {
        while (isTriggered == false)
        {
            yield return new WaitForSeconds(Random.Range(3.0f, 15.0f));

            // Check the distance between player and QTE object
            float distance = Vector3.Distance(qteObject.transform.position, playerScript.transform.position);

            if (distance <= maxDistance)
            {
                qteActive = true;
                Debug.Log("puddleQTEBegin!!");

                Vector3 qtePosition = qteObject.transform.position;
                Vector3 newMarkPosition = new Vector3(qtePosition.x - 1.5f, qtePosition.y + 5.4f, qtePosition.z + 1);
                GameObject markInstance = Instantiate(mark, newMarkPosition, Quaternion.Euler(markRotation));

                yield return new WaitForSeconds(qteDuration);

                if (currentClicks < requiredClicks)
                {
                    PunishmentEvent();
                }
                else
                {
                    Destroy(markInstance);
                    qteActive = false;
                    Debug.Log("puddleQTESuccess!!");
                    isTriggered = true;
                }
            }
            else
            {
                Debug.Log("Player is too far for QTE!");
            }
        }
    }

    void Update()
    {
        if (qteActive && Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("QTEObject"))
            {
                currentClicks++;
            }
        }
    }

    void PunishmentEvent()
    {
        Vector3 playerPosition = qteObject.transform.position;
        Vector3 newPuddlePosition = new Vector3(playerPosition.x + 19.5f, playerPosition.y + 0.4f, playerPosition.z);
        Instantiate(puddle, newPuddlePosition, Quaternion.Euler(puddleRotation));
        isTriggered = true;
    }
}
