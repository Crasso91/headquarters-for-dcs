[$INDEX$] =
{
  ["modulation"] = 0,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["task"] = "CAS",
  ["uncontrolled"] = false,
  ["taskSelected"] = true,
  ["route"] =
  {
    ["points"] =
    {
      [1] =
      {
        ["alt"] = $ALTITUDE$,
        ["action"] = "Turning Point",
        ["alt_type"] = "BARO",
        ["speed"] = $SPEED$,
        ["task"] =
        {
          ["id"] = "ComboTask",
          ["params"] =
          {
            ["tasks"] =
            {
              [1] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "Orbit",
                ["number"] = 1,
                ["params"] =
                {
                  ["altitude"] = $ALTITUDE$,
                  ["pattern"] = "Circle",
                  ["speed"] = $SPEED$,
                  ["altitudeEdited"] = true,
                }, -- end of ["params"]
              }, -- end of [5]
            }, -- end of ["tasks"]
          }, -- end of ["params"]
        }, -- end of ["task"]
        ["type"] = "Turning Point",
        ["ETA"] = 0,
        ["ETA_locked"] = true,
        ["y"] = $Y$,
        ["x"] = $X$,
        ["name"] = "",
        ["formation_template"] = "",
        ["speed_locked"] = true,
      }, -- end of [1]
    }, -- end of ["points"]
  }, -- end of ["route"]
  ["groupId"] = $GROUPID$,
  ["hidden"] = $HIDDEN$,
  ["units"] =
  {
    $UNITS$
  }, -- end of ["units"]
  ["y"] = $Y$,
  ["x"] = $X$,
  ["name"] = "$NAME$",
  ["communication"] = false,
  ["start_time"] = 0,
  ["frequency"] = 251,
}, -- end of [$INDEX$]
