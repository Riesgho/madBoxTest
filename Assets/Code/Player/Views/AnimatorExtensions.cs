using System;
using System.Collections;
using System.Linq;
using UniRx;
using UnityEngine;


public static class AnimatorExtensions
{
    private static IEnumerator WaitUntilAnimationIsPlayed(this Animator animator, int[] states, int layerIndex)
    {
        yield return null;

        while (
            animator != null
            && (!states.Contains(animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash) ||
                animator.IsInTransition(layerIndex)))
        {
            yield return null;
        }
    }

    public static IObservable<Unit> WaitUntilCurrentAnimationEndsAsObservable(this Animator animator,
        int layerIndex = 0, float normalizedTimeOffset = 0f)
    {
        return animator.WaitUntilCurrentAnimationEnds(layerIndex, normalizedTimeOffset).ToObservable();
    }

    private static IEnumerator WaitUntilAnimationEnds(this Animator animator, int state, int layerIndex,
        float normalizedTimeOffset)
    {
        yield return animator.WaitUntilAnimationEnds(new[] { state }, layerIndex, normalizedTimeOffset);
    }

    private static IEnumerator WaitUntilAnimationEnds(this Animator animator, int[] states, int layerIndex,
        float normalizedTimeOffset)
    {
        yield return animator.WaitUntilAnimationIsPlayed(states, layerIndex);

        while (
            animator != null
            && states.Contains(animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash)
            && !animator.IsInTransition(layerIndex)
            && animator.GetCurrentAnimatorStateInfo(layerIndex).normalizedTime <= (0.95f - normalizedTimeOffset))
        {
            yield return null;
        }
    }

    private static IEnumerator WaitUntilCurrentAnimationEnds(this Animator animator, int layerIndex = 0,
        float normalizedTimeOffset = 0f)
    {
        yield return null;
        yield return animator.WaitUntilAnimationEnds(animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash,
            layerIndex, normalizedTimeOffset);
    }
}