using Microsoft.Xna.Framework.Graphics;
using StardewModdingAPI;
using StardewModdingAPI.Events;
using StardewValley;
using StardewValley.GameData.Objects;
using StardewValley.GameData.Tools;
using StardewValley.GameData.Buffs;
using StardewValley.Menus;
using StardewValley.Tools;

namespace LazyFarmer;

internal sealed class ModEntry : Mod
{
    Fishing fishing = new Fishing();
    Mining mining = new Mining();

    /*********
    ** Public methods
    *********/
    public override void Entry(IModHelper helper)
    {
        helper.Events.Content.AssetRequested += this.OnAssetRequested;
        helper.Events.Display.MenuChanged += this.OnMenuChanged;
        helper.Events.GameLoop.UpdateTicked += this.OnUpdateTicked;
    }


    /*********
    ** Private methods
    *********/
    private void OnAssetRequested(object? sender, AssetRequestedEventArgs e)
    {
        if (e.NameWithoutLocale.IsEquivalentTo("Data/Objects"))
        {
            e.Edit(asset =>
            {
                var data = this.Helper.ModContent.Load<IDictionary<string, ObjectData>>("assets/objects.json");
                var editor = asset.AsDictionary<string, ObjectData>().Data;

                foreach (var pair in data)
                {
                    editor.Add(pair.Key, pair.Value);
                }
            });
        }

        if (e.NameWithoutLocale.IsEquivalentTo("Data/Tools"))
        {
            e.Edit(asset =>
            {
                var data = this.Helper.ModContent.Load<IDictionary<string, ToolData>>("assets/tools.json");
                var editor = asset.AsDictionary<string, ToolData>().Data;

                foreach (var pair in data)
                {
                    editor.Add(pair.Key, pair.Value);
                }
            });
        }

        if (e.NameWithoutLocale.IsEquivalentTo("Data/Buffs"))
        {
            e.Edit(asset =>
            {
                var data = this.Helper.ModContent.Load<IDictionary<string, BuffData>>("assets/buffs.json");
                var editor = asset.AsDictionary<string, BuffData>().Data;

                foreach (var pair in data)
                {
                    editor.Add(pair.Key, pair.Value);
                }
            });
        }

        if (e.NameWithoutLocale.IsEquivalentTo("Data/CookingRecipes"))
        {
            e.Edit(asset =>
            {
                var editor = asset.AsDictionary<string, string>().Data;
                editor["Lazy Drink"] = "LazyEssence 3/10 10/LazyDrink/default/";
            });
        }

        if (e.NameWithoutLocale.IsEquivalentTo("TileSheets/LazySprites"))
        {
            e.LoadFromModFile<Texture2D>("assets/sprites.png", AssetLoadPriority.Medium);
        }
    }

    private void OnMenuChanged(object? sender, MenuChangedEventArgs e)
    {
        var who = Game1.player;

        if (who.IsLocalPlayer
            && who.CurrentTool is FishingRod
            && who.CurrentTool.Name.Contains("Lazy")
            && e.NewMenu is BobberBar bar)
        {
            fishing.BobberBarMenu(bar);
        }
    }

    private void OnUpdateTicked(object? sender, UpdateTickedEventArgs e)
    {
        var who = Game1.player;

        if (who.IsLocalPlayer
            && who.CurrentTool is FishingRod
            && who.CurrentTool.Name.Contains("Lazy")
            && Game1.activeClickableMenu is BobberBar bar)
        {
            fishing.Update(bar);
        }

        if (who.IsLocalPlayer
            && who.CurrentTool is Pickaxe
            && who.CurrentTool.Name.Contains("Lazy")
            && who.UsingTool)
        {
            mining.UsePickaxe(Game1.currentLocation, who.Tile, who);
        }

    }
}
