-- ===============================================
-- COMMON HQ4DCS STUFF
-- ===============================================

hq = { }

hq.constants = { }
hq.constants.artillerySpread = 150 -- artillery inaccuracy radius is 150m
hq.constants.smokeDuration = 300 -- smoke markers last for 5 minutes (300 seconds) in DCS
hq.constants.timeForRadioAnswer = { 5, 7 } -- min/max time (in seconds) before a radio message gets an answer. Include the initial message duration (around 3 seconds for most messages)

hq.objectiveCount = $OBJECTIVECOUNT$
hq.objectiveNames = $OBJECTIVENAMES$

-- MISSION AND OBJECTIVE STATUS
-- 0 = in progress, < 0 = failed, > 0 = complete
hq.missionStatus = 0
hq.objectiveStatus = { }
for i=1,hq.objectiveCount do table.insert(hq.objectiveStatus, 0) end

--hq.enableF10MapTargetDesignation = $ENABLEF10MAPTARGETDESIGNATION$
--hq.lateActivationCAPGroups = $LATEACTIVATIONCAPGROUPS$
--hq.groupsToActiveOnFirstTakeOff = $GROUPSTOACTIVEONFIRSTTAKEOFF$
-- hq.designationText = "$DESIGNATIONCOMMAND$"
-- hq.designationMenuItems = { }
-- hq.designationRemovedMarkersIDX = { }

hq.playerTookOff = false -- becomes true once at least one player took off

-- The ID of the "answering" radio message.
-- Stored so we can cancel/unschedule the function if another radio transmission is made before the answer is played.
hq.radioMessageScheduledID = nil

-- ===============================================
-- UNIT NAMES
-- ===============================================

$UNITNAMES$

-- ===============================================
-- LUA "TOOLBOX" FUNCTIONS
-- ===============================================

