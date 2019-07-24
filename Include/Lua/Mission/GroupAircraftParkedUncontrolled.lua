[$INDEX$] =
{
  ["modulation"] = 0,
  ["tasks"] =
  {
  }, -- end of ["tasks"]
  ["task"] = "CAS",
  ["uncontrolled"] = true,
  ["route"] =
  {
    ["points"] =
    {
      [1] =
      {
        ["alt"] = 45,
        ["action"] = "From Parking Area",
        ["alt_type"] = "BARO",
        ["speed"] = 138.88888888889,
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
                ["key"] = "CAS",
                ["id"] = "EngageTargets",
                ["number"] = 1,
                ["auto"] = true,
                ["params"] =
                {
                  ["targetTypes"] =
                  {
                    [1] = "Helicopters",
                    [2] = "Ground Units",
                    [3] = "Light armed ships",
                  }, -- end of ["targetTypes"]
                  ["priority"] = 0,
                }, -- end of ["params"]
              }, -- end of [1]
            }, -- end of ["tasks"]
          }, -- end of ["params"]
        }, -- end of ["task"]
        ["type"] = "TakeOffParking",
        ["ETA"] = 0,
        ["ETA_locked"] = true,
        ["y"] = $Y$,
        ["x"] = $X$,
        ["name"] = "",
        ["formation_template"] = "",
        ["airdromeId"] = $AIRDROMEID$,
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
  ["frequency"] = 124,
}, -- end of [$INDEX$]
