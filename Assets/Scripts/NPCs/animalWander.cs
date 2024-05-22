using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalWander : MonoBehaviour
{
    public float speed = 1.25f;
    public float range = 1.0f;
    public float maxDistance = 2.0f;
    Vector2 waypoint;
    // Start is called before the first frame update
    void Start()
    {
        waypoint = new Vector2(transform.position.x, transform.position.y);
        wanderAnimal();
    }

    // Update is called once per frame
    void Update(){
        if (Vector2.Distance(transform.position, waypoint) < range){
            wanderAnimal();
        }
        transform.position = Vector2.MoveTowards(transform.position, waypoint, speed * Time.deltaTime);
    }
    void wanderAnimal(){
        waypoint = new Vector2(transform.position.x + Random.Range(-maxDistance, maxDistance), transform.position.y + Random.Range(-maxDistance, maxDistance));
    }
}
