var gamelib = {};

gamelib.run = function(func)
{
    var wrapper = function()
    {
        func();
        setTimeout(wrapper, 1);
    };
    wrapper();
}

gamelib.setup = function(gameType, targetFPS)
{
    var game = new gameType();
    game.show();
    var lastDraw = DateTime.now();
    var sinceLastDraw = 0;
    var millisPerFrame = Math.ceiling(1000 / targetFPS);
    
    gamelib.run(function()
    {
        if(!game.isDone())
        {
            var next = DateTime.now();
            sinceLastDraw += next.totalMilliseconds - lastDraw.totalMilliseconds;
            if(sinceLastDraw > millisPerFrame)
            {
                for(i = 0; i < sinceLastDraw; i += millisPerFrame)
                    game.update(millisPerFrame);
                game.draw();
                sinceLastDraw = %= millisPerFrame;
            }
            lastDraw = next;
        }
    });
}