# ZorkSharp TODO - Missing Features & Content

This document tracks what's missing from the current ZorkSharp implementation compared to the original Zork I. Contributions are welcome!

## 📊 Current Implementation Status

### Rooms
- ✅ **Implemented**: 25 rooms
- ❌ **Missing**: ~85 rooms from the original Zork I
- 📈 **Completion**: ~23%

### Objects
- ✅ **Implemented**: 28 objects
- ❌ **Missing**: ~94 objects from the original Zork I
- 📈 **Completion**: ~23%

### Commands
- ✅ **Implemented**: 9 basic commands
- ❌ **Missing**: ~258 syntax patterns from original
- 📈 **Completion**: ~3%

---

## 🏗️ Missing World Content

### Missing Rooms (Priority Areas)

#### White House Area
- [ ] Attic (exists but needs full implementation with rope mechanics)
- [ ] Behind Kitchen (chimney access)
- [ ] Studio (with painting puzzle)

#### Dungeon - Main Areas
- [ ] Maze (complete the full 8-room maze system)
- [ ] Grating Room (entrance to dungeon from forest)
- [ ] Clearing with Grating
- [ ] Canyon Bottom
- [ ] Rocky Ledge
- [ ] Canyon View
- [ ] Shaft Room
- [ ] Smelly Room
- [ ] Gas Room

#### Dungeon - Treasury & Complex
- [ ] Treasure Room (trophy case destination)
- [ ] Cyclops Room
- [ ] Strange Passage
- [ ] Thief's Hideout
- [ ] Treasure Repository

#### River & Water Areas
- [ ] Stream (north and south sections)
- [ ] Dam
- [ ] Dam Lobby
- [ ] Maintenance Room
- [ ] Dam Base
- [ ] Frigid River (multiple sections)
- [ ] Shore
- [ ] Sandy Beach
- [ ] Sandy Cave
- [ ] Aragain Falls
- [ ] Rainbow Room

#### Mine Complex
- [ ] Shaft (multiple levels)
- [ ] Machine Room
- [ ] Timber Room
- [ ] Drafty Room
- [ ] Coal Mine (multiple sections)
- [ ] Ladder Top
- [ ] Ladder Bottom
- [ ] Squeaky Room
- [ ] Bat Room

#### Temple & Religious Areas
- [ ] Temple (needs full implementation with bell/book/candle puzzle)
- [ ] Altar (needs full implementation)
- [ ] Forest Temple
- [ ] Egyptian Room
- [ ] Torch Room (exists but needs full puzzle implementation)

#### Cave System
- [ ] Twisty Passages (complete all 8 maze rooms)
- [ ] Slide Room
- [ ] Cellar (exists but needs trap door puzzle)
- [ ] Mirror Rooms (north and south)
- [ ] Winding Passage
- [ ] Grotto
- [ ] Narrow Crawlway

#### Special Areas
- [ ] Volcano
- [ ] Volcano Bottom
- [ ] Volcano Core
- [ ] Dome Room (exists but needs full balloon puzzle)
- [ ] Land of the Dead

### Missing Objects

#### Treasures (High Priority - Core Gameplay)
- [ ] Jade Figurine (treasure)
- [ ] Platinum Bar (treasure)
- [ ] Pot of Gold (treasure)
- [ ] Scepter (treasure)
- [ ] Bauble (tree ornament treasure)
- [ ] Diamond (treasure)
- [ ] Ivory Torch (treasure)
- [ ] Scarab (treasure)
- [ ] Emerald (large, treasure)
- [ ] Clockwork Canary (treasure)

#### Tools & Equipment
- [ ] Rusty Knife
- [ ] Screwdriver
- [ ] Wrench
- [ ] Hammer
- [ ] Shovel
- [ ] Axe
- [ ] Inflatable Boat
- [ ] Pump
- [ ] Guidebook
- [ ] Map
- [ ] Labels (for valve)
- [ ] Fuel (for torch)
- [ ] Coal
- [ ] Timber
- [ ] Dynamite
- [ ] Buoy

#### Containers & Openable Objects
- [ ] Safe
- [ ] Coffin
- [ ] Basket (lowered from dome)
- [ ] Bag of Coins (different from pile of coins)
- [ ] Toolbox
- [ ] Case (not trophy case)

