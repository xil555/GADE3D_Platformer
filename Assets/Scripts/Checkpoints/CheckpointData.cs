using UnityEngine;

[System.Serializable]
public class CheckpointData
{
    public Vector3 position;
    public int lives;

    public CheckpointData(Vector3 pos, int lives)
    {
        this.position = pos;
        this.lives = lives;
    }
}
