using UnityEngine;

public interface IInteractable
{
    string interactTitle { get; }

    void OnFocus();

    void OnLoseFocus();

    void Interact(Player player);
}


