[$INDEX$] = 
{
    ["modulation"] = 0,
    ["tasks"] = 
    {
    }, -- end of ["tasks"]
    ["task"] = "Ground Attack",
    ["uncontrolled"] = false,
    ["taskSelected"] = true,
    ["route"] = 
    {
        ["points"] = 
        {
            [1] = 
            {
                ["alt"] = 13,
                ["action"] = "$TAKEOFFACTION$",
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
                                ["auto"] = true,
                                ["id"] = "WrappedAction",
                                ["number"] = 1,
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
                            }, -- end of [1]
                        }, -- end of ["tasks"]
                    }, -- end of ["params"]
                }, -- end of ["task"]
                ["type"] = "$TAKEOFFTYPE$",
                ["ETA"] = 0,
                ["ETA_locked"] = true,
                ["y"] = $Y$,
                ["x"] = $X$,
                ["name"] = "INITPOS",
                ["formation_template"] = "",
                ["airdromeId"] = $AIRDROMEID$,
--                ["linkUnit"] = $LINKUNIT$,
                ["speed_locked"] = true,
            }, -- end of [1]
$WAYPOINTS$
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
}, -- end of [1]
