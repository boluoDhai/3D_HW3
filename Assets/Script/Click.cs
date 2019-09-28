using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Click : MonoBehaviour{

    // Start is called before the first frame update
    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        
    }

    void insCount(string str, int choice) {
        if (choice == 0) {
            if (str[0] == 'p') ++Coast.east_priest_count;
            else ++Coast.east_devil_count;
        } else {
            if (str[0] == 'p') ++Coast.west_priest_count;
            else ++Coast.west_devil_count;
        }
    }

    void decCount(string str, int choice) {
        if (choice == 0) {
            if (str[0] == 'p') --Coast.east_priest_count;
            else --Coast.east_devil_count;
        } else {
            if (str[0] == 'p') --Coast.west_priest_count;
            else --Coast.west_devil_count;
        }
    }

    void OnMouseDown() {
        //在boat上面
        if (this.gameObject.transform.position.y <= BoatController.boat.transform.position.y + 0.61f) {
            string str = this.gameObject.name;
            int pre_index = (str[0] == 'd' ? 3 : 0);
            int index = pre_index + str[str.Length - 1] - '0';
            if (this.gameObject.transform.position.x < BoatController.boat.transform.position.x)
                BoatController.left = true;
            if (BoatController.direction_left) {
                this.gameObject.transform.position = CreateGUI.vec[index];
                insCount(str, 0);
            } else {
                this.gameObject.transform.position = new Vector3(-CreateGUI.vec[index].x, CreateGUI.vec[index].y, CreateGUI.vec[index].z);
                insCount(str, 1);
            }
            --BoatController.count;
            this.gameObject.transform.parent = null;
        } else { // 不在boat上面
            if (BoatController.count < 2) {
                if (BoatController.left) { // 左边有空位
                    this.gameObject.transform.position = new Vector3(BoatController.boat.transform.position.x - 0.6f, BoatController.boat.transform.position.y + 0.6f, BoatController.boat.transform.position.z);
                    BoatController.left = false;
                } else {
                    this.gameObject.transform.position = new Vector3(BoatController.boat.transform.position.x + 0.6f, BoatController.boat.transform.position.y + 0.6f, BoatController.boat.transform.position.z);
                }
                ++BoatController.count;
                this.gameObject.transform.parent = BoatController.boat.transform;
                if (BoatController.direction_left) decCount(this.gameObject.name, 0);
                 else  decCount(this.gameObject.name, 1);
            }
        }
    }
}
