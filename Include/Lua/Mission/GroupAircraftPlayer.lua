                            [$INDEX$] = 
                            {
                                ["modulation"] = 0,
                                ["tasks"] = 
                                {
                                }, -- end of ["tasks"]
                                ["task"] = "CAS",
                                ["uncontrolled"] = false,
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
                                            ["type"] = "$TAKEOFFTYPE$",
                                            ["ETA"] = 0,
                                            ["ETA_locked"] = true,
                                            ["y"] = $Y$,
                                            ["x"] = $X$,
                                            ["name"] = "INITPOS",
                                            ["formation_template"] = "",
                                            ["airdromeId"] = $AIRDROMEID$,
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
                            }, -- end of [$INDEX$]