using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{

    public Transform roadContainer;
    public Transform objectContainer;
    public Transform houseContainer;
    public Transform player;

    [Header("Road Prefab")]
    public GameObject road;

    [Header("Objects Prefab")]
    public GameObject[] objects;

    [Header("Track Prefab")]
    public GameObject track;

    [Header("House Prefab")]
    public GameObject[] houses;

    [Header("Parameters")]
    public float generateDistance = 1000;
    public float unitSize = 30;

    private float roadPos = 0;
    private float trackPos = 0;
    private float leftSideRoadPos = 0;
    private float rightSideRoadPos = 0;

    private float roadLength;
    private float trackLength;
    private float roadWidth;
    private float[] houseWidth;
    private float[] houseLength;

    private float[] lanePos;

    private List<List<int[]>> map;

    // Use this for initialization
    void Start()
    {
        roadLength = road.GetComponent<MeshRenderer>().bounds.size.z * road.transform.localScale.z;
        roadWidth = road.GetComponent<MeshRenderer>().bounds.size.x * road.transform.localScale.x;
        trackLength = track.GetComponent<MeshRenderer>().bounds.size.z * track.transform.localScale.z;

        houseWidth = new float[houses.Length];
        houseLength = new float[houses.Length];

        for (int i = 0; i < houses.Length; i++)
        {
            houseWidth[i] = houses[i].GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.x;
            houseLength[i] = houses[i].GetComponentInChildren<SkinnedMeshRenderer>().bounds.size.z;
        }

        lanePos = new float[3];
        map = LoadMapText();
    }

    // Update is called once per frame
    void Update()
    {
        AddRoad();
        AddTrack();
        AddLeftHouse();
        AddRightHouse();
        AddObjects();
    }

    private void AddObjects()
    {
        while (lanePos[0] < roadPos)
        {
            int idx = UnityEngine.Random.Range(0, map.Count);
            for (int i = 0; i < map[idx].Count; i++)
            {
                for (int j = 0; j < map[idx][i].Length; j++)
                {
                    InstantiateObject(map[idx][i][j], i);
                }
            }
            lanePos[0] = lanePos[1] = lanePos[2] = Mathf.Max(lanePos);
        }
    }

    private void InstantiateObject(int param, int lane)
    {
        if (param == -1)
        {
            lanePos[lane] += unitSize;
            return;
        }

        int objIndex = param / 10;
        int objParam = param % 10;

        GameObject obj = Instantiate(objects[objIndex]);
        float objLength;
        if (obj.GetComponent<MeshRenderer>() != null)
        {
            objLength = obj.GetComponent<MeshRenderer>().bounds.size.z * obj.transform.localScale.z;
        }
        else
        {
            objLength = obj.GetComponentInChildren<MeshRenderer>().bounds.size.z * obj.transform.GetChild(0).localScale.z;
        }

        Vector3 pos = obj.transform.position;
        pos.x = -14 + lane * 14;
        pos.z = lanePos[lane] + objLength / 2;
        obj.transform.position = pos;

        lanePos[lane] += objLength;

        if (objIndex == 0)
        {
            int carriage = objParam / 2 + 1;
            bool isRun = objParam % 2 == 1;
            obj.GetComponent<SubwayControl>().isRun = isRun;
            obj.GetComponent<SubwayControl>().carriage = carriage;
            obj.GetComponent<SubwayControl>().CreateCarriage();
        }
        obj.transform.parent = objectContainer;
    }

    private void AddLeftHouse()
    {
        while (leftSideRoadPos < roadPos)
        {
            int idx = UnityEngine.Random.Range(0, houses.Length);
            GameObject house = Instantiate(houses[idx]);
            Vector3 pos = house.transform.position;
            pos.x = -(roadWidth / 2 + houseWidth[idx] / 2);
            pos.z = leftSideRoadPos + houseLength[idx] / 2;
            Vector3 scale = house.transform.localScale;
            scale.x *= -1;
            house.transform.localScale = scale;
            leftSideRoadPos += houseLength[idx];
            house.transform.position = pos;
            house.transform.parent = houseContainer;
        }
    }

    private void AddRightHouse()
    {
        while (rightSideRoadPos < roadPos)
        {
            int idx = UnityEngine.Random.Range(0, houses.Length);
            GameObject house = Instantiate(houses[idx]);
            Vector3 pos = house.transform.position;
            pos.x = roadWidth / 2 + houseWidth[idx] / 2;
            pos.z = rightSideRoadPos + houseLength[idx] / 2;
            rightSideRoadPos += houseLength[idx];
            house.transform.position = pos;
            house.transform.parent = houseContainer;
        }
    }

    private void AddTrack()
    {
        while (trackPos < roadPos)
        {
            GameObject trackCopy1 = Instantiate(track) as GameObject;
            GameObject trackCopy2 = Instantiate(track) as GameObject;
            GameObject trackCopy3 = Instantiate(track) as GameObject;

            Vector3 pos1 = track.transform.position;
            pos1.z = trackPos;
            pos1.x = -14;
            trackCopy1.transform.position = pos1;

            Vector3 pos2 = track.transform.position;
            pos2.z = trackPos;
            trackCopy2.transform.position = pos2;

            Vector3 pos3 = track.transform.position;
            pos3.z = trackPos;
            pos3.x = 14;
            trackCopy3.transform.position = pos3;

            trackPos += trackLength;
            trackCopy1.transform.parent = roadContainer;
            trackCopy2.transform.parent = roadContainer;
            trackCopy3.transform.parent = roadContainer;
        }
    }

    private void AddRoad()
    {
        while (roadPos - player.position.z < generateDistance)
        {
            GameObject roadCopy = Instantiate(road) as GameObject;
            Vector3 pos = roadCopy.transform.position;
            pos.z = roadPos;
            roadCopy.transform.position = pos;
            roadPos += roadLength;
            roadCopy.transform.parent = roadContainer;
        }
    }

    private List<List<int[]>> LoadMapText()
    {
        TextAsset bindData = Resources.Load("map") as TextAsset;
        string data = bindData.text.Replace(Environment.NewLine, string.Empty);
        data = data.Replace("-", "-1");

        string[] groups = data.Split('|');

        List<List<int[]>> map = new List<List<int[]>>();

        for (int i = 0; i < groups.Length; i++)
        {
            string[] line = groups[i].Split(',');
            map.Add(new List<int[]>());
            for (int j = 0; j < line.Length; j++)
            {
                map[i].Add(Array.ConvertAll(line[j].Split(' '), int.Parse));
            }
        }

        return map;
    }
}
