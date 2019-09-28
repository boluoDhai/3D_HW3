using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine;

public class CreateGUI : MonoBehaviour
{
    private GameObject[] priests = new GameObject[3];
    private GameObject[] devils = new GameObject[3];
    public static Vector3[] vec = new Vector3[6];

    private float begin_len = 3.5f; // 东边海岸的object起始点
    private float space = 1.3f; // 每个物体的距离

    private GameObject east_coast;
    private GameObject west_coast;
    private GameObject water;

    private GUIStyle mstyle;

    private BoatController boat_controller = new BoatController();

    private float east_pos = 8.0f;
    private float west_pos = -8.0f;

    void initializePD() {
        for (int i = 0; i < 3; ++i) {
            vec[i].x = begin_len + i * space;
            vec[i].y = 1;
            vec[i].z = 0;
        }
        for (int i = 3; i < 6; ++i) {
            vec[i].x = begin_len + 2 * space + (i - 2) * space;
            vec[i].y = 1;
            vec[i].z = 0;
        }
        for (int i = 0; i < 3; ++i) {
            priests[i] = GameObject.Instantiate(Resources.Load("Prefabs/Priest", typeof(GameObject)), vec[i], Quaternion.identity, null) as GameObject;
            priests[i].name = "priest" + i.ToString();
            priests[i].AddComponent<Click>();
            devils[i] = GameObject.Instantiate(Resources.Load("Prefabs/Devil", typeof(GameObject)), vec[i + 3], Quaternion.identity, null) as GameObject;
            devils[i].name = "devils" + i.ToString();
            devils[i].AddComponent<Click>();
        }
    }

    void initializeCoast() {
        Vector3 east_vec, west_vec;
        east_vec.x = west_pos;
        east_vec.z = west_vec.z = 0;
        west_vec.x = east_pos;
        east_vec.y = west_vec.y = 0;
        east_coast = GameObject.Instantiate(Resources.Load("Prefabs/Coast", typeof(GameObject)), east_vec, Quaternion.identity, null) as GameObject;
        west_coast = GameObject.Instantiate(Resources.Load("Prefabs/Coast", typeof(GameObject)), west_vec, Quaternion.identity, null) as GameObject;
    }

    void initializeWater() {
        water = GameObject.Instantiate(Resources.Load("Prefabs/Water", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
    }

    // Start is called before the first frame update
    void Start(){
        initializeCoast();
        initializeWater();
        boat_controller.initializeBoat();
        initializePD();
        mstyle = new GUIStyle();
        mstyle.fontSize = 40;
        mstyle.alignment = TextAnchor.MiddleCenter;
    }

    // Update is called once per frame
    void Update(){
        boat_controller.Move();
    }

    void OnGUI() {
        if(GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 200, 100, 100), "Go!")) {
            if(BoatController.count >= 1)
                BoatController.if_move = true;
        }
        if (BoatController.direction_left && judgeEnd() == 2)
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 200), "Failed!", mstyle);
        else if (!BoatController.direction_left && judgeEnd() == 1)
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 200), "Failed!", mstyle);
        else if(judgeEnd() == 0) {
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 200, 200, 200), "Success!", mstyle);
        }
    }

    public int judgeEnd() {
        if (Coast.east_priest_count != 0 && Coast.east_priest_count < Coast.east_devil_count)
            return 1; // 东边失败
        else if (Coast.west_priest_count != 0 && Coast.west_priest_count < Coast.west_devil_count)
            return 2; // 西边失败
        else if (Coast.west_priest_count == 3 && Coast.west_devil_count == 3)
            return 0; // 成功
        return 3; // 普通情况
    }
}
