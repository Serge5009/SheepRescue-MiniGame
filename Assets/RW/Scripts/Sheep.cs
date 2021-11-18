using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sheep : MonoBehaviour
{
    public float runSpeed;
    public float gotHayDestroyDelay;
    private bool hitByHay;

    public float dropDestroyDelay;
    private Collider myCollider;
    private Rigidbody myRigidbody;

    private SheepSpawner sheepSpawner;

    public float heartOffset;
    public GameObject heartPrefab;

    // Start is called before the first frame update
    private void Start()
    {
        myCollider = GetComponent<Collider>();
        myRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Vector3.forward * runSpeed * Time.deltaTime);
    }

    private void HitByHay()
    {
        sheepSpawner.RemoveSheepFromList(gameObject);   //  Removing sheep from list in SheepSpawner
        SoundManager.Instance.PlaySheepHitClip();       //  Play sound
        Instantiate(heartPrefab, transform.position + new Vector3(0, heartOffset, 0), Quaternion.identity); //  Spawn flying heart

        GameStateManager.Instance.SavedSheep();
        hitByHay = true;
        runSpeed = 0;

        TweenScale tweenScale = gameObject.AddComponent<TweenScale>(); ;  //  Shrinking effect for ship
        tweenScale.targetScale = 0;
        tweenScale.timeToReachTarget = gotHayDestroyDelay;
        tweenScale.timeToDestroy = gotHayDestroyDelay;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Hay") && !hitByHay)
        {
            Destroy(other.gameObject);
            HitByHay();
        }
        else if (other.CompareTag("DropSheep"))
        {
            Drop();
        }
    }

    private void Drop()
    {
        GameStateManager.Instance.DroppedSheep();

        sheepSpawner.RemoveSheepFromList(gameObject);   //  Removing sheep from list in SheepSpawner
        SoundManager.Instance.PlaySheepDroppedClip();   //  Play sound
        myRigidbody.isKinematic = false;
        myCollider.enabled = false;
        Destroy(gameObject, dropDestroyDelay);
    }

    public void SetSpawner(SheepSpawner spawner)
    {
        sheepSpawner = spawner;
    }
}