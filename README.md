### Project Information
Basic Third Person melee combat prototype. 
Using input system, cinemachine, Mixamo.com animations. Supports controller. 
Has attack sequence, block, dodge, camera lock, climbing. 
Majority of code is driven by state machine.
- [Project image album](https://imgur.com/a/EATo5qx)
- [Production build as zip](https://drive.google.com/file/d/1_5O5a4AqhOfYXHF0i3chk3EISStes-OM/view?usp=sharing)

### Input Information
Controller | Keyboard & Mouse | Action
--- | --- | ---
South button | Space |  jump / climb ledge
Right shoulder | Shift | drop ledge
West button | LMB | attack
Left stick | WASD | movement
Right stick | Mouse delta | camera rotation
Left trigger | Tab | toggle targeting state
East button | RMB | block (targeting state)
Right shoulder | Shift | dodge (targeting state)

### Features
+ Attacking
+ Animation cancels (around after 90% of attack)
+ Blocking
+ Climbing ledges
+ Jumping
+ Dodging
+ Camera lock
+ Health 
	+ both characters max health = 100
	+ All enemy attacks do 20 damage
	+ Player attack damage vary
		+ 1: 10 damage, 2: 15 damage, 3: 20 damage
+ On hit push back
+ Hit and death animations

### Limitations
+ Ledge needs to be marked with specific script
+ No audio
+ No UI
+ No death / quit menu (need to use alt + f4)

### Course links
- [link](https://www.gamedev.tv/p/unity-3rd-person-combat/?coupon_code=AUTUMN)
