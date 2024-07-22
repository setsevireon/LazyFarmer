using Microsoft.Xna.Framework;
using StardewValley;

namespace LazyFarmer;

internal class Mining
{
    public void UsePickaxe(GameLocation location, Vector2 tile, Farmer who)
    {
        location.explode(tile, 12, who, false);
    }
}

