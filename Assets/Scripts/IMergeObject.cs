using UnityEngine;

namespace DefaultNamespace
{
    public interface IMergeObject
    {
        Transform InitialMergeTransform { get; }
        Vector3 MergePosition { get; }
    }
}