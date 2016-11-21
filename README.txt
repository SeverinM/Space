Synopsis :

This is my first "real" game made with unity. I never thought to publish my game before but when i saw my game became bigger and bigger i thought it can be a good idea.
As i assumed my game will be never published, the code and the game is wrote in french. But i'm going to translate them as soon as possible.

This game is a simple endless space shooter with bosses : the longer you survive the harder ennemies are.
There are a highscore board, but it's not currently working because i have nowhere to host my database


Motivation : 

I made this game to learn how to use unity but also for show my personnal projects and for fun ! 


Tests : 

There are mainly 2 objects types in this game : Bullets and ennemies

  Bullets :
    -Bullet are created and destroyed using the "pooling" method
    -every bullet class is inherited from abstract class "BaseProjectile"
    -Every bullet objects got a speed (can be negative), an initial position,a direction, a damage number (only positive) and a boolean which show if this bullet is perforating or not
    - Every bullet is destroyed when it left the main camera sight
    - Every time you set a direction , the vector's lenght is always set to 1 as the speed is defined by the property "vitesse"
    
  Ennemies : 
    - Every ennemy class is inherited from abstract class "BaseEnnemie", including bosses
    - Every ennemy object got a score value, an HP value, an explosion range (currently unused), a bonus drop probability and a destroy sound
    - Every ennemy is destroyed when it left the main camera sight
    - Every ennemy object got some defaut function : Booster (make the ennemy harder), Detruire (called when HP is equal or below 0 ), Soigner (called when ennemy is healed), perdreVie (called when ennemy lost a life).
    
Ennemis et bullets interact depending their tag : for example Gameobject with tag "Player" will only detect a collision with gameObject "EnnemyBullet"


Contributors :

I'm currently alone on this project.
Severin michaut (severinmichaut@gmail.com)


License : 

this project is using MIT license
