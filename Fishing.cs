using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.Menus;
using StardewValley.Tools;

namespace LazyFarmer;

internal class Fishing
{
    private float prevBobberPosition;
    private float prevDistanceFromCatching;
    private float progressBarDecreaseMultiplier;
    private float progressBarIncreaseMultiplier;

    public void OnMenuChanged(object? sender, MenuChangedEventArgs args)
    {
        var player = Game1.player;
        if (player is not { IsLocalPlayer: true }) return;
        if (player.CurrentTool is not FishingRod rod) return;
        if (args.NewMenu is not BobberBar bar) return;

        prevBobberPosition = 0f;
        prevDistanceFromCatching = 0f;
        progressBarDecreaseMultiplier = 0.5f;
        progressBarIncreaseMultiplier = 2f;

        if (bar.distanceFromCatching != 0.1f)
            bar.distanceFromCatching = 0.3f;
    }

    public void OnUpdateTicked(object? sender, UpdateTickedEventArgs args)
    {
        var player = Game1.player;
        if (player is not { IsLocalPlayer: true }) return;
        if (player.CurrentTool is not FishingRod rod) return;
        if (Game1.activeClickableMenu is not BobberBar bar) return;

        if (prevDistanceFromCatching != 0 && bar.distanceFromCatching != 0 && prevDistanceFromCatching != bar.distanceFromCatching)
        {
            if (prevDistanceFromCatching > bar.distanceFromCatching)
                bar.distanceFromCatching = prevDistanceFromCatching - ((prevDistanceFromCatching - bar.distanceFromCatching) * progressBarDecreaseMultiplier);
            else
                bar.distanceFromCatching = prevDistanceFromCatching + ((bar.distanceFromCatching - prevDistanceFromCatching) * progressBarIncreaseMultiplier);

            bar.distanceFromCatching = Math.Max(0f, Math.Min(1f, bar.distanceFromCatching));
        }

        prevBobberPosition = bar.bobberPosition;
        prevDistanceFromCatching = bar.distanceFromCatching;
    }
}
