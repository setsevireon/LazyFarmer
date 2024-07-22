using StardewValley.Menus;

namespace LazyFarmer;

internal class Fishing
{
    private float prevBobberPosition;
    private float prevDistanceFromCatching;
    private float progressBarDecreaseMultiplier;
    private float progressBarIncreaseMultiplier;

    public void BobberBarMenu(BobberBar bar)
    {
        prevBobberPosition = 0f;
        prevDistanceFromCatching = 0f;
        progressBarDecreaseMultiplier = 0.3f;
        progressBarIncreaseMultiplier = 3f;

        if (bar.distanceFromCatching != 0.1f)
            bar.distanceFromCatching = 0.3f;
    }

    public void Update(BobberBar bar)
    {
        if (prevDistanceFromCatching != 0
            && bar.distanceFromCatching != 0
            && prevDistanceFromCatching != bar.distanceFromCatching)
        {
            if (prevDistanceFromCatching > bar.distanceFromCatching)
            {
                bar.distanceFromCatching = prevDistanceFromCatching
                  - ((prevDistanceFromCatching - bar.distanceFromCatching)
                      * progressBarDecreaseMultiplier);
            }
            else
                bar.distanceFromCatching = prevDistanceFromCatching
                  + ((bar.distanceFromCatching - prevDistanceFromCatching)
                      * progressBarIncreaseMultiplier);

            bar.distanceFromCatching = Math.Max(0f, Math.Min(1f, bar.distanceFromCatching));
        }

        prevBobberPosition = bar.bobberPosition;
        prevDistanceFromCatching = bar.distanceFromCatching;
    }
}
