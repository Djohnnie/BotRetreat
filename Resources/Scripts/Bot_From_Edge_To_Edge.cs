if( (Location.X == 0 && Orientation == WEST) || (Location.X == Width-1 && Orientation == EAST))
{ 
    TurnAround();
}
else
{
    MoveForward();
}