#### Keys & Access Items
- [ ] Skeleton Key
- [ ] Master Key
- [ ] Rusty Key
- [ ] Brass Bell
- [ ] Black Book
- [ ] Purple Book

#### Food & Consumables
- [ ] Cake
- [ ] Bread
- [ ] Cheese
- [ ] Pie
- [ ] Garlic Clove (have some but need full implementation)
- [ ] Mushroom
- [ ] Berries

#### Light Sources
- [ ] Candles (individual, not candlestick)
- [ ] Ivory Torch (treasure + light source)
- [ ] Torch (exists but needs fuel mechanics)

#### Special/Puzzle Items
- [ ] Prayer Book
- [ ] Hymnal
- [ ] Leaflet (exists but needs full "Welcome to Zork" text)
- [ ] Guidebook (dungeon master's guide)
- [ ] Postcard
- [ ] Advertisement
- [ ] Newspaper
- [ ] Magazine (US News & Dungeon Report)
- [ ] Matchbook (exists but needs puzzle mechanics)
- [ ] Button (various buttons)
- [ ] Lever
- [ ] Switch
- [ ] Dial

#### Fixed/Scenery Objects (Interactive)
- [ ] Mailbox (exists but needs full open/close mechanics)
- [ ] Trap Door (needs full puzzle implementation)
- [ ] Grating (entrance to dungeon)
- [ ] Door (various doors throughout)
- [ ] Gate
- [ ] Bolt
- [ ] Bars (prison bars)
- [ ] Machine (in machine room)
- [ ] Basket (rope + basket puzzle)
- [ ] Railing
- [ ] Ladder
- [ ] Slide
- [ ] Stairs (various)
- [ ] Chimney
- [ ] Chute
- [ ] Dam
- [ ] Control Panel
- [ ] Reservoir Control
- [ ] Valve
- [ ] Pipe
- [ ] Gauges

#### NPCs & Creatures
- [ ] Thief (major NPC with AI)
- [ ] Troll (combat encounter)
- [ ] Cyclops (puzzle/combat)
- [ ] Grue (darkness threat)
- [ ] Bat (random encounter)
- [ ] Ghost (special encounter)
- [ ] Spirits (in temple)

#### Environmental Objects
- [ ] Trees (forest)
- [ ] Leaves
- [ ] Sand
- [ ] Pile of Plastic
- [ ] Pile of Bones
- [ ] Skeleton
- [ ] Debris
- [ ] Pile of Coal
- [ ] Rainbow
- [ ] Clouds
- [ ] Rocks
- [ ] Boulder

---

## 🎮 Missing Gameplay Features

### Core Command System

#### Movement Commands (Expanded)
- [ ] **CLIMB** (rope, trees, ladder)
- [ ] **SWIM** (river, reservoir)
- [ ] **BOARD** / **ENTER** (boat)
- [ ] **CROSS** (bridge, river)
- [ ] **JUMP** (over chasms, down)
- [ ] **CRAWL** (through small passages)
- [ ] **SLIDE** (down slide)
- [ ] **LAUNCH** (boat)
- [ ] **DISEMBARK** / **EXIT** (from boat)

#### Object Manipulation Commands
- [ ] **OPEN** (containers, doors, grating) - basic exists but needs full implementation
- [ ] **CLOSE** (containers, doors)
- [ ] **LOCK** / **UNLOCK** (with keys)
- [ ] **TURN ON** / **TURN OFF** (lamp, lights)
- [ ] **LIGHT** (candles, torch)
- [ ] **EXTINGUISH** (flames)
- [ ] **TIE** (rope to railing)
- [ ] **UNTIE** (rope)
- [ ] **RAISE** / **LOWER** (basket)
- [ ] **PUSH** / **PULL** (objects, levers)
- [ ] **MOVE** (objects to reveal hidden items)
- [ ] **SEARCH** (objects and locations)
- [ ] **LOOK UNDER** / **LOOK BEHIND** (objects)
- [ ] **LOOK IN** / **LOOK INSIDE** (containers)
- [ ] **PUT IN** / **PUT ON** (placing objects)
- [ ] **THROW** (objects at targets)
- [ ] **BREAK** (objects, mirrors)
- [ ] **CUT** (with knife or sword)
- [ ] **DIG** (with shovel)
- [ ] **POUR** (water, liquids)
- [ ] **FILL** (bottle from reservoir)
- [ ] **EMPTY** (containers)
- [ ] **INFLATE** / **DEFLATE** (boat)
- [ ] **PUMP** (inflate boat)

#### Reading & Information
- [ ] **READ** (leaflet, books, engravings, signs) - basic exists
- [ ] **EXAMINE** (objects) - basic exists but needs expansion
- [ ] **COUNT** (objects)
- [ ] **DIAGNOSE** (health status)

#### Communication & Sound
- [ ] **RING** (bell)
- [ ] **PRAY** (in temple)
- [ ] **CHANT** (prayers)
- [ ] **SAY** (words, phrases)
- [ ] **YELL** / **SCREAM** / **SHOUT**
- [ ] **LISTEN** (to sounds)
- [ ] **KNOCK** (on doors)

#### Combat & Interaction
- [ ] **ATTACK** / **KILL** / **FIGHT** (creatures with weapons)
- [ ] **WAVE** (sword, items)
- [ ] **SWING** (sword)
- [ ] **THROW AT** (throwing objects at NPCs)
- [ ] **GIVE** / **OFFER** (items to NPCs)
- [ ] **BRIBE** (NPCs with treasures)

#### Senses & Perception
- [ ] **SMELL** (objects, environment)
- [ ] **TASTE** (objects)
- [ ] **TOUCH** / **FEEL** (objects)
- [ ] **RUB** (lamp, objects)

#### Magic & Special
- [ ] **EXORCISE** (ghosts)
- [ ] **ODYSSEUS** (Cyclops puzzle)
- [ ] **ULYSSES** (alternative Cyclops solution)
- [ ] Spell system (if implementing magic)

#### Food & Drink
- [ ] **EAT** (food items)
- [ ] **DRINK** (water, potions)

#### Game Meta Commands
- [ ] **SAVE** (save game state)
- [ ] **RESTORE** (load saved game)
- [ ] **RESTART** (start new game)
- [ ] **VERBOSE** (always show full room descriptions)
- [ ] **BRIEF** (show short room descriptions)
- [ ] **SUPERBRIEF** (minimal descriptions)
- [ ] **WAIT** / **Z** (pass time)
- [ ] **AGAIN** / **G** (repeat last command)
- [ ] **OOPS** (correct last word typed)
- [ ] **VERSION** (show game version)
- [ ] **SCRIPT** (start transcript)
- [ ] **UNSCRIPT** (end transcript)

### Combat System
- [ ] **NPC Combat Engine** (Thief, Troll, Cyclops)
  - [ ] Health/damage system
  - [ ] Weapon effectiveness
  - [ ] Combat turns
  - [ ] NPC AI behavior
  - [ ] Death and respawning

- [ ] **Weapons System**
  - [ ] Sword effectiveness
  - [ ] Knife as weapon
  - [ ] Axe as weapon
  - [ ] Trident as weapon
  - [ ] Bare hands (last resort)

### NPC AI & Behavior
- [ ] **Thief Character**
  - [ ] Random movement through dungeon
  - [ ] Steals treasures from player
  - [ ] Combat with player
  - [ ] Drops random items
  - [ ] Hideout location
  - [ ] Death triggers treasure drop

- [ ] **Troll Character**
  - [ ] Guards specific passage
  - [ ] Combat behavior
  - [ ] Axe dropping on death
  - [ ] Blocks passage until defeated

- [ ] **Cyclops Character**
  - [ ] Name puzzle (Odysseus/Ulysses)
  - [ ] Opens treasury path
  - [ ] Alternative: kill Cyclops

- [ ] **Grue (Darkness Threat)**
  - [ ] Warning messages in darkness
  - [ ] Death after too long in dark
  - [ ] Cannot be fought

### Puzzle Systems

#### Major Puzzles
- [ ] **Trap Door Puzzle** (rug reveals trap door, needs key)
- [ ] **Mailbox Puzzle** (contains leaflet)
- [ ] **Trophy Case** (deposit treasures for points)
- [ ] **Dam Puzzle** (flood control, reveals passage)
- [ ] **Rainbow Puzzle** (cross rainbow, pot of gold)
- [ ] **Coffin Puzzle** (contains scarab)
- [ ] **Bell/Book/Candle Puzzle** (exorcism in temple)
- [ ] **Mirror Puzzle** (mirror room reflections)
- [ ] **Cyclops Puzzle** (name puzzle)
- [ ] **Thief Puzzle** (defeat or evade)
- [ ] **Troll Puzzle** (defeat or bribe)
- [ ] **Dome Puzzle** (lower basket, send items up)
- [ ] **Maze Solutions** (twisty passages mapping)
- [ ] **Grating Puzzle** (unlock to enter dungeon)
- [ ] **Chimney Puzzle** (become small enough to enter)
- [ ] **Coal Mine Puzzle** (navigate darkness with light)
- [ ] **Safe Puzzle** (combination or tools to open)
- [ ] **Volcano Puzzle** (timing and navigation)
- [ ] **Boat Puzzle** (inflate and navigate river)
- [ ] **Basket/Rope Puzzle** (send items to ledge)
- [ ] **Engravings Puzzle** (clues in ancient text)

#### Interactive Object Puzzles
- [ ] Objects hidden under other objects
- [ ] Objects behind moved objects
- [ ] Keys unlock specific doors
- [ ] Containers with locked contents
- [ ] Combination locks
- [ ] Tools needed to open things (wrench, screwdriver)

### Game State & Persistence
- [ ] **Save/Load System**
  - [ ] Save game to file
  - [ ] Load saved game
  - [ ] Multiple save slots
  - [ ] Auto-save on quit

- [ ] **Game State Tracking**
  - [ ] Visited rooms tracking
  - [ ] Puzzle completion flags
  - [ ] NPC state persistence
  - [ ] Time-based events
  - [ ] Global game flags

### Scoring System (Expanded)
- [ ] **Treasure Scoring** (currently basic)
  - [ ] Points for finding treasures: 50-60 points
  - [ ] Points for placing in trophy case: 290 points total
  - [ ] Individual treasure values

- [ ] **Achievement Scoring**
  - [ ] Defeating Thief: 5 points
  - [ ] Defeating Troll: 4 points
  - [ ] Solving major puzzles: various points
  - [ ] Finding secret areas: various points

- [ ] **Rank System**
  - [ ] Beginner (0-50)
  - [ ] Novice Adventurer (50-100)
  - [ ] Junior Adventurer (100-200)
  - [ ] Adventurer (200-300)
  - [ ] Master Adventurer (300-350)

### Light & Darkness System (Enhanced)
- [x] Basic darkness detection (implemented)
- [ ] **Lamp Mechanics**
  - [ ] Lamp battery life (limited turns)
  - [ ] Lamp on/off state
  - [ ] Warning messages when battery low
  - [ ] Lamp dies permanently after X turns

- [ ] **Alternative Light Sources**
  - [ ] Torch (limited duration)
  - [ ] Candles (consumable)
  - [ ] Matches (light candles/torch)

- [ ] **Darkness Threat**
  - [ ] Grue warning messages
  - [ ] Death by Grue after turns in darkness
  - [ ] Safe dark rooms (some rooms safe without light)

### Inventory Management (Enhanced)
- [x] Basic weight limit (implemented)
- [ ] **Item Limits**
  - [ ] Maximum items carried (not just weight)
  - [ ] Some items take two hands
  - [ ] Items too large to carry

- [ ] **Container Mechanics**
  - [ ] Items in containers count toward weight
  - [ ] View contents of containers
  - [ ] Nested containers

### Time & Event System
- [ ] **Turn Counter**
  - [ ] Track number of moves
  - [ ] Display move count

- [ ] **Timed Events**
  - [x] Basic clock system (implemented)
  - [ ] Lamp battery depletion
  - [ ] Torch burning out
  - [ ] Candle melting
  - [ ] Thief random appearances
  - [ ] Dam flood timing
  - [ ] Volcano eruption cycle

- [ ] **Scheduled Events**
  - [ ] Events triggered after N turns
  - [ ] Events triggered by player location
  - [ ] One-time events vs. repeating

### Advanced Parser Features
- [ ] **Pronoun Support**
  - [ ] IT, THEM (refer to last noun)
  - [ ] ALL, EVERYTHING (take all)
  - [ ] Context-aware parsing

- [ ] **Multi-object Commands**
  - [ ] TAKE ALL (take all visible items)
  - [ ] DROP ALL (drop entire inventory)
  - [ ] TAKE ALL EXCEPT (selective taking)
  - [ ] EXAMINE ALL

- [ ] **Complex Syntax**
  - [ ] PUT [object] IN/ON [container]
  - [ ] THROW [object] AT [target]
  - [ ] GIVE [object] TO [NPC]
  - [ ] ATTACK [NPC] WITH [weapon]

- [ ] **Smart Disambiguation**
  - [ ] Ask which object if ambiguous
  - [ ] Prefer visible objects over inventory
  - [ ] Context-sensitive object matching

- [ ] **Command Chaining**
  - [ ] Multiple commands with AND/THEN
  - [ ] Example: "TAKE LAMP AND GO NORTH"

### Room Description System (Enhanced)
- [x] Basic room descriptions (implemented)
- [ ] **Verbose/Brief Modes**
  - [ ] VERBOSE: Always full description
  - [ ] BRIEF: Short on repeat visits (default)
  - [ ] SUPERBRIEF: Just room name

- [ ] **Dynamic Descriptions**
  - [ ] Room changes after puzzles solved
  - [ ] Different descriptions based on state
  - [ ] Time-of-day descriptions (if implementing)

- [ ] **Special Messages**
  - [ ] First-time visit messages
  - [ ] Random flavor text
  - [ ] Atmospheric details

### Death & Game Over
- [ ] **Death Scenarios**
  - [ ] Killed by Grue (darkness)
  - [ ] Killed by Thief
  - [ ] Killed by Troll
  - [ ] Killed by Cyclops
  - [ ] Fell into chasm
  - [ ] Drowned in river
  - [ ] Volcano death
  - [ ] Other environmental deaths

- [ ] **Game Over Handling**
  - [ ] Death message
  - [ ] Option to restore
  - [ ] Option to restart
  - [ ] Show final score

### Win Condition
- [ ] **Victory Requirements**
  - [ ] All treasures in trophy case
  - [ ] Final score calculation
  - [ ] Victory message
  - [ ] Rank assignment
  - [ ] Endgame sequence

---

## 🔧 Technical Improvements

### Architecture Enhancements
- [ ] **Action Handler System**
  - [ ] Room-specific action handlers (referenced but not implemented)
  - [ ] Object-specific action handlers (referenced but not implemented)
  - [ ] Pre-action and post-action hooks
  - [ ] Custom behavior per object/room

- [ ] **State Machine for NPCs**
  - [ ] NPC behavior states
  - [ ] State transitions
  - [ ] Event-driven NPC actions

- [ ] **Plugin System**
  - [ ] Loadable room modules
  - [ ] Loadable object modules
  - [ ] Custom command plugins

### Data-Driven Content
- [ ] **JSON/XML World Data**
  - [ ] Move hardcoded rooms to data files
  - [ ] Move hardcoded objects to data files
  - [ ] Easier content editing
  - [ ] Support for modding

- [ ] **Script System**
  - [ ] Scripted puzzle logic
  - [ ] Scripted NPC behavior
  - [ ] Event scripts

### Performance & Quality
- [ ] **Unit Tests**
  - [ ] Parser tests
  - [ ] Command tests
  - [ ] World state tests
  - [ ] Game logic tests

- [ ] **Integration Tests**
  - [ ] End-to-end gameplay tests
  - [ ] Puzzle solution tests
  - [ ] NPC interaction tests

- [ ] **Documentation**
  - [ ] XML documentation for all public APIs
  - [ ] Developer guide for adding content
  - [ ] Tutorial for creating custom commands
  - [ ] Puzzle walkthrough (for testing)

### User Experience
- [ ] **Better Error Messages**
  - [ ] Context-aware help
  - [ ] Suggestions for similar commands
  - [ ] Hints when stuck

- [ ] **Accessibility**
  - [ ] Screen reader support
  - [ ] Configurable text size
  - [ ] High contrast mode
  - [ ] Colorblind-friendly output

- [ ] **Multiple UI Implementations**
  - [x] Console UI (implemented)
  - [ ] GUI (Windows Forms, WPF, or Avalonia)
  - [ ] Web UI (Blazor)
  - [ ] Mobile UI (MAUI)

---

## 📋 Implementation Priority Guide

### Phase 1: Core Gameplay (Essential for Playability)
1. **Major Puzzles**: Trap door, trophy case, lamp mechanics
2. **Critical Rooms**: Complete White House, basic dungeon layout
3. **Essential Commands**: OPEN, CLOSE, TURN ON/OFF, READ
4. **Light System**: Lamp battery, darkness death
5. **Save/Load**: Game persistence

### Phase 2: Combat & NPCs (Major Gameplay)
1. **Thief AI**: Movement, stealing, combat
2. **Troll Combat**: Basic combat system
3. **Combat Commands**: ATTACK, KILL with weapons
4. **NPC Interactions**: GIVE, TALK TO

### Phase 3: Complete World (Content)
1. **Remaining Rooms**: River system, mine, temple
2. **All Treasures**: Complete 20 treasures
3. **Puzzle Implementation**: Dam, rainbow, Cyclops, etc.
4. **Environmental Objects**: Interactive scenery

### Phase 4: Advanced Features (Polish)
1. **Advanced Parser**: Pronouns, multi-object, disambiguation
2. **Timed Events**: Complex event system
3. **Multiple Endings**: Different victory/death scenarios
4. **Achievements**: Beyond basic scoring

### Phase 5: Quality & Extensibility (Professional)
1. **Unit Tests**: Comprehensive test coverage
2. **Documentation**: Full API docs
3. **Data-Driven**: JSON/XML world data
4. **UI Alternatives**: GUI, web, mobile

---

## 🤝 Contributing

Interested in implementing any of these features? Here's how to help:

1. **Pick an item** from the lists above
2. **Open an issue** on GitHub describing what you plan to implement
3. **Fork the repository** and create a feature branch
4. **Implement the feature** following SOLID principles
5. **Write tests** for your feature
6. **Submit a pull request** with a clear description

### Contribution Guidelines
- Follow existing code style and architecture patterns
- Maintain SOLID principles
- Add XML documentation comments
- Include unit tests for new features
- Update this TODO file when features are completed

### Good First Issues
- [ ] Add individual room implementations (pick any from the list)
- [ ] Add individual object implementations (pick any from the list)
- [ ] Implement simple commands (SMELL, TASTE, TOUCH)
- [ ] Add more room descriptions
- [ ] Improve parser vocabulary

### Medium Complexity
- [ ] Implement a complete puzzle (e.g., mailbox)
- [ ] Add combat system basics
- [ ] Implement save/load functionality
- [ ] Add new command with full syntax support

### Advanced/Expert
- [ ] Thief AI and behavior
- [ ] Complete event system
- [ ] Data-driven world loading
- [ ] Alternative UI implementation

---

## 📖 Reference Materials

### Original Zork I Documentation
- ZIL source code in this repository (root directory)
- [ZILF Compiler Documentation](http://zilf.io)
- [Zork I Walkthrough](https://www.gamefaqs.com/pc/564467-zork-i/faqs)
- [Interactive Fiction Archive](https://www.ifarchive.org/)

### Technical Resources
- See `ARCHITECTURE.md` for SOLID principles implementation
- See `README.md` for current feature list
- Browse `ZorkSharp/` source code for implementation examples

---

## 📝 Progress Tracking

**Last Updated**: 2025-01-XX

**Recent Completions**:
- ✅ Basic game engine and command system
- ✅ 25 rooms from White House and early dungeon
- ✅ 28 objects including basic treasures
- ✅ Natural language parser
- ✅ Lighting system basics

**Current Focus**:
- [ ] Awaiting community contributions!

**Next Milestones**:
1. 50 rooms implemented
2. All 20 treasures added
3. Basic combat system
4. Save/load functionality
5. Complete trophy case puzzle

---

*This is a living document. As features are implemented, they should be checked off and moved to the "Completed" section. Feel free to add newly discovered missing features!*
