using JetBrains.Annotations;
using UnityEngine;

[CreateAssetMenu(fileName = "TeethObjects", menuName = "Scriptable Objects/TeethObjects")]
public class TeethObjects : ScriptableObject
{
    public Teeth chosenTeethSet;
    public int level = 1;
}
