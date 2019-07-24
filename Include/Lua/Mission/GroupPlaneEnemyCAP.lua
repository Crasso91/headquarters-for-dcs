[$INDEX$] =
{
  ["modulation"] = 0,
  ["lateActivation"] = $LATEACTIVATION$,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["task"] = "CAP",
  ["uncontrolled"] = false,
  ["taskSelected"] = true,
  ["route"] =
  {
    ["points"] =
    {
      [1] =
      {
        ["alt"] = 6096,
        ["action"] = "Turning Point",
        ["alt_type"] = "BARO",
        ["speed"] = 220,
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
                ["key"] = "CAP",
                ["id"] = "EngageTargets",
                ["number"] = 1,
                ["auto"] = true,
                ["params"] =
                {
                  ["targetTypes"] =
                  {
                    [1] = "Air",
                  }, -- end of ["targetTypes"]
                  ["priority"] = 0,
                }, -- end of ["params"]
              }, -- end of [1]
              [2] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "Orbit",
                ["number"] = 2,
                ["params"] =
                {
                  ["altitude"] = 6096,
                  ["pattern"] = "Circle",
                  ["speed"] = 220,
                  ["altitudeEdited"] = true,
                }, -- end of ["params"]
              }, -- end of [2]
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
  ["communication"] = true,
  ["start_time"] = 0,
  ["frequency"] = $FREQUENCY$,
}, -- end of [$INDEX$]
