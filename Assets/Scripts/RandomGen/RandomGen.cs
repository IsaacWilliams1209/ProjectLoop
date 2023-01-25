using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGen
{
    // Exclusive
    int roomMin = 8;
    int minRoomSize = 6;
    // Inclusive
    int roomMax = 12;
    int maxRoomSize = 20;

    float timer;
    struct Room
    {
        // Width and height are both in half measures
        public float height;
        public float width;
        public Vector3 centerPos;
    }


    List<Room> rooms = new List<Room>();


    bool CheckPlacement(Room room1, Room room2)
    {
        if (Mathf.Abs(room1.centerPos.x - room2.centerPos.x) < room1.width/2 + room2.width/2)
            return false;

        if (Mathf.Abs(room1.centerPos.z - room2.centerPos.z) < room1.height/2 + room2.height/2)
            return false;

        return true;
    }

    bool IsValidPlacement(Room newRoom)
    {
        foreach(Room room in rooms)
        {
            if (!CheckPlacement(newRoom, room))
            {
                
                return false;
            }
        }

        return true;
    }

    public void GenerateRooms()
    {
        int noRooms = Random.Range(roomMin, roomMax);
        while(rooms.Count < noRooms)
        {
            timer += 0.01f;
            Room tempRoom = new Room();
            tempRoom.centerPos.x = Random.Range(-40, 40);
            tempRoom.centerPos.z = Random.Range(-40, 40);
            tempRoom.width = Random.Range(minRoomSize, maxRoomSize);
            tempRoom.height = Random.Range(minRoomSize, maxRoomSize);
            if (IsValidPlacement(tempRoom) || rooms.Count == 0)
            {
                rooms.Add(tempRoom);
                
            }
            if (timer > 50000)
            {
                Debug.Log("Break");
                break;
            }
        }
        foreach (Room room in rooms)
        {
            GameObject layout = GameObject.CreatePrimitive(PrimitiveType.Cube);
            layout.transform.position = room.centerPos;
            layout.transform.localScale = new Vector3(room.width, 1, room.height);
        }
        Debug.Log("Generation complete");
    }
}
