[$INDEX$] =
{
  ["modulation"] = 0,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["task"] = "Refueling",
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
                ["auto"] = true,
                ["id"] = "Tanker",
                ["number"] = 1,
                ["params"] =
                {
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
                    ["id"] = "ActivateBeacon",
                    ["params"] =
                    {
                      ["type"] = 4,
                      ["AA"] = true,
                      ["modeChannel"] = "X",
                      ["channel"] = 40,
                      ["system"] = 4,
                      ["callsign"] = "TKR",
                      ["bearing"] = true,
                      ["frequency"] = 1088000000,
                    }, -- end of ["params"]
                  }, -- end of ["action"]
                }, -- end of ["params"]
              }, -- end of [2]
              [3] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "WrappedAction",
                ["number"] = 3,
                ["params"] =
                {
                  ["action"] =
                  {
                    ["id"] = "SetInvisible",
                    ["params"] =
                    {
                      ["value"] = true,
                    }, -- end of ["params"]
                  }, -- end of ["action"]
                }, -- end of ["params"]
              }, -- end of [3]
              [4] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "WrappedAction",
                ["number"] = 4,
                ["params"] =
                {
                  ["action"] =
                  {
                    ["id"] = "SetImmortal",
                    ["params"] =
                    {
                      ["value"] = true,
                    }, -- end of ["params"]
                  }, -- end of ["action"]
                }, -- end of ["params"]
              }, -- end of [4]
              [5] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "Orbit",
                ["number"] = 5,
                ["params"] =
                {
                  ["altitude"] = 6096,
                  ["pattern"] = "Race-Track",
                  ["speed"] = 136.11111111111,
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
      [2] =
      {
        ["alt"] = 6096,
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
            }, -- end of ["tasks"]
          }, -- end of ["params"]
        }, -- end of ["task"]
        ["type"] = "Turning Point",
        ["ETA"] = 0.0,
        ["ETA_locked"] = false,
        ["y"] = $Y2$,
        ["x"] = $X2$,
        ["name"] = "",
        ["formation_template"] = "",
        ["speed_locked"] = true,
      }, -- end of [2]
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
