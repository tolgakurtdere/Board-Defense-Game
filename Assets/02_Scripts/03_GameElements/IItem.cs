using UnityEngine;

public interface IItem
{
    void Activate();
    void Deactivate();
    void SetPosition(Vector3 position);
}