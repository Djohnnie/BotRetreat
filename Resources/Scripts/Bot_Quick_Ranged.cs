var enemyBot = Vision.Bots.FirstOrDefault();
if( enemyBot != null )
{
    RangedAttack(enemyBot.Location.X, enemyBot.Location.Y);
}