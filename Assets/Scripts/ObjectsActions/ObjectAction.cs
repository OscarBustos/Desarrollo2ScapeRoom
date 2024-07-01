using UnityEngine;

public abstract class ObjectAction : MonoBehaviour, IObjectActions
{
    public abstract void Execute();
}