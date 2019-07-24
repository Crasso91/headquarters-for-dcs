mission = 
{
    ["requiredModules"] = 
    {
    }, -- end of ["requiredModules"]
    ["date"] = 
    {
        ["Year"] = $DATEYEAR$,
        ["Day"] = $DATEDAY$,
        ["Month"] = $DATEMONTH$,
    }, -- end of ["date"]
    ["trig"] = 
    {
        ["actions"] = 
        {
            [1] = "a_do_script_file(getValueResourceByKey(\"ResKey_Script\"));",
        }, -- end of ["actions"]
        ["events"] = 
        {
        }, -- end of ["events"]
        ["custom"] = 
        {
        }, -- end of ["custom"]
        ["func"] = 
        {
        }, -- end of ["func"]
        ["flag"] = 
        {
            [1] = true,
        }, -- end of ["flag"]
        ["conditions"] = 
        {
            [1] = "return(true)",
        }, -- end of ["conditions"]
        ["customStartup"] = 
        {
        }, -- end of ["customStartup"]
        ["funcStartup"] = 
        {
            [1] = "if mission.trig.conditions[1]() then mission.trig.actions[1]() end",
        }, -- end of ["funcStartup"]
    }, -- end of ["trig"]
    ["result"] = 
    {
        ["offline"] = 
        {
            ["conditions"] = 
            {
            }, -- end of ["conditions"]
            ["actions"] = 
            {
            }, -- end of ["actions"]
            ["func"] = 
            {
            }, -- end of ["func"]
        }, -- end of ["offline"]
        ["total"] = 0,
        ["blue"] = 
        {
            ["conditions"] = 
            {
            }, -- end of ["conditions"]
            ["actions"] = 
            {
            }, -- end of ["actions"]
            ["func"] = 
            {
            }, -- end of ["func"]
        }, -- end of ["blue"]
        ["red"] = 
        {
            ["conditions"] = 
            {
            }, -- end of ["conditions"]
            ["actions"] = 
            {
            }, -- end of ["actions"]
            ["func"] = 
            {
            }, -- end of ["func"]
        }, -- end of ["red"]
    }, -- end of ["result"]
    ["maxDictId"] = 0,
    ["pictureFileNameN"] = 
    {
    }, -- end of ["pictureFileNameN"]
    ["groundControl"] = 
    {
        ["isPilotControlVehicles"] = false,
        ["roles"] = 
        {
            ["artillery_commander"] = 
            {
                ["neutrals"] = 0,
                ["blue"] = 0,
                ["red"] = 0,
            }, -- end of ["artillery_commander"]
            ["instructor"] = 
            {
                ["neutrals"] = 0,
                ["blue"] = 0,
                ["red"] = 0,
            }, -- end of ["instructor"]
            ["observer"] = 
            {
                ["neutrals"] = 0,
                ["blue"] = 0,
                ["red"] = 0,
            }, -- end of ["observer"]
            ["forward_observer"] = 
            {
                ["neutrals"] = 0,
                ["blue"] = 0,
                ["red"] = 0,
            }, -- end of ["forward_observer"]
        }, -- end of ["roles"]
    }, -- end of ["groundControl"]
    ["goals"] = 
    {
    }, -- end of ["goals"]
    ["weather"] = 
    {
        ["atmosphere_type"] = 0,
        ["groundTurbulence"] = $WEATHERGROUNDTURBULENCE$,
        ["enable_fog"] = $WEATHERFOGENABLED$,
        ["wind"] = 
        {
            ["at8000"] = 
            {
                ["speed"] = $WEATHERWIND3$,
                ["dir"] = $WEATHERWIND3DIR$,
            }, -- end of ["at8000"]
            ["atGround"] = 
            {
                ["speed"] = $WEATHERWIND1$,
                ["dir"] = $WEATHERWIND1DIR$,
            }, -- end of ["atGround"]
            ["at2000"] = 
            {
                ["speed"] = $WEATHERWIND2$,
                ["dir"] = $WEATHERWIND2DIR$,
            }, -- end of ["at2000"]
        }, -- end of ["wind"]
        ["season"] = 
        {
            ["temperature"] = $WEATHERTEMPERATURE$,
        }, -- end of ["season"]
        ["type_weather"] = 0,
        ["qnh"] = $WEATHERQNH$,
        ["cyclones"] = 
        {
        }, -- end of ["cyclones"]
        ["name"] = "Weather",
        ["fog"] = 
        {
            ["thickness"] = $WEATHERFOGTHICKNESS$,
            ["visibility"] = $WEATHERFOGVISIBILITY$,
        }, -- end of ["fog"]
        ["visibility"] = 
        {
            ["distance"] = $WEATHERVISIBILITYDISTANCE$,
        }, -- end of ["visibility"]
        ["dust_density"] = $WEATHERDUSTDENSITY$,
        ["enable_dust"] = $WEATHERDUSTENABLED$,
        ["clouds"] = 
        {
            ["thickness"] = $WEATHERCLOUDSTHICKNESS$,
            ["density"] = $WEATHERCLOUDSDENSITY$,
            ["base"] = $WEATHERCLOUDSBASE$,
            ["iprecptns"] = $WEATHERCLOUDSPRECIPITATION$,
        }, -- end of ["clouds"]
    }, -- end of ["weather"]
    ["theatre"] = "$THEATERID$",
    ["triggers"] = 
    {
        ["zones"] = 
        {
        }, -- end of ["zones"]
    }, -- end of ["triggers"]
    ["map"] = 
    {
        ["centerY"] = $CENTERY$,
        ["zoom"] = 512000,
        ["centerX"] = $CENTERX$,
    }, -- end of ["map"]
    ["coalitions"] = 
    {
        ["blue"] = 
        {
$COUNTRIESBLUE$
        }, -- end of ["blue"]
        ["neutrals"] = 
        {
        }, -- end of ["neutrals"]
        ["red"] = 
        {
$COUNTRIESRED$
        }, -- end of ["red"]
    }, -- end of ["coalitions"]
    ["descriptionText"] = "$BRIEFINGDESCRIPTION$",
    ["pictureFileNameR"] = 
    {
$BRIEFINGPICTURESRED$
    }, -- end of ["pictureFileNameR"]
    ["descriptionNeutralsTask"] = "",
    ["descriptionBlueTask"] = "$BRIEFINGBLUE$",
    ["descriptionRedTask"] = "$BRIEFINGRED$",
    ["pictureFileNameB"] = 
    {
$BRIEFINGPICTURESBLUE$
    }, -- end of ["pictureFileNameB"]
    ["coalition"] = 
    {
        ["blue"] = 
        {
            ["bullseye"] = 
            {
                ["y"] = $BULLSEYEREDY$,
                ["x"] = $BULLSEYEREDX$,
            }, -- end of ["bullseye"]
            ["nav_points"] = 
            {
            }, -- end of ["nav_points"]
            ["name"] = "blue",
            ["country"] = 
            {
$COALITIONBLUE$
            }, -- end of ["country"]
        }, -- end of ["blue"]
        ["red"] = 
        {
            ["bullseye"] = 
            {
                ["y"] = $BULLSEYEBLUEY$,
                ["x"] = $BULLSEYEBLUEX$,
            }, -- end of ["bullseye"]
            ["nav_points"] = 
            {
            }, -- end of ["nav_points"]
            ["name"] = "red",
            ["country"] = 
            {
$COALITIONRED$
            }, -- end of ["country"]
        }, -- end of ["red"]
    }, -- end of ["coalition"]
    ["sortie"] = "$MISSIONNAME$",
    ["version"] = 16,
    ["trigrules"] = 
    {
        [1] = 
        {
            ["rules"] = 
            {
            }, -- end of ["rules"]
            ["eventlist"] = "",
            ["predicate"] = "triggerStart",
            ["actions"] = 
            {
                [1] = 
                {
                    ["density"] = 1,
                    ["zone"] = "",
                    ["preset"] = 1,
                    ["file"] = "ResKey_Script",
                    ["predicate"] = "a_do_script_file",
                    ["ai_task"] = 
                    {
                        [1] = "",
                        [2] = "",
                    }, -- end of ["ai_task"]
                }, -- end of [1]
            }, -- end of ["actions"]
            ["comment"] = "Run main mission script",
        }, -- end of [1]
    }, -- end of ["trigrules"]
    ["currentKey"] = 0,
    ["start_time"] = $MISSIONTIME$,
    ["forcedOptions"] = 
    {
        ["userMarks"] = true, -- required for some scripts
        $FAILUREENABLED$
    }, -- end of ["forcedOptions"]
    ["failures"] = 
    {
    }, -- end of ["failures"]
} -- end of mission
