-- ===============================================
-- COMMON HQ4DCS STUFF
-- ===============================================

hq = { }

-- ===============================================
-- MISSION SCRIPT
-- ===============================================

-- Create common "other" menu
hq.f10Menu = { }
missionCommands.addCommandForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.MissionStatus£", nil, hq.f10MissionStatus)
hq.f10Menu.FlightPlan=missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.FlightPlan£", nil))
hq.f10Menu.FlightPlanItems = { }
hq.f10Menu.Objective=missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.Objectives£", nil))
hq.f10Menu.ObjectiveItems = { }
hq.f10Menu.Support=missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.Support£", nil))
hq.f10Menu.SupportItems = { }

$SCRIPTGLOBAL$

-- TODO: remove unused F10 menus if no subitems
