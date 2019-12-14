using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockController : MonoBehaviour
{ 
    public int SeperationDistance = 3;
    public float Speed;
    public int Neighbors = 0;
    public Vector3 Vel;
    private float velCounter = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 alignment = Alignment(); 
       
        if (Vector3.Distance(transform.position, GameAssets.I.player.transform.position) > 4)
        {
           
            Vector3 dir = GetDirectionToPlayer();
            transform.position = Vector3.MoveTowards(new Vector3(transform.position.x + alignment.x, 0, transform.position.z + alignment.z), GameAssets.I.player.transform.position, Time.deltaTime * Speed);
            transform.LookAt(GameAssets.I.player.transform);
        }

        velCounter += Time.deltaTime;
 
    } 
    private Vector3 GetDirectionToPlayer()
    {
        Vector2 Direction = GameAssets.I.player.transform.position - transform.position;
        Direction.Normalize();
        return Direction;
    }
    public Vector3 Alignment()
    {
        FlockController[] agents = GameObject.FindObjectsOfType<FlockController>();
        foreach (FlockController agent in agents)
        {
            if (agent != this)
            {
                if (Vector3.Distance(transform.position, agent.transform.position) <= 3)
                {
                    Vel.x += agent.transform.position.x - transform.position.x;
                    Vel.z += agent.transform.position.z - transform.position.z;
                    Neighbors++;
                }

            }
        }

        if (Neighbors == 0)
            return Vel;
        Vel.x /= Neighbors;
        Vel.z /= Neighbors;
        Vector3.Normalize(Vel);
        return Vel;
    }



    

}
