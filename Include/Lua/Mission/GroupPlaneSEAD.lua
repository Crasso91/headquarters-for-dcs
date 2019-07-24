[$INDEX$] =
{
  ["modulation"] = 0,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["task"] = "SEAD",
  ["uncontrolled"] = false,
  ["taskSelected"] = true,
  ["route"] =
  {
    ["points"] =
    {
      [1] =
      {
        ["alt"] = 5486.4,
        ["action"] = "Turning Point",
        ["alt_type"] = "BARO",
        ["speed"] = 220.97222222222,
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
                ["key"] = "SEAD",
                ["id"] = "EngageTargets",
                ["number"] = 1,
                ["auto"] = true,
                ["params"] =
                {
                  ["targetTypes"] =
                  {
                    [1] = "Air Defence",
                  }, -- end of ["targetTypes"]
                  ["priority"] = 0,
                }, -- end of ["params"]
              }, -- end of [1]
              [2] =
              {
                ["enabled"] = true,
                ["auto"] = true,
                ["id"] = "WrappedAction",
                ["number"] = 2,
                ["params"] =
                {
                  ["action"] =
                  {
                    ["id"] = "Option",
                    ["params"] =
                    {
                      ["value"] = 4,
                      ["name"] = 1,
                    }, -- end of ["params"]
                  }, -- end of ["action"]
                }, -- end of ["params"]
              }, -- end of [2]
              [3] =
              {
                ["enabled"] = true,
                ["auto"] = true,
                ["id"] = "WrappedAction",
                ["number"] = 3,
                ["params"] =
                {
                  ["action"] =
                  {
                    ["id"] = "EPLRS",
                    ["params"] =
                    {
                      ["value"] = true,
                      ["groupId"] = $GROUPID$,
                    }, -- end of ["params"]
                  }, -- end of ["action"]
                }, -- end of ["params"]
              }, -- end of [3]

              [4] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "Orbit",
                ["number"] = 4,
                ["params"] =
                {
                  ["altitude"] = 5486.4,
                  ["pattern"] = "Circle",
                  ["speed"] = 138.88888888889,
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
