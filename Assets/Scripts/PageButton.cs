
using UdonSharp;
using UnityEngine;

[UdonBehaviourSyncMode(BehaviourSyncMode.None)]
public class PageButton : UdonSharpBehaviour
{
    [SerializeField]
    public bool Direction;

    public void OnButtonClicked()
    {
        var picker = GetComponentInParent<AvatarPicker>();
        picker.ChangePage(Direction);
    }
}
