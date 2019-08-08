-- TODO: check objective is not complete yet

if event.id == world.event.S_EVENT_DEAD then
  if event.initiator then
    local group = event.initiator:getGroup()

    if (group:getID() == $GROUPID$) and (group:getSize() <= 1) then
      -- TODO: objective complete
    end
  end
end
