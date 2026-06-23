using UnityEngine;

public class MoveData
{
    public Transform StartPos;
    
    public Vector3 MovePower;
    
    public Vector3 EndPos;
    
    public float MovePoint;
    
    public MoveData(Transform startPos, Vector3 endPos, Vector3 movePower)
    {
        StartPos = startPos;
        MovePower = movePower;
        EndPos = endPos;
    }
    
    public MoveData(MoveData data)
    {
        MovePoint = data.MovePoint;
        StartPos = data.StartPos;
        MovePower = data.MovePower;
        EndPos = data.EndPos;

    }
}
