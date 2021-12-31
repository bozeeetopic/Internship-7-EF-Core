using Presentation.Actions;
using Presentation.Helpers.Intro;

namespace PaymentManager.Presentation
{
    public class Program
    {
        static void Main()
        {
            //IntroAnimation.PrintIntro( 100, 300);
            ActionsCaller.PrintMenuAndDoAction(ActionsCaller.AppStartActions);
        }
    }
}