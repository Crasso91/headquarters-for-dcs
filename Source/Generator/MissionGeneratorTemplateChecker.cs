using Headquarters4DCS.DefinitionLibrary;
using Headquarters4DCS.Template;
using System;
using System.Linq;

namespace Headquarters4DCS.Generator
{
    public sealed class MissionGeneratorTemplateChecker : IDisposable
    {
        private const int MAX_NUMBER_OF_OBJECTIVES = 10; // Must be AT MOST max number of entries in the F10 "Other" menu, else all objective submenus won't be displayed.
        private const int MAX_NUMBER_OF_SUPPORT = 10; // Must be AT MOST max number of entries in the F10 "Other" menu, else all objective submenus won't be displayed.

        public MissionGeneratorTemplateChecker() { }
        public void Dispose() { }

        public void CheckTemplate(MissionTemplate template)
        {
#if !DEBUG
            if (template.GetPlayerCount() < 1)
                throw new HQ4DCSException($"No player flight groups. A mission must have at least one player flight group.");
#endif

            CheckMissingDefinitions(template);
            CheckCoalitions(template);
            CheckTheaterNodes(template);

            //mission.Countries[(int)Coalition.Red].Except(mission.Countries[(int)Coalition.Blue])

            //// Copy blue and red coalitions' countries list
            //mission.Countries[(int)Coalition.Blue] = coalitions[(int)Coalition.Blue].Countries.ToArray();
            //if (mission.Countries[(int)Coalition.Blue].Length == 0)
            //    throw new Exception("Blue coalition has no countries.");

            //mission.Countries[(int)Coalition.Red] = coalitions[(int)Coalition.Red].Countries.ToArray();
            //if (mission.Countries[(int)Coalition.Red].Length == 0)
            //    throw new Exception("Red coalition has no countries.");

            //// Remove blue countries from red coalition to make sure no country belongs to both.
            //mission.Countries[(int)Coalition.Red] = mission.Countries[(int)Coalition.Red].Except(mission.Countries[(int)Coalition.Blue]).ToArray();
            //if (mission.Countries[(int)Coalition.Red].Length == 0)
            //    throw new Exception("Blue and red coalitions share the same countries.");

            //// TODO: check template nodes
            //foreach (HQTemplateNode n in template.Nodes.Values)
            //{
            //}
        }

        private void CheckTheaterNodes(MissionTemplate template)
        {
            int objectiveCount = 0;
            int supportCount = 0;

            //foreach (MissionTemplateLocation node in template.Locations.Values)
            //{
            //    if (node.Definition.LocationType == TheaterLocationType.Airbase)
            //    {
            //        if ((node.Coalition == CoalitionNeutral.Neutral) && (node.PlayerFlightGroups.Count > 0))
            //            throw new HQ4DCSException($"Neutral airbase {node.Definition.DisplayName.ToUpperInvariant()} has player flight groups. Player flight groups cannot take off from neutral airbases.");

            //        if ((node.Coalition != (CoalitionNeutral)template.Settings.ContextPlayerCoalition) && (node.PlayerFlightGroups.Count > 0))
            //            throw new HQ4DCSException($"Enemy airbase {node.Definition.DisplayName.ToUpperInvariant()} has player flight groups. Player flight groups cannot take off from enemy airbases.");
            //    }

            //    DefinitionFeature[] features = node.GetFeaturesDefinitions();
            //    objectiveCount += (from DefinitionFeature f in features where f.Category == FeatureCategory.Objective select f).Count();
            //    if ((from DefinitionFeature f in features where f.Category == FeatureCategory.Objective select f).Count() > 0)
            //        supportCount++;

            //    DefinitionFeature[] uniqueFeatures = (from DefinitionFeature f in features where f.FeatureFlags.Contains(FeatureFlag.UniqueInLocation) select f).ToArray();

            //    foreach (DefinitionFeature feature in uniqueFeatures)
            //    {
            //        if ((from DefinitionFeature f in uniqueFeatures where f.ID == feature.ID select f).Count() > 1)
            //            throw new HQ4DCSException($"Feature {feature.DisplayName.ToUpperInvariant()} should only appear once per location, but it appears multiple times in {node.Definition.DisplayName.ToUpperInvariant()}.");
            //    }
            //}

            if (objectiveCount > MAX_NUMBER_OF_OBJECTIVES)
                throw new HQ4DCSException($"Too many mission objectives: {objectiveCount}, maximum is {MAX_NUMBER_OF_OBJECTIVES}.");

            if (supportCount > MAX_NUMBER_OF_SUPPORT)
                throw new HQ4DCSException($"Too many locations with support features: {supportCount}, maximum is {MAX_NUMBER_OF_SUPPORT}.");
        }

        private void CheckCoalitions(MissionTemplate template)
        {
            DefinitionCoalition[] coals = new DefinitionCoalition[2];
            coals[(int)Coalition.Blue] = Library.Instance.GetDefinition<DefinitionCoalition>(template.ContextCoalitionBlue);
            coals[(int)Coalition.Red] = Library.Instance.GetDefinition<DefinitionCoalition>(template.ContextCoalitionRed);

            for (int i = 0; i < 2; i++)
            {
                if ((coals[i].MinMaxTimePeriod[0] > template.ContextTimePeriod) || (coals[i].MinMaxTimePeriod[1] < template.ContextTimePeriod))
                    throw new HQ4DCSException($"Coalition {coals[i].DisplayName} is not available in the {template.ContextTimePeriod.ToString().Replace("Decade", "")}s.");
            }

            // TODO
            // if (coals[1].Countries.Distinct(coals[0].Countries).Count() == 0)
            //        throw new HQ4DCSException($"Both coalitions have the exact same countries.");
        }

        /// <summary>
        /// Looks for missing definitions and throw an error if any missing definition is found.
        /// </summary>
        /// <param name="template">The mission template in which to search for missing definitions</param>
        private void CheckMissingDefinitions(MissionTemplate template)
        {
            if (!Library.Instance.DefinitionExists<DefinitionCoalition>(template.ContextCoalitionBlue))
                throw new HQ4DCSException($"Coalition \"{template.ContextCoalitionBlue}\" not found.");

            if (!Library.Instance.DefinitionExists<DefinitionCoalition>(template.ContextCoalitionRed))
                throw new HQ4DCSException($"Coalition \"{template.ContextCoalitionRed}\" not found.");

            if (!Library.Instance.DefinitionExists<DefinitionLanguage>(template.PreferencesLanguage))
                throw new HQ4DCSException($"Language \"{template.PreferencesLanguage}\" not found.");

            if (!Library.Instance.DefinitionExists<DefinitionTheater>(template.ContextTheater))
                throw new HQ4DCSException($"Theater \"{template.ContextTheater}\" not found.");
        }
    }
}
