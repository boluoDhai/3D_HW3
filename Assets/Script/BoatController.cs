using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController
{
    public static GameObject boat;
    private float speed = 2.0f;
    public static bool direction_left = true;
    public static bool if_move = false;
    public static bool left = true;
    public static int count = 0;

    public void initializeBoat() {
        Vector3 boat_vec;
        boat_vec.x = 2;
        boat_vec.y = 0.2f;
        boat_vec.z = 0;
        boat = GameObject.Instantiate(Resources.Load("Prefabs/Boat", typeof(GameObject)), boat_vec, Quaternion.identity, null) as GameObject;
        boat.name = "boat";
    }

    public void Move() {
        if (if_move) {
            if (direction_left) {
                if (boat.transform.position.x > -2) {
                    boat.transform.Translate(new Vector3(-1, 0, 0) * speed * Time.deltaTime, Space.World);
                } else {
                    if_move = direction_left = false;
                }
            } else {
                if(boat.transform.position.x < 2) {
                    boat.transform.Translate(new Vector3(1, 0, 0) * speed * Time.deltaTime, Space.World);
                } else {
                    if_move = false;
                    direction_left = true;
                }
            }
        }
    }

    public int getCount() {
        return count;
    }
}
