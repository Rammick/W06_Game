using System;
using System.Collections.Generic;
using System.IO;
using Unit06.Game.Casting;
using Unit06.Game.Scripting;
using Unit06.Game.Services;


namespace Unit06.Game.Directing
{
    public class SceneManager
    {
        public static AudioService AudioService = new RaylibAudioService();
        public static KeyboardService KeyboardService = new RaylibKeyboardService();
        public static PhysicsService PhysicsService = new RaylibPhysicsService();
        public static VideoService VideoService = new RaylibVideoService(Constants.GAME_NAME,
            Constants.SCREEN_WIDTH, Constants.SCREEN_HEIGHT, Constants.GREEN);

        public SceneManager()
        {
        }

        public void PrepareScene(string scene, Cast cast, Script script)
        {
            if (scene == Constants.NEW_GAME)
            {
                PrepareNewGame(cast, script);
            }
            else if (scene == Constants.NEXT_LEVEL)
            {
                PrepareNextLevel(cast, script);
            }
            else if (scene == Constants.TRY_AGAIN)
            {
                PrepareTryAgain(cast, script);
            }
            else if (scene == Constants.IN_PLAY)
            {
                PrepareInPlay(cast, script);
            }
            else if (scene == Constants.GAME_OVER)
            {
                PrepareGameOver(cast, script);
            }
            else if (scene == Constants.P1_DID_WIN)
            {
                PrepareGameOver(cast, script);
            }
            else if (scene == Constants.P2_DID_WIN)
            {
                PrepareGameOver(cast, script);
            }
        }

        private void PrepareNewGame(Cast cast, Script script)
        {
            //Adds HUD items and Selectors to the cast
            AddStats(cast);
            AddLives1(cast);
            AddLives2(cast);
            AddSelector(cast);
            AddDialog(cast, Constants.ENTER_TO_START);
            ///note: Fighters are added to the cast via the SpawnFightersAction class
            /// activated in PrepareInPlay

            script.ClearAllActions();
            AddInitActions(script);
            AddLoadActions(script);

            ChangeSceneAction a = new ChangeSceneAction(KeyboardService, Constants.NEXT_LEVEL);
            script.AddAction(Constants.INPUT, a);

            AddOutputActions(script);
            AddUnloadActions(script);
            AddReleaseActions(script);
        }
        ///new Selector created and added to the cast
        private void ActivateSelector(Cast cast)
        {
            Selector selector = (Selector)cast.GetFirstActor(Constants.SELECTOR_GROUP);
            selector.MoveNext();

            Selector selector2 = (Selector)cast.GetFirstActor(Constants.SELECTOR_GROUP);
            selector2.MoveNext();
        }

        private void PrepareNextLevel(Cast cast, Script script)
        {
            AddSelector(cast);
            AddDialog(cast, Constants.PREP_TO_LAUNCH);

            script.ClearAllActions();

            TimedChangeSceneAction ta = new TimedChangeSceneAction(Constants.IN_PLAY, 2, DateTime.Now);
            script.AddAction(Constants.INPUT, ta);

            AddOutputActions(script);

            PlaySoundAction sa = new PlaySoundAction(AudioService, Constants.WELCOME_SOUND);
            script.AddAction(Constants.OUTPUT, sa);
        }

        private void PrepareTryAgain(Cast cast, Script script)
        {
            AddSelector(cast);
            AddDialog(cast, Constants.PREP_TO_LAUNCH);

            script.ClearAllActions();
            
            TimedChangeSceneAction ta = new TimedChangeSceneAction(Constants.IN_PLAY, 2, DateTime.Now);
            script.AddAction(Constants.INPUT, ta);
            
            AddUpdateActions(script);
            AddOutputActions(script);
        }
        ///prepares InPlay for the game
        private void PrepareInPlay(Cast cast, Script script)
        {
            ActivateSelector(cast);
            ActivateFighter(cast);
            cast.ClearActors(Constants.DIALOG_GROUP);

            script.ClearAllActions();

            ControlSelectorAction action = new ControlSelectorAction(KeyboardService);
            script.AddAction(Constants.INPUT, action);

            AddUpdateActions(script);
            AddOutputActions(script);
        
        }
        ///Game over setup for teams and ending when lives are at 0 or less
        private void PrepareGameOver(Cast cast, Script script)
        {
            Stats stats = (Stats)cast.GetFirstActor(Constants.STATS_GROUP);
            AddSelector(cast);
            AddDialog(cast, Constants.WAS_GOOD_GAME);
           
            if (stats.GetPlayer2Lives() <= 0)
            {
                AddDialog(cast, Constants.P1_DID_WIN);
            }
            else
            {
                AddDialog(cast, Constants.P2_DID_WIN);
            }
            
            script.ClearAllActions();

            TimedChangeSceneAction ta = new TimedChangeSceneAction(Constants.NEW_GAME, 5, DateTime.Now);
            script.AddAction(Constants.INPUT, ta);

            AddOutputActions(script);
        }

        // -----------------------------------------------------------------------------------------
        // casting methods
        // -----------------------------------------------------------------------------------------
        private void AddDialog(Cast cast, string message)
        {
            cast.ClearActors(Constants.DIALOG_GROUP);

            Text text = new Text(message, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_CENTER, Constants.WHITE);
            Point position = new Point(Constants.CENTER_X, Constants.CENTER_Y);

            Label label = new Label(text, position);
            cast.AddActor(Constants.DIALOG_GROUP, label);   
        }

