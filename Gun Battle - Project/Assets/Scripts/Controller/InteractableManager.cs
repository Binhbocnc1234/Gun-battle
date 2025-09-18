using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    public List<Interactable> candidates = new List<Interactable>();
    void Start() {
        for (int i = 0; i < Player.container.Count; ++i) {
            candidates.Add(null);
        }
        foreach (Player player in Player.container) {
            player.OnPlayerLoot += ApplyEffectFromCandidate;
        }
    }
    void Update()
    {
        foreach (Player player in Player.container) {
            ChooseCandidate(player);
        }
    }

    Interactable ChooseCandidate(Player player)
    {
        Interactable ans = null;
        float bestDistance = 100000f;
        foreach (Interactable inter in Interactable.container)
        {
            float dist = Vector2.Distance(inter.transform.position, player.transform.position);
            if (dist <= bestDistance && dist <= inter.maxDistance)
            {
                ans = inter;
                bestDistance = dist;
            }
        }
        ans?.TriggerEnter();
        foreach (Interactable inter in Interactable.container)
        {
            if (ans != inter)
            {
                inter.TriggerExit();
            }
        }
        return ans;
    }
    void ApplyEffectFromCandidate(int playerIndex)
    {
        candidates[playerIndex]?.Interact(Player.container[playerIndex]);
    }
}
