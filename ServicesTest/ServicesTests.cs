using MultiApp.Services.Interfaces;
using Moq;
using MusicModule.ViewModels;
using Prism.Regions;
using System.Windows.Media;

namespace ServicesTest
{
    public class ServicesTest
    {
        Mock<IMusicPlayerService> _musicService;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void MusicPlayerTest()
        {
            var player = new PlayerViewModel(_musicService.Object);
            player.PlayCurrentSound.Execute();
            player.PlayCurrentSound.Execute();
            _musicService.Verify(e => e.Play(), Times.Exactly(2));
            //Assert.Throws<NotImplementedException>(player.PlayNextSound.Execute);

        }

        public ServicesTest()
        {
            var musicS = new Mock<IMusicPlayerService>();
            var regionManager = new Mock<IRegionManager>();
            musicS.Setup(e => e.Play()).Callback(() =>
            {
                mediaPlayer.Open(new Uri("https://drive.google.com/uc?export=download&id=1V6w2Ru17X58zNsDJlDFToMhcoiG8fP_j"));
                mediaPlayer.Play();
            });

            _musicService = musicS;
        }


            private MediaPlayer mediaPlayer = new MediaPlayer();

            
        

    }
}