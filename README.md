## Synopsis :

This is my first "real" game made with unity. I never thought to publish it before but when i saw it became bigger and bigger i thought it can be a good idea.
As i assumed my game will never be published, the code and the game is written in french. 
But i'm going to translate them as soon as possible.

This game is a simple endless space shooter with bosses : the longer you survive the harder ennemies are.
There is a highscore board, but it's not currently working because i have nowhere to host my database.


## Motivation : 

I made this game to learn how to use unity but also to show my personnal projects and for fun ! 

## How to launch the game : 

Just download the Space.exe standalone.

##Screenshots :


### Menu
![Menu](https://github.com/SeverinM/Space/blob/Test/images/MenuScreen.png)

### Regular battle
![Battle](https://github.com/SeverinM/Space/blob/Test/images/Battle1.png)
![Battle](https://github.com/SeverinM/Space/blob/Test/images/Battle2.png)
![Battle](https://github.com/SeverinM/Space/blob/Test/images/Battle3.png)

### Boss battle
![Boss](https://github.com/SeverinM/Space/blob/Test/images/Boss.png)

### Game Over

![Game Over](https://github.com/SeverinM/Space/blob/Test/images/GameOver.png)

## Code description : 

There are mainly 2 object's types in this game : Bullets and ennemies
Until translation and despite written in french, most of the time classes' name are self-explanatory, since english and french languages share vocabulary or are quite close, sometimes...
* Bullets :
    * Bullet are created and destroyed using the "pooling" method
    * Every bullet class is inherited from abstract class *BaseProjectile*
    * Every bullet objects got a speed (can be negative), an initial position, a direction, a damage number (only positive) and a boolean which show if this bullet is perforating or not
    *  Every bullet is destroyed when it left the main camera sight
    *  Every time you set a direction , the vector's length is always set to 1 as the speed is defined by the property *vitesse*, which means speed in french
    
* Ennemies : 
    * Every ennemy class is inherited from abstract class *BaseEnnemie*, including bosses
    * Every ennemy object got a score value, an HP value, an explosion range (currently unused), a bonus drop probability and a destroy sound
    * Every ennemy is destroyed when it left the main camera sight
    * Every ennemy object got some defaut function : *Booster* (make the ennemy harder), *Detruire* (called when HP is equal or below 0 ), *Soigner* (called when ennemy is healed), *perdreVie* (called when ennemy lost a life).
    
**Ennemis and bullets interact depending their tag** : for example Gameobject with tag *Player* will only detect a collision with gameObject *EnnemyBullet*

Feel free to contact me or pull requests. I'll also be happy to have advices or criticism.

## Contributors :

I'm currently the only developer on this project. Pull requests highly appreciated. But perhaps most of you will have to wait for a proper english translation :-)

Severin michaut (severinmichaut@gmail.com)

Musics made by Léo Juriens (SoundCloud : https://soundcloud.com/last-nova)


## License : 

<img src="images/MIT-logo.png" width="200">
