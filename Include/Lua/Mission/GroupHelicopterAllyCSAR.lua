[$INDEX$] =
{
  ["lateActivation"] = true,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["task"] = "Transport",
  ["uncontrolled"] = false,
  ["taskSelected"] = true,
  ["route"] =
  {
    ["routeRelativeTOT"] = true,
    ["points"] =
    {
      [1] =
      {
        ["alt"] = 457.2,
        ["action"] = "Turning Point",
        ["alt_type"] = "RADIO",
        ["speed"] = 46.25,
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
                ["id"] = "WrappedAction",
                ["number"] = 1,
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
              }, -- end of [1]
              [2] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "WrappedAction",
                ["number"] = 2,
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
              }, -- end of [2]
              [3] =
              {
                ["enabled"] = true,
                ["auto"] = false,
                ["id"] = "Orbit",
                ["number"] = 3,
                ["params"] =
                {
                  ["altitude"] = 457.2,
                  ["pattern"] = "Circle",
                  ["speed"] = 41.666666666667,
                  ["altitudeEdited"] = true,
                }, -- end of ["params"]
              }, -- end of [3]
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
  ["hidden"] = false,
  ["units"] =
  {
    $UNITS$
  }, -- end of ["units"]
  ["y"] = $Y$,
  ["x"] = $X$,
  ["name"] = "$NAME$",
  ["communication"] = true,
  ["start_time"] = 0,
  ["modulation"] = 0,
  ["frequency"] = $FREQUENCY$,
}, -- end of [$INDEX$]