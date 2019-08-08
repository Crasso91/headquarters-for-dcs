-- ===============================================
-- COMMON HQ4DCS STUFF
-- ===============================================

hq = { }

-- ===============================================
-- EVENT HANDLER
-- ===============================================

hq.eventHandler = {}
function hq.eventHandler:onEvent(event)
  --------------------------
  -- Target designation mark
  --------------------------
--   if hq.enableF10MapTargetDesignation then
--     if event.id == world.event.S_EVENT_MARK_ADDED or event.id == world.event.S_EVENT_MARK_REMOVED or event.id == world.event.S_EVENT_MARK_CHANGE then
--       if event.id == world.event.S_EVENT_MARK_REMOVED then table.insert(hq.designationRemovedMarkersIDX, event.idx) end
--       hq.updateDesignationMenuItems()
--     end
--   end

  -------------------------------------------------------
  -- Ugly hack to make parked airplanes easier to destroy
  -- DCS won't mark them as destroyed unless they're
  -- at 0 HP, even if both wings have been clipped off
  -------------------------------------------------------
  -- if event.id == world.event.S_EVENT_HIT then
  --   if target ~= nil then
  --     if target:inAir() == false and target:getCoalition() == coalition.side.$ENEMYCOALITION$ then
  --       if (target:getCategory() == Unit.Category.AIRPLANE) or (target:getCategory() == Unit.Category.HELICOPTER) then
  --         trigger.action.explosion(target:getPoint(), 100)
  --       end
  --     end
  --   end
  -- end

  -------------------------------------------------------
  -- Actions to perform when the first player takes off
  -------------------------------------------------------
--   if event.id == world.event.S_EVENT_TAKEOFF then
--     if event.initiator ~= nil then
--       if (hq.playerTookOff == false) and (event.initiator:getPlayerName() ~= nil) then
--         hq.playerTookOff = true
--         for _,v in ipairs(hq.groupsToActiveOnFirstTakeOff) do
--           local group = hq.getGroupByID(v)
--           if group ~= nil then group:activate() end
--         end
--         hq.groupsToActiveOnFirstTakeOff = { }
--       end
--     end
--   end

  -- Custom events for the mission
  $SCRIPTEVENT$
end
world.addEventHandler(hq.eventHandler)

-- ===============================================
-- SCHEDULED FUNCTION, RAN EVERY SECOND
-- ===============================================

function hq.everySecond(args, time)
  $SCRIPTTIMER$
  return time + 1
end
timer.scheduleFunction(hq.everySecond, nil, timer.getTime() + 1)

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
