-- ===========================================
-- JTAC FOR OBJECTIVE #$ID$
-- ===========================================

hq.jtac$ID$NextTargetSmoke = -999999 -- time when next smoke marker will be available

hq.jtac$ID$LaserSpot = nil -- current laser spot
hq.jtac$ID$LaserTargetUnitName = nil -- the name of the current lased unit
hq.jtac$ID$LaserCode = 1688 -- the laser code for this JTAC

------------------------------------------------------------------------
-- Finds a target for the JTAC. Returns an unit if found one, nil if not
------------------------------------------------------------------------
function hq.getJTAC$ID$Target()
  local g = hq.getGroupByID($ID$000)
  if g == nil then return nil end

  for __,u in ipairs(g:getUnits()) do
    if u:getLife() > 0 then
      local validUnit = false

      if jtac$ID$TargetAttribute == nil then
        validUnit = true
      elseif u:hasAttribute(jtac$ID$TargetAttribute) then
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
function hq.updateJTAC$ID$Laser(args, time)
  local invalidTarget = false

  if hq.isUnitAlive(hq.jtac$ID$LaserTargetUnitName) == false then
    -- trigger.action.outText("No laser", 1, false)
    if hq.jtac$ID$LaserSpot ~= nil then
      Spot.destroy(hq.jtac$ID$LaserSpot)
      hq.jtac$ID$LaserSpot = nil
    end
    hq.jtac$ID$LaserSpot = nil
    hq.jtac$ID$LaserTargetUnitName = nil
    return time + 2
  end

  local unit = Unit.getByName(hq.jtac$ID$LaserTargetUnitName)

  local targetPos = unit:getPoint()
  local targetSpeed = unit:getVelocity()
  targetPos.x = targetPos.x + targetSpeed.x
  targetPos.y = targetPos.y + 2.0
  targetPos.z = targetPos.z + targetSpeed.z

  trigger.action.outText("Updating laser: "..tostring(targetPos.x)..", "..tostring(targetPos.z), 1, false)

  if hq.jtac$ID$LaserSpot == nil then
    hq.jtac$ID$LaserSpot = Spot.createLaser(unit, { x = 0, y = 2.0, z = 0 }, targetPos, hq.jtac$ID$LaserCode)
  else
    hq.jtac$ID$LaserSpot:setPoint(targetPos)
  end

  return time + 1
end

function hq.jtac$ID$StartLasing(unitName)
  hq.jtac$ID$LaserTargetUnitName = unitName -- set target
end

function hq.jtac$ID$StopLasing()
  hq.jtac$ID$LaserTargetUnitName = nil
end

-----------------------------------------------
-- Radioes the coordinates of the JTAC's target
-----------------------------------------------
function hq.f10jtac$ID$CoordinatesTarget()
  local unit = hq.getJTAC$ID$Target()

  if unit == nil then
    hq.playRadioMessage(
      "£RadioMessages/Player.CoordinatesTarget£", "Player.CoordinatesTarget",
      "£RadioMessages/Radio.NoTarget£", "Radio.NoTarget")
    return
  end

  hq.playRadioMessage(
    "£RadioMessages/Player.CoordinatesTarget£", "Player.CoordinatesTarget",
    "£RadioMessages/Radio.TargetCoordinates£\n\n"..hq.vec3ToStringCoordinates(unit:getPoint()), "Radio.TargetCoordinates", 15)
end

