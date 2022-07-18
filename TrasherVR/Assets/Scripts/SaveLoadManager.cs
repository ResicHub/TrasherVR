using UnityEngine;

public static class SaveLoadManager
{
    private static int level;
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

    private static Vector3 floorPosition;
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

    private static Vector3 playerPosition;
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

    private static int caughtCount;
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

    private static int missedCount;
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
