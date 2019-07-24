            [1] = 
            {
                ["alt"] = 13,
                ["action"] = "$TAKEOFFACTION$",
                ["alt_type"] = "BARO",
                ["speed"] = 130,
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
                ["speed_locked"] = true,
            }, -- end of [1]
