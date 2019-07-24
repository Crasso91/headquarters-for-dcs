hq.photographyTargets = $PHOTOGRAPHYTARGETS$
hq.photographyStaticTargets = $STATICTARGETS$

-- play the message a little bit later, so the photo shutter sound can be heard
function hq.photographCompleteMessage(parameter, time)
  if #hq.photographyTargets == 0 then
    hq.setMissionStatus(true)
  else
    hq.playRadioMessage("missionAnnounce.objectiveComplete")
    -- little chance to active a reserve enemy CAP group on each objective completed
    if math.random(1, math.max(1, $PHOTOCOUNT$)) == 1 then hq.activateCAPGroup() end
  end

  return nil
end

function hq.f10Photography()
  trigger.action.outSoundForCoalition(coalition.side.$PLAYERCOALITION$, "l10n/DEFAULT/photo.ogg" )

  for _,p in ipairs(coalition.getPlayers(coalition.side.$PLAYERCOALITION$)) do
    local groundPt = land.getIP(p:getPoint(), p:getVelocity(), 150)

    if groundPt ~= nil then
      if hq.photographyStaticTargets then
        for i,v in ipairs(hq.photographyTargets) do
          if math.distance({ x = groundPt.x, y = groundPt.z }, v) < 100 then
            table.remove(hq.photographyTargets, i)
            timer.scheduleFunction(hq.photographCompleteMessage, nil, timer.getTime() + 2)
            return
          end
        end
      else
        for i,v in ipairs(hq.photographyTargets) do
          for _,g in ipairs(coalition.getGroups(coalition.side.$ENEMYCOALITION$)) do
            if g:getID() == v then
              for __,u in ipairs(g:getUnits()) do
                if u:getLife() > 0 then
                  local targetPosition = { x = u:getPoint().x, y = u:getPoint().z }
                  if math.distance({ x = groundPt.x, y = groundPt.z }, targetPosition) < 75 then
                    table.remove(hq.photographyTargets, i)
                    timer.scheduleFunction(hq.photographCompleteMessage, nil, timer.getTime() + 2)
                    return
                  end
                end
              end
            end
          end
        end
      end
    end
  end
end

missionCommands.addCommandForCoalition(coalition.side.$PLAYERCOALITION$, "$PHOTOGRAPHMENUITEM$", nil, hq.f10Photography)