        /// <summary>
        /// Adds player 1's lives to the cast to be be put on top left of HUD
        /// </summary>
        private void AddLives1(Cast cast)
        {
            cast.ClearActors(Constants.LIVES1_GROUP);

            Text text = new Text(Constants.LIVES1_FORMAT, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_LEFT, Constants.WHITE);
            Point position = new Point(Constants.HUD_MARGIN, Constants.HUD_MARGIN);

            Label label = new Label(text, position);
            cast.AddActor(Constants.LIVES1_GROUP, label);
        }

        /// <summary>
        /// Adds player 2's lives to the cast to be be put on top left of HUD
        /// </summary>
        private void AddLives2(Cast cast)
        {
            cast.ClearActors(Constants.LIVES2_GROUP);

            Text text = new Text(Constants.LIVES2_FORMAT, Constants.FONT_FILE, Constants.FONT_SIZE, 
                Constants.ALIGN_RIGHT, Constants.WHITE);
            Point position = new Point(Constants.SCREEN_WIDTH - Constants.HUD_MARGIN, 
                Constants.HUD_MARGIN);

            Label label = new Label(text, position);
            cast.AddActor(Constants.LIVES2_GROUP, label);   
        }

        /// <summary>
        /// Sets positions of Selectors and calls to BuildAddSelector(s).
        /// </summary>
        private void AddSelector(Cast cast)
        {
            cast.ClearActors(Constants.SELECTOR_GROUP);
        
            int x1 = Constants.SCREEN_WIDTH * (9/10);
            int y1 = Constants.SCREEN_HEIGHT / 2;

            BuildAddSelector(x1, y1, cast, false);

            int x2 = Constants.SCREEN_WIDTH - 2 * Constants.SELECTOR_WIDTH;
            int y2 = Constants.SCREEN_HEIGHT / 2;
            
            BuildAddSelector(x2, y2, cast, true);
        
        }

        /// <summary>
        /// Applies position, size, and velocity to the Selectors.
        /// Adds the Selectors to the cast.
        /// </summary>
        private void BuildAddSelector(int x, int y, Cast cast, bool is_S2)
        {
            Point position = new Point(x, y); //sets the position of Selector
            Point size = new Point(Constants.SELECTOR_WIDTH, Constants.SELECTOR_HEIGHT);
            Point velocity = new Point(0, 0);
        
            Body body = new Body(position, size, velocity);
            Animation animation;
            if(is_S2)
                animation = new Animation(Constants.SELECTOR2_IMAGES, 0, Constants.SELECTOR_RATE);
            else    
                animation = new Animation(Constants.SELECTOR_IMAGES, 0, Constants.SELECTOR_RATE);
            Selector selector = new Selector(body, animation, false);
        
            cast.AddActor(Constants.SELECTOR_GROUP, selector);

        }
        private void ActivateFighter(Cast cast)
        {
            List<Actor> fighters = cast.GetActors(Constants.FIGHTER_GROUP);

            foreach (Fighter f in fighters)
            {
                f.MoveNext();
            }
        }
    
        private void AddStats(Cast cast)
        {
            cast.ClearActors(Constants.STATS_GROUP);
            Stats stats = new Stats();
            cast.AddActor(Constants.STATS_GROUP, stats);
        }


        // -----------------------------------------------------------------------------------------
        // scripting methods
        // -----------------------------------------------------------------------------------------

        private void AddInitActions(Script script)
        {
            script.AddAction(Constants.INITIALIZE, new InitializeDevicesAction(AudioService, 
                VideoService));
        }

        private void AddLoadActions(Script script)
        {
            script.AddAction(Constants.LOAD, new LoadAssetsAction(AudioService, VideoService));
        }
        ///Draw script participants on the screen

        private void AddOutputActions(Script script)
        {
            
            script.AddAction(Constants.OUTPUT, new StartDrawingAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawSceneAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawHudAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawSelectorAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawFighterAction(VideoService));
            script.AddAction(Constants.OUTPUT, new DrawDialogAction(VideoService));
            script.AddAction(Constants.OUTPUT, new EndDrawingAction(VideoService));
        }

        private void AddUnloadActions(Script script)
        {
            script.AddAction(Constants.UNLOAD, new UnloadAssetsAction(AudioService, VideoService));
        }

        private void AddReleaseActions(Script script)
        {
            script.AddAction(Constants.RELEASE, new ReleaseDevicesAction(AudioService, 
                VideoService));
        }
        ///Updates the game with moving and spawning methods
        private void AddUpdateActions(Script script)
        {
            script.AddAction(Constants.UPDATE, new MoveFighterAction());
            script.AddAction(Constants.UPDATE, new MoveSelectorAction());
            
            script.AddAction(Constants.UPDATE, new SpawnFighterAction(KeyboardService, AudioService));
            
            script.AddAction(Constants.UPDATE, new CollideBordersAction(PhysicsService, AudioService));
            script.AddAction(Constants.UPDATE, new CollideFightersAction(PhysicsService, AudioService));
            // script.AddAction(Constants.UPDATE, new CheckOverAction());     
        }
    }
}