----------------------------------------
-- Marks the target with a smoke grenade
----------------------------------------
function hq.f10jtac$ID$SmokeTarget()
  local unit = hq.getJTAC$ID$Target()
  
  if unit == nil then
    hq.playRadioMessage(
      "£RadioMessages/Player.SmokeTarget£", "Player.SmokeTarget",
      "£RadioMessages/Radio.NoTarget£", "Radio.NoTarget")
    return
  end

  if timer.getAbsTime() < hq.jtac$ID$NextTargetSmoke then
    hq.playRadioMessage(
      "£RadioMessages/Player.SmokeTarget£", "Player.SmokeTarget",
      "£RadioMessages/Radio.SmokeTargetAlready£", "Radio.SmokeTargetAlready")
    return
  end

  hq.jtac$ID$NextTargetSmoke = timer.getAbsTime() + hq.constants.smokeDuration

  hq.playRadioMessage(
    "£RadioMessages/Player.SmokeTarget£", "Player.SmokeTarget",
    "£RadioMessages/Radio.SmokeTarget£", "Radio.SmokeTarget", nil,
    hq.markWithSmoke, { unit:getPoint(), trigger.smokeColor.Red })
end

------------------------------------
-- Starts lasing the target (if any)
------------------------------------
function hq.f10jtac$ID$LaseTarget()
  if hq.jtac$ID$LaserTargetUnitName == nil then -- no lasing target
    local unit = hq.getJTAC$ID$Target() -- find a target

    -- no target found, return
    if unit == nil then
      hq.playRadioMessage(
        "£RadioMessages/Player.LaseTarget£", "Player.LaseTarget",
        "£RadioMessages/Radio.NoTarget£", "Radio.NoTarget")
      return
    end

    hq.playRadioMessage(
      "£RadioMessages/Player.LaseTarget£", "Player.LaseTarget",
      string.replace("£RadioMessages/Radio.LaseTarget£", "$LASERCODE$", tostring(hq.jtac$ID$LaserCode)), "Radio.LaseTarget", nil,
      hq.jtac$ID$StartLasing, nil)
  else -- already lasing a target
    -- if hq.isUnitAlive(hq.jtac$ID$LaserTargetUnitName) == false then -- target is not valid anymore
      -- hq.jtac$ID$LaserTargetUnitName = nil
      -- hq.f10jtac$ID$Laser() -- look for another target
      -- return
    -- end

    hq.playRadioMessage(
      "£RadioMessages/Player.LaseTarget£", "Player.LaseTarget",
      string.replace("£RadioMessages/Radio.LaseTargetAlready£", "$LASERCODE$", tostring(hq.jtac$ID$LaserCode)), "Radio.LaseTargetAlready")
  end
end

-----------------------------------
-- Stops lasing the target (if any)
-----------------------------------
function hq.f10jtac$ID$LaseTargetStop()
  if hq.jtac$ID$LaserTargetUnitName == nil then -- no lasing target
    hq.playRadioMessage(
      "£RadioMessages/Player.LaseTargetStop£", "Player.LaseTargetStop",
      "£RadioMessages/Radio.LaseTargetNotLasing£", "Radio.LaseTargetNotLasing")
  else -- we already have a lasing target
    hq.playRadioMessage(
      "£RadioMessages/Player.LaseTargetStop£", "Player.LaseTargetStop",
      "£RadioMessages/Radio.LaseTargetStopped£", "Radio.LaseTargetStopped", nil,
     hq.jtac$ID$StopLasing, nil)
  end
end

------------------------
-- Add F10 menu commands
------------------------

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£RadioMessages/Player.CoordinatesTarget£",
  hq.objectiveSubmenu[$ID$], hq.f10jtac$ID$CoordinatesTarget)

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£RadioMessages/Player.SmokeTarget£",
  hq.objectiveSubmenu[$ID$], hq.f10jtac$ID$SmokeTarget)

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£RadioMessages/Player.LaseTarget£",
  hq.objectiveSubmenu[$ID$], hq.f10jtac$ID$LaseTarget)

missionCommands.addCommandForCoalition(
  coalition.side.$PLAYERCOALITION$, "£RadioMessages/Player.LaseTargetStop£",
  hq.objectiveSubmenu[$ID$], hq.f10jtac$ID$LaseTargetStop)

---------------------
-- Schedule functions
---------------------

timer.scheduleFunction(hq.updateJTAC$ID$Laser, nil, timer.getTime() + 1)
