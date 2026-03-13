using UnityEngine;

[CreateAssetMenu(fileName = "DialogueSO", menuName = "Scriptable Objects/DialogueSO")]
public class DialogueSO : ScriptableObject
{
    public string characterName;
    public Sprite characterIcon;

    [TextArea(3, 5)]
    public string message;
}
