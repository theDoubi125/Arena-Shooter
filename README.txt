Authors:
	- Maxime Bourgeois
	- Julien Revel
	- Donatien Rabiller
	- Gabriel Rassoul

Homework #2 made for the algorithm course 8INF870 during the trimester WINTER 2016

Controls:
	- Shoot -> [Ctrl] OR [Left Click]
	- Move -> [Z][Q][S][D]
	- Move Target -> [Mouse]
	- Next Difficulty -> [Enter] OR [Space]

Enemies:
	- BasicEnemy will charge you and inflict damages on contact
	- ShooterEnemy will shoot you and inflict damages when the projectiles touch you
	- BossEnemy will shoot and charge you, it will inflict damages on contact and when the projectiles touch you
	
Difficulty Behaviour:
	- When loosing:
		- BossEnemy loses health
		- Other enemy that couldn't be killed will be removed from the tree
	- When winning:
		- BossEnemy wins health
		- 50% of the time sequential nodes that are parents of the beaten enemies will be duplicated
		- 50% of the time spawn from beaten enemies will be duplicated