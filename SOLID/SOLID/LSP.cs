using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SOLID
{
    public class LSP
    {
        public void Execute()
        {
            #region with violation example 1
            Tester videoTester = new Tester(new BasicVideoPlayer());
            videoTester.execute();

            videoTester = new Tester(new VideoPlayer1());
            videoTester.execute();

            videoTester = new Tester(new VideoPlayer2());
            videoTester.execute();

            #endregion

            #region without violation example 1
            //To correct this, we should keep only general methods in BasicVideoPlayer class i.e.Play() and Pause() and let
            //the child classes define SingleTap() and DoubleTap(). This will also avoid overriding the methods which are not used

            #endregion

            #region with violation example 2
            RectangleWithoutLSP rectangle = new SquareWithoutLSP();
            rectangle.Width = 5;
            rectangle.Height = 10;

            Console.WriteLine($"Area: {rectangle.CalculateArea()}"); // Incorrect result for Square
            #endregion


            #region without violation example 2
            //Fix for LSP: A better design would be to have a separate class hierarchy for Square and Rectangle:
            RectangleLSP rect = new RectangleLSP() { Height = 2, Width = 5 };
            SquareLSP sq = new SquareLSP() { Side=8};
            Console.WriteLine($"Area: {rect.CalculateArea()}");
            Console.WriteLine($"Area: {sq.CalculateArea()}");
            #endregion

        }
    }

    #region class with violation example 1
    public class BasicVideoPlayer
    {
        public void play()
        {
            Console.WriteLine("Video is playing.");
        }

        public void pause()
        {
            Console.WriteLine("Video is paused.");
        }

        public void singleTap()
        {
            Console.WriteLine("Setting playback speed at 1.5x");
        }

        public void doubleTap()
        {
            Console.WriteLine("Setting playback speed at 2x");
        }
    }

    public class VideoPlayer1: BasicVideoPlayer
    {
        public void singleTap()
        {
            Console.WriteLine("Setting & displaying playback speed at 1.5x");
        }

        public void doubleTap()
        {
            Console.WriteLine("Setting & displaying playback speed at 2x");
        }
    }

    public class VideoPlayer2 : BasicVideoPlayer
    {
        public void singleTap()
        {
            showPopup();
        }

        public void showPopup()
        {
            Console.WriteLine("Asking user for playback speed");
        }

        public void doubleTap()
        {
            //do nothing
        }
    }

    public class Tester
    {
        BasicVideoPlayer player;
        public Tester(BasicVideoPlayer player)
        {
            this.player = player;
        }
        public void execute()
        {
            player.play();
            player.pause();
            player.singleTap();
            player.doubleTap();
        }
    }
    #endregion

    #region non-violation class - example 1
    public class BasicVideoPlayerLSP
    {
        public void play()
        {
            Console.WriteLine("Video is playing.");
        }

        public void pause()
        {
            Console.WriteLine("Video is paused.");
        }
    }
    #endregion


    #region class with violation example 2
    class RectangleWithoutLSP
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int CalculateArea()
        {
            return Width * Height;
        }
    }

    class SquareWithoutLSP : RectangleWithoutLSP
    {
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }
    #endregion


    #region class without violation example 2
    class RectangleLSP
    {
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public int CalculateArea()
        {
            return Width * Height;
        }
    }

    public class SquareLSP
    {
        //public virtual int Side { get; set; }
        public int Side { get { return 10; } set { } }

        public int CalculateArea()
        {
            return Side * Side;
        }
    }


    public class SquareChild: SquareLSP
    {
        public new int Side { get { return 20; } set => base.Side = value; }

    }
    #endregion
}



