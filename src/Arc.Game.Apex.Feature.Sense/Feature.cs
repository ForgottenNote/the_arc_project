using Arc.Core.Models;
using Arc.Game.Apex.Core;
using Arc.Game.Apex.Core.Interfaces;

namespace Arc.Game.Apex.Feature.Sense
{
    public class Feature : IFeature
    {
        private readonly Config _config;

        #region Constructors

        public Feature(Config config)
        {
            _config = config;
        }

        #endregion

        #region Implementation of IFeature

        public void Tick(DateTime frameTime, State state)
        {
            if (state.Players.TryGetValue(state.LocalPlayer, out var localPlayer))
            {
                foreach (var (_, player) in state.Players)
                {
                    if(player.TeamNum == localPlayer.TeamNum)
                        continue;

                    if(!player.IsValid())
                        continue;

                    // Check if the player is closer than the config's distance value
                    if (localPlayer.LocalOrigin.Distance2(player.LocalOrigin) * Constants.UnitToMeter < _config.Distance)
                    {
                        if(!(player.GlowColorR >= 0 && player.GlowColorR <= 255)) {
                            player.GlowEnable = (byte)(player.Visible ? 5 : 7);
                            player.GlowThroughWalls = (byte)(player.Visible ? 1 : 2);
                            continue;
                        }
                      
                    
                        // If so enable the glow
                        player.GlowEnable = (byte)(1);
                        player.GlowThroughWalls = (byte)(player.Visible ? 1 : 2);
                        
                        if(_config.HealthBasedGlow == true) {
                            double multiplier = 255.0 / (100 + player.MaxShields);
                            Vector color = new Vector(
                                (float)((255.0 - multiplier * (player.Health + player.Shields)) / 255.0),
                                (float)((multiplier * (player.Health + player.Shields)) / 255.0),
                                (float)(0)
                            );
                            player.GlowColor = color;
                            //player.GlowColorR = (float)((255.0 - multiplier * (player.Health + player.Shields)) / 255.0);
                            //player.GlowColorG = (float)((multiplier * (player.Health + player.Shields)) / 255.0);
                            //player.GlowColorB = (float)(0);
                        } else {
                            player.GlowColorR = (float)(player.Visible ? _config.GlowColorRVisible : _config.GlowColorRHidden);
                            player.GlowColorG = (float)(player.Visible ? _config.GlowColorGVisible : _config.GlowColorGHidden);
                            player.GlowColorB = (float)(player.Visible ? _config.GlowColorBVisible : _config.GlowColorBHidden);
                        }
                    }
                    // Turn off the glow when the player has glow on and is farther away than the config's distance value
                    else if (player.GlowEnable is 1)
                    {
                        // Default values for GlowEnable and GlowThroughWalls
                        player.GlowEnable = 2;
                        player.GlowThroughWalls = 5;
                    }
                }
            }
        }

        #endregion
    }
}