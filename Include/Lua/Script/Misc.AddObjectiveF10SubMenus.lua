hq.objectiveSubmenu = { }

for i=1,hq.objectiveCount do
  table.insert(hq.objectiveSubmenu, missionCommands.addSubMenuForCoalition(coalition.side.$PLAYERCOALITION$, "Objective "..hq.objectiveNames[i], nil))
end
