-- ===============================================
-- COMMON HQ4DCS STUFF
-- ===============================================

hq = { }
hq.constants = { }
hq.constants.artillerySpread = 150 -- artillery inaccuracy radius is 150m
hq.constants.defaultTextMessageDuration = 5 -- default duration (in seconds) for "subtitles" of radio messages
hq.constants.smokeDuration = 300 -- smoke markers last for 5 minutes (300 seconds) in DCS World
hq.constants.timeForRadioAnswer = { 4, 6 } -- min/max time (in seconds) before a radio message gets an answer. Include the initial message duration (around 3 seconds for most messages)

hq.missionComplete = false -- is the mission complete yet?
hq.playerTookOff = false -- becomes true once at least one player took off
hq.radioMessageScheduledID = nil -- The ID of the "answer" radio message (stored so we can cancel/unschedule the function if another radio transmission is made before the answer is played)

$SCRIPTCOMMON$

------------------------
-- Plays a radio message
------------------------
function hq.playRadioMessage(message, oggFile, message2, oggFile2, duration, functionToRun, functionParameters)
  -- unschedule any radio message answer
  if hq.radioMessageScheduledID ~= nil then timer.removeFunction(hq.radioMessageScheduledID) end
  hq.radioMessageScheduledID = nil

  oggFile = oggFile or "radio0"
  oggFile = oggFile:lower()
  duration = duration or hq.constants.defaultTextMessageDuration

  if message2 ~= nil then
    trigger.action.outTextForCoalition(coalition.side.$PLAYERCOALITION$, message, hq.constants.defaultTextMessageDuration)
  else
    trigger.action.outTextForCoalition(coalition.side.$PLAYERCOALITION$, message, duration)
  end

  trigger.action.outSoundForCoalition(coalition.side.$PLAYERCOALITION$, "l10n/DEFAULT/"..oggFile..".ogg" )

  if message2 ~= nil then
    oggFile2 = oggFile2 or "radio0"
    oggFile2 = oggFile2:lower()

    hq.radioMessageScheduledID = timer.scheduleFunction(
      hq.playRadioMessageAnswer,
      { message2, oggFile2, duration, functionToRun, functionParameters },
      timer.getTime() + math.random(hq.constants.timeForRadioAnswer[1], hq.constants.timeForRadioAnswer[2]))
  else
    if (functionToRun ~= nil) then
      functionToRun(functionParameters)
    end
  end
end

function hq.playRadioMessageAnswer(msgParameters, time)
  trigger.action.outTextForCoalition(coalition.side.$PLAYERCOALITION$, msgParameters[1], msgParameters[3])
  trigger.action.outSoundForCoalition(coalition.side.$PLAYERCOALITION$, "l10n/DEFAULT/"..msgParameters[2]..".ogg" )

  -- Run function (if a function was provided)
  if (msgParameters[4] ~= nil) then
    msgParameters[4](msgParameters[5])
  end
  
  hq.radioMessageScheduledID = nil

  return nil
end

-------------------------
-- Completes an objective
-------------------------
function hq.completeObjective(objectiveID)
  if (hq.objectiveStatus[objectiveID] == false) and (hq.missionComplete == false) then
     hq.objectiveStatus[objectiveID] = true
     hq.objectiveLeft = hq.objectiveLeft - 1
     
     if (hq.objectiveLeft <= 0) then -- all objectives complete, mission complete
      hq.missionComplete = true
      hq.playRadioMessage( "£Radio.RadioHQMissionEndComplete£", "RadioHQMissionEndComplete", nil, nil, nil, nil, nil)
     else
      hq.playRadioMessage( "£Radio.RadioHQObjectiveComplete£", "RadioHQObjectiveComplete", nil, nil, nil, nil, nil)
     end
  end
end

--------------------------
-- Finds a group by its ID
--------------------------
function hq.getGroupByID(id)
  for coalID=1,2 do
    for _,g in pairs(coalition.getGroups(coalID)) do
      if g:getID() == id then return g end
    end
  end

  return nil
end

--------------------
-- Is an unit alive?
--------------------
function hq.isUnitAlive(name)
  if name == nil then return false end
  local unit = Unit.getByName(name)
  if unit == nil then return false end
  if unit:isActive() == false then return false end
  if unit:getLife() < 1 then return false end

  return true
end

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

-- Request update on mission status
function hq.f10MissionStatus()
  if hq.missionStatus < 0 then -- mission failed
    hq.playRadioMessage("£Player.MissionStatus£", "PlayerMissionStatus", "£Radio.MissionStatusFailed£", "RadioMissionStatusFailed")
  elseif hq.missionStatus > 0 then -- mission complete
    hq.playRadioMessage("£Player.MissionStatus£", "PlayerMissionStatus", "£Radio.MissionStatusComplete£", "RadioMissionStatusComplete")
  else  -- mission in progress, show status of each objective
    local objectiveStatusList = "\n";
    for i=1,hq.objectiveCount do
      objectiveStatusList = objectiveStatusList.."\nObjective "..hq.objectiveNames[i].." "
      if (hq.objectiveStatus[i] == true) then objectiveStatusList = objectiveStatusList.."[X]"
      else objectiveStatusList = objectiveStatusList.."[ ]" end
    end

    hq.playRadioMessage("£Player.MissionStatus£", "PlayerMissionStatus", "£Radio.MissionStatusInProgress£"..objectiveStatusList, "RadioMissionStatusInProgress")
  end
end

-- Create common "other" menu
hq.f10Menu = { }
missionCommands.addCommandForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.MissionStatus£", nil, hq.f10MissionStatus)
hq.f10Menu.FlightPlan=missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.FlightPlan£", nil)
hq.f10Menu.FlightPlanItems = { }
hq.f10Menu.Objective=missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.Objectives£", nil)
hq.f10Menu.ObjectiveItems = { }
hq.f10Menu.Support=missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "£F10Menu.Main.Support£", nil)
hq.f10Menu.SupportItems = { }

$SCRIPTGLOBAL$

-- TODO: remove unused F10 menus if no subitems