---------------------------------
-- Returns a deep copy of a table
---------------------------------
function table.copy(t) -- code from MihailJP (https://gist.github.com/MihailJP/3931841)
  if type(t) ~= "table" then return t end

  local meta = getmetatable(t)
  local target = {}
  for k, v in pairs(t) do
    if type(v) == "table" then
      target[k] = table.copy(v)
    else
      target[k] = v
    end
  end
  setmetatable(target, meta)
  return target
end

---------------------------------------------------------------
-- Replaces every occurence of OLDSTRING in text with NEWSTRING
---------------------------------------------------------------
function string.replace(text, oldstring, newstring)
  if type(newstring) == "boolean" then
    if newstring == true then newstring = "true" else newstring = "false" end
  elseif type(newstring) == "number" then
    newstring = tostring(newstring)
  end

  newstring = newstring or ""

  return text:gsub(oldstring, newstring)
end

-----------------------------------------------------
-- Returns a random value from a number-indexed table
-----------------------------------------------------
function table.random(t)
  if (type(t) ~= "table") or (#t == 0) then return nil end

  return table.copy(t[math.random(1, #t)])
end

-----------------------------------------------------------------
-- Shuffles a table
-- (Code by Uradamus : https://gist.github.com/Uradamus/10323382)
-----------------------------------------------------------------
function table.shuffle(t)
  size = #t
  for i = size, 1, -1 do
    local rand = math.random(size)
    t[i], t[rand] = t[rand], t[i]
  end
  return t
end

----------------------------------------------------------
-- MATH.ROUND (from http://lua-users.org/wiki/SimpleRound)
-- Returns a rounded number with N decimal places.
----------------------------------------------------------
function math.round(num, numDecimalPlaces)
  local mult = 10^(numDecimalPlaces or 0)
  return math.floor(num * mult + 0.5) / mult
end

--------------------------------------------
-- Set the mission status (completed/failed)
--------------------------------------------
function hq.setMissionStatus(completed)
  if hq.missionStatus ~= nil then return end -- Mission already failed or completed, ignore

  hq.missionStatus = completed
  if hq.missionStatus == true then
    hq.playRadioMessage("missionAnnounce.complete")
    hq.activateCAPGroup(true) -- activate all non-activated CAP groups left
  else
    hq.playRadioMessage("missionAnnounce.failed")
  end
end

---------------------------------------------------------------------
-- HQ.MARKWITHSMOKE
-- An alternative to "trigger.action.smoke" with a table as parameter
-- Table index #1 is position (vec3), #2 is smoke color
---------------------------------------------------------------------
function hq.markWithSmoke(args)
  trigger.action.smoke(args[1], args[2])
end

-----------------------------------------------------------
-- Returns true if T contains E, false if it doesn't
-----------------------------------------------------------
function table.contains(t, e)
  for _,v in ipairs(t) do
    if v == e then return true end
  end

  return false
end

---------------------------------
-- Finds a group by its ID
---------------------------------
function hq.getGroupByID(id)
  for coalID=1,2 do
    for _,g in pairs(coalition.getGroups(coalID)) do
      if g:getID() == id then return g end
    end
  end
  return nil
end

function hq.isUnitAlive(name)
  if name == nil then return false end
  local unit = Unit.getByName(name)
  if unit == nil then return false end
  if unit:isActive() == false then return false end
  if unit:getLife() < 1 then return false end

  return true
end

---------------------------------
-- Active a late activation CAP group
---------------------------------
function hq.activateCAPGroup(activateAll)
  if #hq.lateActivationCAPGroups == 0 then return false end
  activateAll = activateAll or false

  if activateAll then -- activate all groups left
    for _,v in ipairs(hq.lateActivationCAPGroups) do
      local group = hq.getGroupByID(v)
      if group ~= nil then group:activate() end
    end
    hq.lateActivationCAPGroups = { }
  else -- active the first group in queue
    local group = hq.getGroupByID(hq.lateActivationCAPGroups[1])
    if group ~= nil then group:activate() end
    table.remove(hq.lateActivationCAPGroups, 1)
  end

  return true
end

---------------------------------
-- Returns the distance between two 2d points
---------------------------------
function math.distance(p1, p2)
  local dX = p1.x - p2.x
  local dY = p1.y - p2.y
  return math.sqrt(dX * dX + dY * dY)
end

------------------------
-- Plays a radio message
------------------------
function hq.playRadioMessage(message, oggFile, message2, oggFile2, duration, functionToRun, functionParameters)
  -- unschedule any radio message answer
  if hq.radioMessageScheduledID ~= nil then timer.removeFunction(hq.radioMessageScheduledID) end
  hq.radioMessageScheduledID = nil

  oggFile = oggFile or "radio0"
  oggFile = oggFile:lower()
  duration = duration or 5 -- default duration is 5 seconds

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

------------------------------------------------------------
-- Converts a time value in seconds to hours/minutes/seconds
------------------------------------------------------------
function hq.timeToHMS(timecode)
  local h = math.floor(timecode / 3600)
  timecode = timecode - h * 3600
  local m = math.floor(timecode / 60)
  timecode = timecode - m * 60
  local s = timecode

  return string.format("%.2i:%.2i:%.2i", h, m, s)
end

----------------------------------------------------------------------------------------
-- Converts a DCS vec3 to lat/long string (D°M'S" and decimal) and MGRS grid coordinates
-- Based on code by Bushmanni - https://forums.eagle.ru/showthread.php?t=99480
----------------------------------------------------------------------------------------
function hq.vec3ToStringCoordinates(pos)
  local cooString = ""

  local LLposN, LLposE = coord.LOtoLL(pos)
  local LLposfixN, LLposdegN = math.modf(LLposN)
  LLposdegN = LLposdegN * 60
  local LLposdegN2, LLposdegN3 = math.modf(LLposdegN)
  local LLposdegN3Decimal = LLposdegN3 * 1000
  LLposdegN3 = LLposdegN3 * 60

  local LLposfixE, LLposdegE = math.modf(LLposE)
  LLposdegE = LLposdegE * 60
  local LLposdegE2, LLposdegE3 = math.modf(LLposdegE)
  local LLposdegE3Decimal = LLposdegE3 * 1000
  LLposdegE3 = LLposdegE3 * 60
  
  -- TODO: LLposdegN4, LLposdegE4 (so precision is high enough for GPS bombs) - check if LLposdegN3Decimal is not enough
  -- TODO: bullseye: coalition.getMainRefPoint(enum coalitionId )

  local LLns = "N"
  if LLposfixN < 0 then LLns = "S" end
  local LLew = "E"
  if LLposfixE < 0 then LLew = "W" end

  local LLposNstring = LLns.." "..string.format("%.2i°%.2i'%.2i''", LLposfixN, LLposdegN2, LLposdegN3)
  local LLposEstring = LLew.." "..string.format("%.3i°%.2i'%.2i''", LLposfixE, LLposdegE2, LLposdegE3)
  cooString = "L/L: "..LLposNstring.." "..LLposEstring

  local LLposNstring = LLns.." "..string.format("%.2i°%.2i.%.3i", LLposfixN, LLposdegN2, LLposdegN3Decimal)
  local LLposEstring = LLew.." "..string.format("%.3i°%.2i.%.3i", LLposfixE, LLposdegE2, LLposdegE3Decimal)
  cooString = cooString.."\nL/L: "..LLposNstring.." "..LLposEstring

  local mgrs = coord.LLtoMGRS(LLposN, LLposE)
  local mgrsString = mgrs.MGRSDigraph.." "..mgrs.UTMZone.." "..tostring(mgrs.Easting).." "..tostring(mgrs.Northing)
  cooString = cooString.."\nMGRS: "..mgrsString

  cooString = cooString.."\nElevation: "..tostring(math.round(pos.y * 3.28084)).." feet ("..math.round(tostring(pos.y), 1).." meters)"

  return cooString
end

function hq.updateDesignationMenuItems()
  -- Remove all panel menu options
  for _,v in ipairs(hq.designationMenuItems) do
    missionCommands.removeItemForCoalition(coalition.side.$PLAYERCOALITION$, v)
  end
  hq.designationMenuItems = { }

  local markPanels = world.getMarkPanels()
  for i=#markPanels,1,-1 do -- add last panel first
    if markPanels[i] ~= nil and
      markPanels[i].coalition == coalition.side.$PLAYERCOALITION$ and -- only add panels from the players' coalition
      (not table.contains(hq.designationRemovedMarkersIDX, markPanels[i].idx)) and -- do not add removed panels
      #hq.designationMenuItems < 10 then -- no more than 10 panels
        local cmdPath = missionCommands.addCommandForCoalition(
          coalition.side.$PLAYERCOALITION$,
          hq.designationText.." '"..markPanels[i].text.."' ("..hq.timeToHMS(markPanels[i].time)..")",
          hq.targetDesignationSubmenu,
          hq.performTargetDesignation,
          {
            name = markPanels[i].text,
            pos = { x = markPanels[i].pos.z, y = markPanels[i].pos.y, z = markPanels[i].pos.x }
          })
      table.insert(hq.designationMenuItems, cmdPath)
    end
  end
end

-- ===============================================
-- EVENT HANDLER
-- ===============================================

hq.eventHandler = {}
function hq.eventHandler:onEvent(event)
  --------------------------
  -- Target designation mark
  --------------------------
  if hq.enableF10MapTargetDesignation then
    if event.id == world.event.S_EVENT_MARK_ADDED or event.id == world.event.S_EVENT_MARK_REMOVED or event.id == world.event.S_EVENT_MARK_CHANGE then
      if event.id == world.event.S_EVENT_MARK_REMOVED then table.insert(hq.designationRemovedMarkersIDX, event.idx) end
      hq.updateDesignationMenuItems()
    end
  end

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
  if event.id == world.event.S_EVENT_TAKEOFF then
    if event.initiator ~= nil then
      if (hq.playerTookOff == false) and (event.initiator:getPlayerName() ~= nil) then
        hq.playerTookOff = true
        for _,v in ipairs(hq.groupsToActiveOnFirstTakeOff) do
          local group = hq.getGroupByID(v)
          if group ~= nil then group:activate() end
        end
        hq.groupsToActiveOnFirstTakeOff = { }
      end
    end
  end

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
    hq.playRadioMessage("£RadioMessages/Player.MissionStatus£", "PlayerMissionStatus", "£RadioMessages/Radio.MissionStatusFailed£", "RadioMissionStatusFailed")
  elseif hq.missionStatus > 0 then -- mission complete
    hq.playRadioMessage("£RadioMessages/Player.MissionStatus£", "PlayerMissionStatus", "£RadioMessages/Radio.MissionStatusComplete£", "RadioMissionStatusComplete")
  else  -- mission in progress, show status of each objective
    local objectiveStatusList = "\n";
    for i=1,hq.objectiveCount do
      objectiveStatusList = objectiveStatusList.."\nObjective "..hq.objectiveNames[i].." "
      if (objectiveStatus[i] > 0) then objectiveStatusList = objectiveStatusList.."[X]"
      else objectiveStatusList = objectiveStatusList.."[ ]" end
    end

    hq.playRadioMessage("£RadioMessages/Player.MissionStatus£", "PlayerMissionStatus", "£RadioMessages/Radio.MissionStatusInProgress£"..objectiveStatusList, "RadioMissionStatusInProgress")
  end
end

-- "Mission status" F10 menu
missionCommands.addCommandForCoalition(coalition.side.$PLAYERCOALITION$, "£RadioMessages/Player.MissionStatus£", nil, hq.f10MissionStatus)

$SCRIPTGLOBAL$
