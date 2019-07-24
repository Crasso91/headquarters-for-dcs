## News

### 21 June 2019
**HQ4DCS Open Beta 2 will be open source!**
You read that right: the next version of HQ4DCS will be open source and hosted on GitHub. The new code (more than 80% of the code from Open Beta 1 has been rewritten from scratch) is faster, leaner, more open, and will allow for easy modding and upgrades. While I'm busy polishing the next version (I will commit the full C# source code to the repository as soon as all basic mission generation features are done), the old deprecated version of the program can still be downloaded from this page.

Although Open Beta 2 is not available yet, you may already to take a peek at the new version's features on the [source code repository](https://github.com/akaAgar/headquarters-for-dcs). The very early (and incomplete) draft of the modding guide will give you an idea of the way .ini files will work. Browsing the Library directory will also help you realize how easy adding or modifying mission types, units, coalitions and theaters will be.

### 25 May 2019
It's been some time without news and an update is long overdue.

First of all, many thanks to all of you who donated to my PayPal or sent me bugfixes and suggestions, it feels good knowing the DCS community is invested in this project.
Progress on the open beta 2 has been going well. Not only are most bugs from open beta 1 now fixed, but I've devised a way to easily implement carrier ops, which should be ready for open beta 3. By the way, future updates should come a LOT faster: most of the work being done now on open beta 2 will ensure a very lean and clean codebase to allow for easy improvement and modifications.

Which brings me to today's subject: the long-awaited .ini file system. No longer is all data hardcoded in the program itself. Everything, from enemy air defense settings to mission types to theaters to coalitions, and much more, is now conveniently stored in .ini files for easy edition and modding.

The ETA for open beta 2 is still early June – in the worst case scenario, it will be available before July.

Thanks again for your support !

### 15 Apr 2019
The cause of the false positive with some antivirus program has been found: it was triggered by the Lua code embedded in HQ4DCS.exe (while the main program is C#, most of the mission generation code is written in Lua). In order to fix this, I'm currently porting most of the mission generation logic to C#, which takes some time.

Now for the good news: the C# code, unsurprisingly, turns out to be a lot faster (mission generation now literally takes half a second) and will be a lot easier to maintain and improve. It also opens new possibilities for the mission generator: instead of loading data about countries, theaters, payloads and units from Lua tables, the new version now uses .ini files, which allow for very easy customization. You've read that right: not only will the next version of HQ4DCS will be a lot faster, leaner and stable, but users will be able to mod it to add new payloads, units or even mission types.

Of course, porting all the code to C# will require a little more work that writing a quick hotfix for the open beta 1, but I'm convinced this is for the better and will make sure further updates come a lot faster.

The next version, known as open beta 2, should be released mid-May, early June.

### 01 Apr 2019
I started working on the first hotfix, which should iron out most of the bugs users have found in Open Beta 1. Once the hotfix is released, I'll gradually add additional features, content and mission types. To make bug reports easier, and to help you get a better idea of what is done an what's left to do, a Trello development board has been added on this page. Don't hesitate to send feedback, bug reports and suggestions if you notice something missing.

## Downloads

- **[HQ4DCS Prototype 1](http://www.cafedefaune.org/uploads/hq4dcs/HQ4DCS_Prototype1_Deprecated.zip)** (4 Mb): a **deprecated** version of HQ4DCS, the prototype 1 (formerly known as "open beta 1") is still available until the new version is complete.

## Features

- Generate single-player or cooperative air-to-air or strike missions.
- Create complete missions with briefings and waypoints, then export them to a DCS World .MIZ file in just a few clicks.
- Can create almost any kind of scenarios, from a deep strike mission behind a superpower's cutting-edge integrated air defense network to a photo reconnaissance mission over a terrorist training camp.
- No units spawned through runtime scripting. All units are added to the mission itself, so they can be edited with DCS World's mission editor for further customization.
- Save mission templates to small .lua files and share them with your friends.
- Choose from a large variety of mission types, from combat air patrols to bomber interception, target designation for artillery strikes, photo reconnaissance, bomb damage assessment and many others...
- Search and designate ground targets by yourself or use HQ4DCS's complete JTAC system which can provide coordinates of enemy units, mark targets with smoke and lase them, whether they're static or moving.
- Customize SAM and air-to-air opposition for any mission difficulty.
- Generates proper friendly/enemy units according to country and time period.

## Changelog

### Open Beta 2 (_coming very soon_)
- **HQ4DCS is now an open source project**, released under the GNU v3 licence
- **All new "open definition library".** All data used by HQ4DCS is now stored in external .ini files, allowing for customization of aircraft, theaters, mission types and more
- **80% of the code has been rewritten. Almost all Lua code has been ported to C#.** Mission generation is now a matter of milliseconds instead of seconds, code will be easier to maintain, future update should come a lot faster.
- Briefing is now available on kneeboards (on a single page for the moment, multi-page briefings will be available in a future version)
- All new user interface
- Added an option to print briefings
- Added distinct "wind" and "cloud cover" parameters instead of a single "weather" settings
- Added option to input custom mission name and/or briefing description instead of the randomly generated ones.
- Improved radio messages so both the pilot request and the answer can be heard
- Fixed problem in embedded Lua files triggering a false positive on some antivirus software
- Fixed bug where CAP/SEAD escort aircraft sometimes go to the edge of the map
- Minor fixes to many player aircraft: F-5 using AI aircraft, Su-25TM listed as a controllable aircraft...

### Prototype 1, aka Open Beta 1 (19 March 2019)
- First public version

