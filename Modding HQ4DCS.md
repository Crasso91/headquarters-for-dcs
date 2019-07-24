# Headquarters for DCS World modding guide

This guide contains everything you need to mod or add features to HQ4DCS.

**INDEX**

1. The *Library* directory
     1. CommonSettings.ini
          1. The [DistanceToObjective] section
          1. The [EnemyAirDefense] section
          1. The [EnemyAirDefenseDistance] section
          1. The [EnemyCombatAirPatrols] section
     1. The *Coalitions* definitions
     1. The *Theaters* definitions
  1. The *Include* directory
       1. The *Briefing.html* file
       2. The *Jpeg* directory
       3. The *Lua* directory
          1. *Dictionary.lua*
          2. *MapResource.lua*
          3. *Mission.lua*
          4. *Script.lua*
          5. *Warehouses.lua*
          6. The *Mission* subdirectory
          7. The *Script* subdirectory
          8. The *Warehouses* subdirectory
       4. The *Ogg* directory

# The *Library* directory

The *Library* directory contains data about everything required by the mission generator, from units to mission types, stored in Ini files, an old Windows file format which has the dual benefit of being very simple and more human-readable than more modern formats such as Xml (please read [the Wikipedia page on the Ini format](https://en.wikipedia.org/wiki/INI_file) if you want to know more). All HQ4DCS Ini files should use the UTF-8 encoding.

There are two main types of Ini files in HQ4DCS:

- **CommonSettings.ini**: this single Ini file handles many parameters shared by all components of HQ4DCS. It governs the distance at which objectives should be spawned from one another, the amount of enemy air defense to spawn, etc.
- **Other Ini files, stored in subdirectories, also known as "definitions"**: these files, stored in subdirectories of the Library directory, store information about a certain element of the game, such as an unit, a theater or a mission type. Each subdirectory can itself contain as many subdirectories as desired, but filenames must be unique (filenames are used to creatred unique case-insensitive ID for definitions. Duplicates will be ignored, even if files are stored in different subdirectories).

## CommonSettings.ini

This file contains many sections.

### The [DistanceToObjective] section

Stores information about distance between targets.

There are five distance categories, one for each possible value in the *"distance to objective"* setting in the mission template: **VeryLow**, **Low**, **Average**, **High** and **VeryHigh**.

In each category the following values are expected:
- **DistanceFromStartLocation**: Min,max distance between the departure airbase and the first target (in nautical miles)
- **DistanceBetweenObjectives**: Min,max distance between one target and the next (in nautical miles)

**Example:**

```
[DistanceToObjective]
Low.DistanceFromStartLocation=40,60
Low.DistanceBetweenObjectives=10,20

Average.DistanceFromStartLocation=60,90
Average.DistanceBetweenObjectives=15,30
```

### The [EnemyAirDefense] section

Stores information about enemy surface-to-air defense parameters.

There are six "air defense intensity" categories, one for each possible value in the *"enemy air defense"* setting in the mission template: **None**, **VeryLow**, **Low**, **Average**, **High** and **VeryHigh**.

In each category the following values are expected:

- **Embedded.Count:** The min,max number of short-range air defense units to embed in defended vehicle groups (such as APCs, tanks...)
- **Embedded.Families:** Valid unit families for embedded short-range air defense units. Acceptable values are *VehicleAAA*, *VehicleInfantryMANPADS*, *VehicleSAMShort* and *VehicleSAMShortIR*. A value can be included multiple times to increase its chance of spawning. (e.g. *"VehicleAAA,VehicleAAA,VehicleSAMShortIR"* means "spawn a AAA 2 times out of 3 and an IR SAM 1 time out of 3")
- **InArea.*[ShortRange/MediumRange/LongRange]*.GroupCount:** The min,max number of air defense unit groups to spawn in the area around objectives
- **InArea.*[ShortRange/MediumRange/LongRange]*.GroupSize:** The min,max number of units in each unit group. Be aware that some units (such as most MERAD/LORAD SAMs) are spawned as a group - all units are spawned at once. So a min,max value of *"1,1"* should be enough for long-range SAMs as it means "one complete S-300 site", not "one S-300 launcher"
- **InArea.*[ShortRange/MediumRange/LongRange]*.Families:** Valid unit families. Acceptable values are *VehicleAAA*, *VehicleInfantryMANPADS*, *VehicleSAMShort*, *VehicleSAMShortIR*, *VehicleSAMMedium*, *VehicleSAMLong*. Just like with **Embedded.Families**, a value can be included multiple times to increase its chance of spawning

**Example:**

```
[EnemyAirDefense]
Average.Embedded.Chance=60
Average.Embedded.Count=1,3
Average.Embedded.Families=VehicleAAA,VehicleAAA,VehicleAAA,VehicleSAMShort,VehicleSAMShortIR,VehicleSAMShortIR
Average.InArea.ShortRange.GroupCount=2,6
Average.InArea.ShortRange.GroupSize=1,1
Average.InArea.ShortRange.Families=VehicleAAA,VehicleAAA,VehicleInfantryMANPADS,VehicleSAMShortIR,VehicleSAMShortIR,VehicleSAMShort
Average.InArea.MediumRange.GroupCount=0,2
Average.InArea.MediumRange.GroupSize=1,1
Average.InArea.MediumRange.Families=VehicleSAMShort,VehicleSAMShort,VehicleSAMShort,VehicleSAMMedium
Average.InArea.LongRange.GroupCount=0,0
Average.InArea.LongRange.GroupSize=0,0
Average.InArea.LongRange.Families=

High.Embedded.Chance=70
High.Embedded.Count=1,4
High.Embedded.Families=VehicleAAA,VehicleAAA,VehicleSAMShort,VehicleSAMShortIR,VehicleSAMShortIR
High.InArea.ShortRange.GroupCount=2,6
High.InArea.ShortRange.GroupSize=1,1
High.InArea.ShortRange.Families=VehicleAAA,VehicleAAA,VehicleInfantryMANPADS,VehicleSAMShortIR,VehicleSAMShortIR,VehicleSAMShort
High.InArea.MediumRange.GroupCount=1,3
High.InArea.MediumRange.GroupSize=1,1
High.InArea.MediumRange.Families=VehicleSAMShort,VehicleSAMShort,VehicleSAMShort,VehicleSAMMedium
High.InArea.LongRange.GroupCount=0,1
High.InArea.LongRange.GroupSize=1,1
High.InArea.LongRange.Families=VehicleSAMLong

```

### The [EnemyAirDefenseDistance] section

Stores information about where to spawn enemy air defense.

There are five air defense range categories: **ShortRange**, **MediumRange** and **LongRange**.

In each category the following values are expected:

- **DistanceFromTarget**: min,max distance between the air defense system and the mission target(s) (in nautical miles)
- **MinDistanceFromTakeOffLocation**: minimum distance from the player(s) starting location, to make sure noone gets spiked by an S-300 just after taking off.
- **NodeTypes**: on which map nodes (stored in *Theater* definitions) should air defense of this type be spawned. Acceptable values are *LandSmall*, *LandMedium* and *LandLarge*

**Example:**

```
[EnemyAirDefenseDistance]
ShortRange.DistanceFromTarget=0,10
ShortRange.MinDistanceFromTakeOffLocation=15
ShortRange.NodeTypes=LandSmall,LandMedium,LandLarge

MediumRange.DistanceFromTarget=12,24
MediumRange.MinDistanceFromTakeOffLocation=25
MediumRange.NodeTypes=LandMedium,LandLarge
```

### The [EnemyCombatAirPatrols] section

Stores information about the amount (and location) of enemy combat air patrols defending the objective(s). This section contains two global values, and six "multiplier" values.

- **DistanceFromObjective**: how far from the objective(s), in nautical miles, should the CAP flight groups be spawned?
- **MinDistanceFromTakeOffLocation**: minimum distance from the player(s) starting location, in nautical miles.
- **Multiplier.*[None/VeryLow/Low/Average/High/VeryHigh]***: the relative strength of the enemy combat air patrols for the various *"Enemy CAP power"* settings. 0 means no enemy CAP, 100 means "as strong as the allied air force", 200 means "twice as strong", etc. The strength of an air force is the sum of the air-to-air power rating (see *Units* definitions) of all its units.

```
[EnemyCombatAirPatrols]
DistanceFromObjective=0,100
MinDistanceFromTakeOffLocation=100

Multiplier.None=0
Multiplier.VeryLow=25
Multiplier.Low=70
Multiplier.Average=100
Multiplier.High=200
Multiplier.VeryHigh=300
```

## The *Coalitions* definitions

A series of files. Each files contains information about a single HQ4DCS coalition (a group a countries and armies). The name of the file is used as the unique ID of the coalition in HQ4DCS.

- **Name:** The ID of the coalition's name in the language .ini file.
- **NATOCallsigns:** Does this coalition uses NATO callsigns? (*true*/*false*)
- **Countries:** A comma-separated list of the DCS countries belonging to this coalition. During the mission, all units will belong to the first country in the list (the "primary" country). Others are just for show in the briefing and to know which liveries should be used. Acceptable values are: *Abkhazia, Aggressors, Algeria, Australia, Austria, Bahrain, Belarus, Belgium, Brazil, Bulgaria, Canada, Chile, China, Croatia, Cuba, CzechRepublic, Denmark, Egypt, Ethiopia, Finland, France, Georgia, Germany, Greece, Honduras, Hungary, India, Indonesia, Insurgents, Iran, Iraq, Israel, ItalianSocialRepublic, Italy, Japan, Jordan, Kazakhstan, Kuwait, Libya, Malaysia, Mexico, Morocco, NorthKorea, Norway, Oman, Pakistan, Philippines, Poland, Qatar, Romania, Russia, SaudiArabia, Serbia, Slovakia, SouthAfrica, SouthKorea, SouthOsetia, Spain, Sudan, Sweden, Switzerland, Syria, Thailand, TheNetherlands, ThirdReich, Tunisia, Turkey, UK, USA, USSR, Ukraine, UnitedArabEmirates, Venezuela, Vietnam, Yemen* and *Yugoslavia*.
- **Armies:** A comma-separated list of army .ini files the IDs of armies which are part of this coalition.
- **RequiredModules:** An comma-separated list of the names required DCS World modules.
- **TimePeriod:** Min/max decades (comma-separated) during which this coalition can be used. Acceptable values are: *Decade1940*, *Decade1950*, *Decade1960*, *Decade1970*, *Decade1980*, *Decade1990*, *Decade2000* and *Decade2010*

**Example:**
```
[Coalition]
Name=USA
NATOCallsigns=True
Countries=USA
Armies=USA
RequiredModules=
TimePeriod=Decade1960,Decade2010
```

## The *Theaters* definitions

A series of files found in the **Library/Theaters** subdirectory. Each files contains information about a single DCS World theater. The name of the file is used as the unique ID of the theater in HQ4DCS.

- **[Theater]**
  - **DCSID:** The internal ID of the theater in DCS World. Case-sensitive.
  - **DefaultMapCenter:** The default X,Y point on which to center the F10 map.
  - **RequiredModules:** The name of the DCS World module required to fly in this theater. Leave empty if none.

**Example:**
```
[Theater]
DCSID=Caucasus
DefaultMapCenter=-186462.32296594,680206.74315168
RequiredModules=
```

- **[Daytime]**
  - ***[Month]***: The sunrise and sunset time (in minutes since midnight) for each month of the year.

**Example:**
```
[Daytime]
;08:26 - 17:57
January=506,1077
;07:49 - 18:30
February=469,1110
;07:14 - 19:01
March=434,1141
;07:30 - 19:34
April=450,1174
;06:35 - 20:06
May=395,1206
;05:33 - 20:40
June=333,1240
;06:06 - 20:18
July=366,1218
;06:39 - 19:45
August=399,1185
;07:11 - 19:13
September=431,1153
;08:02 - 18:40
October=482,1120
;08:17 - 18:07
November=497,1087
;08:39 - 17:45
December=519,1065
```

- **[Temperature]**
  - ***[Month]***: Min/max temperatures for each month, in °C.

**Example:**
```
[Temperature]
January=-7,5
February=-5,7
March=0,10
April=5,17
May=12,23
June=15,28
July=18,32
August=19,33
September=14,26
October=7,19
November=1,11
December=-5,7
```

- **[Airbases]**
  - ***[AirbaseUniqueID]*.ATC**: The ATC radio frequency (in Mhz).
  - ***[AirbaseUniqueID]*.Coalition**: The coalition this airbase belongs to.
  - ***[AirbaseUniqueID]*.Coordinates**: The map X,Y coordinates of the runway (the point where aircraft are spawned when "takeoff from runway" is selected in the editor).
  - ***[AirbaseUniqueID]*.DCSID**: The internal numerical ID of the airbase in DCS World.
  - ***[AirbaseUniqueID]*.ILS**: A string representation of the ILS frequency, only used in briefings. Empty if none.
  - ***[AirbaseUniqueID]*.Military**: The ATC radio frequency (in Mhz).
  - ***[AirbaseUniqueID]*.Name**: The full name of the airbase (for briefings).
  - ***[AirbaseUniqueID]*.NearWater**: Is the airbase near water (less than 10-15nm)?
  - ***[AirbaseUniqueID]*.ParkingSpots**: The number of parking spots.
  - ***[AirbaseUniqueID]*.Runways**: Comma-separated list of runways.
  - ***[AirbaseUniqueID]*.TACAN**: A string of the TACAN, only used in briefings. Empty if none.

**Example:**
```
[Airbases]
Vaziani.ATC=140.0
Vaziani.Coalition=Blue
Vaziani.Coordinates=-319064.875,903148.53125
Vaziani.DCSID=31
Vaziani.ILS=14, 108.75
Vaziani.Military=true
Vaziani.Name=Vaziani
Vaziani.NearWater=false
Vaziani.ParkingSpots=92
Vaziani.Runways=14,32
Vaziani.TACAN=22X VAS
```

- **[Weather]**
  - **Level[00-10].Clouds.Base**: Min,max base cloud height (in meters).
  - **Level[00-10].Clouds.Density**: Min,max cloud density (0-10).
  - **Level[00-10].Clouds.Precipitation**: Precipitation type (*None*, *Rain* or *Thunderstorm*). Be aware that DCS only allow some values is cloud density if high enough (*Rain* required 5 or more, *Thunderstorm* requires 9 or 10). Invalid values will be ignored by DCS.
  - **Level[00-10].Clouds.Thickness**: Min,max base cloud thickness in meters
  - **Level[00-10].Dust.Enabled**: Are dust storm enabled (true or false). If multiple values can be provided, a random value will be selected (e.g. true,true,false gives 66% chance of duststorm).
  - **Level[00-10].Dust.Density**: Min,max dust storm density, if enabled (0-10).
  - **Level[00-10].Fog.Enabled**: Is fog enabled (*true* or *false*). If multiple values can be provided, a random value will be selected (e.g. *"true,true,false"* gives 66% chance of fog)
  - **Level[00-10].Fog.Thickness**: Min,max fog thickness.
  - **Level[00-10].Fog.Visibility**: Min,max fog visibility in meters.
  - **Level[00-10].QNH**: Min,max atmospheric pressure at mean sea level.
  - **Level[00-10].Turbulence**: Min,max turbulence in meters/second.
  - **Level[00-10].Visibility**: Min,max visibility in meters.

*NOTE:* There are 11 weather quality levels, 00 being the best, 10 the worst.

**Example:**
```
Level02.Clouds.Base=1000,1000
Level02.Clouds.Density=2,2
Level02.Clouds.Precipitation=None
Level02.Clouds.Thickness=300,400
Level02.Dust.Enabled=false
Level02.Dust.Density=0,0
Level02.Fog.Enabled=false
Level02.Fog.Thickness=0,0
Level02.Fog.Visibility=0,0
Level02.QNH=752,762
Level02.Turbulence=2,4
Level02.Visibility=80000,80000
```

- **[Wind]**
  - **Level[00-10].Wind**: Min,max wind speed in meters/second.
  - **Level[00-10].Turbulence**: Min,max turbulence in meters/second.

**Example:**
```
[Wind]
Level05.Wind=10,15
Level05.Turbulence=15,25
```

- **[CarrierPath]**
  - ***[XXXXXX]***: X,Y map coordinates.

A list of X,Y map coordinates carrier groups on this map will follow as navigation path. The name of each waypoint has no effect, but each must be unique.

**Example:**
```
[CarrierPath]
Waypoint000=-42805.371428571,204536.68571429
Waypoint001=-65622.514285714,239675.08571429
Waypoint002=-85245.257142857,288047.42857143
Waypoint003=-116276.57142857,345546.62857143
Waypoint004=-151871.31428571,381141.37142857
Waypoint005=-179251.88571429,430882.74285714
Waypoint006=-208914.17142857,468302.85714286
Waypoint007=-221235.42857143,501159.54285714
Waypoint008=-247703.31428571,534472.57142857
Waypoint009=-266413.37142857,570067.31428571
Waypoint010=-299726.4,588777.37142857
Waypoint011=-340340.91428571,589690.05714286
```

- **[Nodes]**
  - ***[XXXXXX]***: X coordinate, Y coordinate, node type, coalition

A list of points where surface, sea and air units can be spawned. The name of each node has no effect, but each must be unique. As there can (and should) be **MANY** nodes in a theater, format has be made deliberately compact with *four* comma-separated values on each line.
- ***X coordinate*** and ***Y coordinate*** shouldn't require any explanation.
- ***Node type*** determines what kind of unit should be spawned here: *Sea* means open water, *LandSmall* means forest/city streets where small units can be hidden, *LandMedium* means open land areas not suitable for buildings (like the middle of a field), *LandLarge* means open areas where building can be spawned.
- ***Coalition*** is the default coalition of the country where this node is located.

**Example:**
```
[Nodes]
node0835=-310179.49727513,911258.89521983,LandMedium,Blue
node0836=-308093.23564731,910157.16155121,LandMedium,Blue
node0837=-308116.6767892,908211.5467747,LandMedium,Blue
node0838=28021.026938074,277684.97455456,LandMedium,Red
node0839=20698.902003499,277049.72170636,LandMedium,Red
node0840=24744.459615753,263341.6339293,LandMedium,Red
node0841=15884.354101312,269794.46549264,LandMedium,Red
node0842=10835.765676103,271064.97118905,LandMedium,Red
node0843=11638.190326467,277885.58071715,LandMedium,Red
node0844=8094.1481206912,283903.76559489,LandMedium,Red
node0845=6923.9455055764,289219.82890355,LandMedium,Red
node0846=19394.961946657,289487.303787,LandMedium,Red
node0847=18124.456250246,295472.05430431,LandMedium,Red
```
# The *Include* directory

Unlike the *Library* directory, which contains data that will be interpreted by HQ4DCS, the *Include* directory, as its name indicates, contains files that will be included **as is** when the mission is exported to a Miz file. Well, almost. In all text files (Html pages and Lua scripts), you'll find uppercased words encased in dollar signs, such as *$PLAYERCOALITION$* or *$MISSIONNAME$*. These are *"variables"*, which HQ4DCS will replace with values appropriate for the mission.

## The *Briefing.html* file

Briefing.html is an HTML file used as a template for all generated briefings. It has two purposes :

- It provides the minimal HTML5 structure (html, head and body tags, etc.) to ensure the briefing is a valid HTML document.
- It contains a complete CSS stylesheet. Changing it will change the looks of all HQ4DCS briefings.

There are only three variables in Briefing.html. *$LANGUAGEISOCODE$* is the international ISO code for the briefing language (found in the Language definition), *$TITLE$* is the mission name, *$BRIEFING$* is the full briefing in HTML format.

**Example:**

```
<!DOCTYPE html>
<html lang='$LANGUAGEISOCODE$'>
 <head>
  <meta charset='utf-8'>
  <title>$TITLE$</title>
  <style>...</style
 </head>
 <body>
$BRIEFING$
 </body>
</html>
```

## The *Jpeg* subdirectory
As of now, the Jpeg subdirectory contains only one file, Title.jpg, which is used as a decoration on the DCS World briefing screen (HQ4DCS just draws the mission name over it).

## The *Lua* subdirectory

### *Dictionary.lua*
### *MapResource.lua*
### *Mission.lua*
### *Script.lua*
### *Warehouses.lua*
### The *Mission* subdirectory
### The *Script* subdirectory
### The *Warehouses* subdirectory

## The *Ogg* subdirectory
