-- ===========================================
-- JTAC FOR OBJECTIVE #$OBJECTIVEID$
-- ===========================================

hq.jtac$OBJECTIVEID$NextTargetSmoke = -999999 -- time when next smoke marker will be available

hq.jtac$OBJECTIVEID$LaserSpot = nil -- current laser spot
hq.jtac$OBJECTIVEID$LaserTargetUnitName = nil -- the name of the current lased unit
hq.jtac$OBJECTIVEID$LaserCode = 1688 -- the laser code for this JTAC

------------------------------------------------------------------------
-- Finds a target for the JTAC. Returns an unit if found one, nil if not
------------------------------------------------------------------------
function hq.getJTAC$OBJECTIVEID$Target()
  local g = hq.getGroupByID(1000 + $OBJECTIVEID$)
  if g == nil then return nil end

  for __,u in ipairs(g:getUnits()) do
    if u:getLife() > 0 then
      local validUnit = false

      if jtac$OBJECTIVEID$TargetAttribute == nil then
        validUnit = true
      elseif u:hasAttribute(jtac$OBJECTIVEID$TargetAttribute) then
        validUnit = true
      end

      if validUnit == true then return u end
    end
  end

  return nil
end

-- ===========================================
--  TOOL FUNCTIONS
-- ===========================================

-----------------------------------------------------------------------------------
-- Update the JTAC's laser. Runs every second when lasing, every 2 seconds when not
-----------------------------------------------------------------------------------
function hq.updateJTAC$OBJECTIVEID$Laser(args, time)
  local invalidTarget = false

  if hq.isUnitAlive(hq.jtac$OBJECTIVEID$LaserTargetUnitName) == false then
    -- trigger.action.outText("No laser", 1, false)
    if hq.jtac$OBJECTIVEID$LaserSpot ~= nil then
      Spot.destroy(hq.jtac$OBJECTIVEID$LaserSpot)
      hq.jtac$OBJECTIVEID$LaserSpot = nil
    end
    hq.jtac$OBJECTIVEID$LaserSpot = nil
    hq.jtac$OBJECTIVEID$LaserTargetUnitName = nil
    return time + 2
  end

  local unit = Unit.getByName(hq.jtac$OBJECTIVEID$LaserTargetUnitName)

  local targetPos = unit:getPoint()
  local targetSpeed = unit:getVelocity()
  targetPos.x = targetPos.x + targetSpeed.x
  targetPos.y = targetPos.y + 2.0
  targetPos.z = targetPos.z + targetSpeed.z

  trigger.action.outText("Updating laser: "..tostring(targetPos.x)..", "..tostring(targetPos.z), 1, false)

  if hq.jtac$OBJECTIVEID$LaserSpot == nil then
    hq.jtac$OBJECTIVEID$LaserSpot = Spot.createLaser(unit, { x = 0, y = 2.0, z = 0 }, targetPos, hq.jtac$OBJECTIVEID$LaserCode)
  else
    hq.jtac$OBJECTIVEID$LaserSpot:setPoint(targetPos)
  end

  return time + 1
end

function hq.jtac$OBJECTIVEID$StartLasing(unitName)
  hq.jtac$OBJECTIVEID$LaserTargetUnitName = unitName -- set target
end

function hq.jtac$OBJECTIVEID$StopLasing()
  hq.jtac$OBJECTIVEID$LaserTargetUnitName = nil
end

-----------------------------------------------
-- Radioes the coordinates of the JTAC's target
-----------------------------------------------
function hq.f10jtac$OBJECTIVEID$CoordinatesTarget()
  local unit = hq.getJTAC$OBJECTIVEID$Target()

  if unit == nil then
    hq.playRadioMessage(
      "£PlayerCoordinatesTarget£", "PlayerCoordinatesTarget",
      "£RadioSupportTargetNone£", "RadioNoTarget")
    return
  end

  hq.playRadioMessage(
    "£PlayerCoordinatesTarget£", "PlayerCoordinatesTarget",
    "£RadioTargetCoordinates£\n\n"..hq.vec3ToStringCoordinates(unit:getPoint()), "RadioTargetCoordinates", 15)
end

