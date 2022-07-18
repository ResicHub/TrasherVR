using UnityEngine;

public static class SaveLoadManager
{
    private static int level = 1;
    public static int Level
    {
        get
        {
            return level;
        }
        set
        {
            level = value;
        }
    }

    private static Vector3 floorPosition = new Vector3(0, 0, -0.5f);
    public static Vector3 FloorPosition
    {
        get
        {
            return floorPosition;
        }
        set
        {
            floorPosition = value;
        }
    }

    private static Vector3 playerPosition = new Vector3(0, 0.85f, -1.4f);
    public static Vector3 PlayerPosition
    {
        get
        {
            return playerPosition;
        }
        set
        {
            playerPosition = value;
        }
    }

    private static int caughtCount = 0;
    public static int CaughtCount
    {
        get
        {
            return caughtCount;
        }
        set
        {
            caughtCount = value;
        }
    }

    private static int missedCount = 0;
    public static int MissedCount
    {
        get
        {
            return missedCount;
        }
        set
        {
            missedCount = value;
        }
    }
}
