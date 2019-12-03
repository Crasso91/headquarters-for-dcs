if event.id == world.event.S_EVENT_DEAD then
  if event.initiator then
    local group = event.initiator:getGroup()

    if (group:getID() == $GROUPID$) then
     if (group:getSize() <= 1) then
       hq.completeObjective($OBJECTIVEID$)
     else
       hq.playRadioMessage("£Radio.RadioHQTargetDestroyed£", "RadioHQTargetDestroyed", nil, nil, nil, nil, nil)
     end
    end
  end
end
