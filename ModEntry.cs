using StardewModdingAPI;
using StardewModdingAPI.Events;

namespace LazyFarmer;

internal sealed class ModEntry : Mod
{
    private Fishing? fishing;

    /*********
    ** Public methods
    *********/
    public override void Entry(IModHelper helper)
    {
        fishing = new();

        helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
        helper.Events.Display.MenuChanged += this.OnMenuChanged;
    }


    /*********
    ** Private methods
    *********/
    private void OnMenuChanged(object? sender, MenuChangedEventArgs args)
    {
        fishing?.OnMenuChanged(sender, args);
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs args)
    {
        fishing?.OnUpdateTicked(sender, args);
    }
}