----------------------------------------
-- Marks the target with a smoke grenade
----------------------------------------
function hq.f10jtac$OBJECTIVEID$SmokeTarget()
  local unit = hq.getJTAC$OBJECTIVEID$Target()
  
  if unit == nil then
    hq.playRadioMessage(
      "£PlayerSmokeTarget£", "PlayerSmokeTarget",
      "£RadioNoTarget£", "RadioNoTarget")
    return
  end

  if timer.getAbsTime() < hq.jtac$OBJECTIVEID$NextTargetSmoke then
    hq.playRadioMessage(
      "£PlayerSmokeTarget£", "PlayerSmokeTarget",
      "£RadioSmokeTargetAlready£", "RadioSmokeTargetAlready")
    return
  end

  hq.jtac$OBJECTIVEID$NextTargetSmoke = timer.getAbsTime() + hq.constants.smokeDuration

  hq.playRadioMessage(
    "£PlayerSmokeTarget£", "PlayerSmokeTarget",
    "£RadioSmokeTarget£", "RadioSmokeTarget", nil,
    hq.markWithSmoke, { unit:getPoint(), trigger.smokeColor.Red })
end

------------------------------------
-- Starts lasing the target (if any)
------------------------------------
function hq.f10jtac$OBJECTIVEID$LaseTarget()
  if hq.jtac$OBJECTIVEID$LaserTargetUnitName == nil then -- no lasing target
    local unit = hq.getJTAC$OBJECTIVEID$Target() -- find a target

    -- no target found, return
    if unit == nil then
      hq.playRadioMessage(
        "£PlayerLaseTarget£", "PlayerLaseTarget",
        "£RadioNoTarget£", "RadioNoTarget")
      return
    end

    hq.playRadioMessage(
      "£PlayerLaseTarget£", "PlayerLaseTarget",
      string.replace("£RadioLaseTarget£", "$LASERCODE$", tostring(hq.jtac$OBJECTIVEID$LaserCode)), "RadioLaseTarget", nil,
      hq.jtac$OBJECTIVEID$StartLasing, nil)
  else -- already lasing a target
    -- if hq.isUnitAlive(hq.jtac$OBJECTIVEID$LaserTargetUnitName) == false then -- target is not valid anymore
      -- hq.jtac$OBJECTIVEID$LaserTargetUnitName = nil
      -- hq.f10jtac$OBJECTIVEID$Laser() -- look for another target
      -- return
    -- end

    hq.playRadioMessage(
      "£PlayerLaseTarget£", "PlayerLaseTarget",
      string.replace("£RadioLaseTargetAlready£", "$LASERCODE$", tostring(hq.jtac$OBJECTIVEID$LaserCode)), "RadioLaseTargetAlready")
  end
end

-----------------------------------
-- Stops lasing the target (if any)
-----------------------------------
function hq.f10jtac$OBJECTIVEID$LaseTargetStop()
  if hq.jtac$OBJECTIVEID$LaserTargetUnitName == nil then -- no lasing target
    hq.playRadioMessage(
      "£PlayerLaseTargetStop£", "PlayerLaseTargetStop",
      "£RadioLaseTargetNotLasing£", "RadioLaseTargetNotLasing")
  else -- we already have a lasing target
    hq.playRadioMessage(
      "£PlayerLaseTargetStop£", "PlayerLaseTargetStop",
      "£RadioLaseTargetStopped£", "RadioLaseTargetStopped", nil,
     hq.jtac$OBJECTIVEID$StopLasing, nil)
  end
end

------------------------
-- Add F10 menu commands
------------------------

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£PlayerCoordinatesTarget£",
  hq.objectiveSubmenu[$OBJECTIVEID$], hq.f10jtac$OBJECTIVEID$CoordinatesTarget)

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£PlayerSmokeTarget£",
  hq.objectiveSubmenu[$OBJECTIVEID$], hq.f10jtac$OBJECTIVEID$SmokeTarget)

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£PlayerLaseTarget£",
  hq.objectiveSubmenu[$OBJECTIVEID$], hq.f10jtac$OBJECTIVEID$LaseTarget)

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£PlayerLaseTargetStop£",
  hq.objectiveSubmenu[$OBJECTIVEID$], hq.f10jtac$OBJECTIVEID$LaseTargetStop)

---------------------
-- Schedule functions
---------------------

timer.scheduleFunction(hq.updateJTAC$OBJECTIVEID$Laser, nil, timer.getTime() + 1)
