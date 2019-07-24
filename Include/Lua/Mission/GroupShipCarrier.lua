[$INDEX$] = 
{
    ["visible"] = false,
    ["tasks"] = 
    {
    }, -- end of ["tasks"]
    ["uncontrollable"] = false,
    ["route"] = 
    {
        ["points"] = 
        {
            [1] = 
            {
                ["alt"] = 0,
                ["type"] = "Turning Point",
                ["ETA"] = 0,
                ["alt_type"] = "BARO",
                ["formation_template"] = "",
                ["y"] = $Y$,
                ["x"] = $X$,
                ["name"] = "",
                ["ETA_locked"] = true,
                ["speed"] = 2.5,
                ["action"] = "Turning Point",
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
    ["id"] = "WrappedAction",
    ["number"] = 1,
    ["params"] = 
    {
        ["action"] = 
        {
            ["id"] = "ActivateBeacon",
            ["params"] = 
            {
                ["type"] = 4,
                ["AA"] = false,
                ["unitId"] = $UNITID$,
                ["modeChannel"] = "X",
                ["channel"] = 10,
                ["system"] = 3,
                ["callsign"] = "TKR",
                ["bearing"] = true,
                ["frequency"] = 971000000,
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
            ["id"] = "Option",
            ["params"] = 
            {
                ["value"] = 4,
                ["name"] = 0,
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
            ["id"] = "SetImmortal",
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
            ["id"] = "SetInvisible",
            ["params"] = 
            {
                ["value"] = true,
            }, -- end of ["params"]
        }, -- end of ["action"]
    }, -- end of ["params"]
}, -- end of [4]
                        }, -- end of ["tasks"]
                    }, -- end of ["params"]
                }, -- end of ["task"]
                ["speed_locked"] = true,
            }, -- end of [1]
            [2] = 
            {
                ["alt"] = 0,
                ["type"] = "Turning Point",
                ["ETA"] = 0.0,
                ["alt_type"] = "BARO",
                ["formation_template"] = "",
                ["y"] = 0.0,
                ["x"] = 0.0,
                ["name"] = "",
                ["ETA_locked"] = false,
                ["speed"] = 2.5,
                ["action"] = "Turning Point",
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
    ["start_time"] = 0,
}, -- end of [$INDEX$]
