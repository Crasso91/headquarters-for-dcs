-- ===============================================
-- HQ4DCS DEBUG MENU
-- ===============================================

hq.debugMenu = { }

function hq.debugMenu.destroyEnemyUnit()
  for _,g in ipairs(coalition.getGroups(coalition.side.$ENEMYCOALITION$)) do
    for __,u in ipairs(g:getUnits()) do
      if u:getLife() > 0 then
        trigger.action.outText(g:getName(), 3)
        trigger.action.explosion(u:getPoint(), 100)
        return
      end
    end
  end
end

function hq.debugMenu.destroyEnemyStructure()
  for _,s in ipairs(coalition.getStaticObjects(coalition.side.$ENEMYCOALITION$)) do
    if s:getLife() > 0 then
      trigger.action.outText(s:getName(), 3)
      for i=1,32 do trigger.action.explosion(s:getPoint(), 100) end
      return
    end
  end
end

function hq.debugMenu.activateEnemyCAPGroup()
  if hq.activateCAPGroup() then
    trigger.action.outText("Enemy CAP group activated", 3)
  else
    trigger.action.outText("No enemy CAP group left to activate", 3)
  end
end

function hq.debugMenu.simulatePlayerTakeOff()
  if hq.playerTookOff == true then return end

  hq.playerTookOff = true
  for _,v in ipairs(hq.groupsToActiveOnFirstTakeOff) do
    local group = hq.getGroupByID(v)
    if group ~= nil then group:activate() end
  end
  hq.groupsToActiveOnFirstTakeOff = { }
  trigger.action.outText("Player takeoff simulated", 3)
end

local _DEBUGMENU = missionCommands.addSubMenu("(HQ4DCS DEBUG MENU)", nil)

missionCommands.addCommand("Destroy enemy unit", _DEBUGMENU, hq.debugMenu.destroyEnemyUnit)
missionCommands.addCommand("Destroy enemy structure", _DEBUGMENU, hq.debugMenu.destroyEnemyStructure)
missionCommands.addCommand("Activate enemy CAP group", _DEBUGMENU, hq.debugMenu.activateEnemyCAPGroup)
missionCommands.addCommand("Simulate player takeoff", _DEBUGMENU, hq.debugMenu.simulatePlayerTakeOff)
