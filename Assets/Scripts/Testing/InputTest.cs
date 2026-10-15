using UnityEngine;

public static class InputTest
{
    public static void OnGUI(GameView view)
    {
        if (view.inputTest.input.text.ToString().Length == view.inputTest.lettersCount)
            return;

        int newLength = view.inputTest.input.text.ToString().Length;

        Transform spriteTransform = view.inputTest.sprite.transform;

        Vector3 targetScale = new Vector3(
            spriteTransform.localScale.x,
            .2f + newLength * .1f,
            spriteTransform.localScale.z
        );

        spriteTransform.localScale = Vector3.MoveTowards(
            spriteTransform.localScale,
            targetScale,
            .1f * Time.deltaTime
        );

        if (targetScale == spriteTransform.localScale)
        {
            view.inputTest.lettersCount = newLength;
        }
    }
}
