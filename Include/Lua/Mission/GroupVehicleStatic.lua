[$INDEX$] =
{
  ["visible"] = false,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["uncontrollable"] = false,
  ["task"] = "Ground Nothing",
  ["route"] =
  {
    ["spans"] =
    {
    }, -- end of ["spans"]
    ["points"] =
    {
      [1] =
      {
        ["alt"] = 8,
        ["type"] = "Turning Point",
        ["ETA"] = 0,
        ["alt_type"] = "BARO",
        ["formation_template"] = "",
        ["y"] = $Y$,
        ["x"] = $X$,
        ["name"] = "",
        ["ETA_locked"] = true,
        ["speed"] = 5.5555555555556,
        ["action"] = "Off Road",
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
                ["id"] = "WrappedAction",
                ["number"] = 3,
                ["params"] =
                {
                  ["action"] =
                  {
                    ["id"] = "Option",
                    ["params"] =
                    {
                      ["name"] = 0,
                      ["value"] = 4,
                    }, -- end of ["params"]
                  }, -- end of ["action"]
                }, -- end of ["params"]
              }, -- end of [3]
            }, -- end of ["tasks"]
          }, -- end of ["params"]
        }, -- end of ["task"]
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
  ["start_time"] = 0,
}, -- end of [$INDEX$]
