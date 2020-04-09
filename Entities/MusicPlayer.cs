using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace PIMonsterMash {
    static class MusicPlayer {
        static Dictionary<int, string> musicList = new Dictionary<int, string>();
        static SoundPlayer soundDevice;
        static Boolean isMuted = true;

        static MusicPlayer() {
            musicList.Add(0, AppDomain.CurrentDomain.BaseDirectory + "\\B1.wav");
            musicList.Add(1, AppDomain.CurrentDomain.BaseDirectory + "\\B2.wav");
            musicList.Add(2, AppDomain.CurrentDomain.BaseDirectory + "\\B3.wav");
            musicList.Add(3, AppDomain.CurrentDomain.BaseDirectory + "\\B4.wav");
            musicList.Add(4, AppDomain.CurrentDomain.BaseDirectory + "\\B5.wav");
            musicList.Add(5, AppDomain.CurrentDomain.BaseDirectory + "\\B6.wav");
            musicList.Add(6, AppDomain.CurrentDomain.BaseDirectory + "\\B7.wav");
            musicList.Add(7, AppDomain.CurrentDomain.BaseDirectory + "\\B8.wav");            
        }

        public static string GetRandom() {
            var rnd = new Random(DateTime.Now.Millisecond);
            return musicList[rnd.Next(0, musicList.Count)];
        }

        public static void Play(string path) {  
            if (!String.IsNullOrEmpty(path)) {
                soundDevice = new SoundPlayer();
                soundDevice.SoundLocation = path;
                soundDevice.PlayLooping();
                isMuted = false;
            }
        }

        public static void Stop() {
            if (soundDevice != null) {
                soundDevice.Stop();
                isMuted = true;
            }
        }

        public static void ToggleMusic()
        {
            if (soundDevice != null)
            {
                if (!string.IsNullOrEmpty(soundDevice.SoundLocation))
                {
                    if (isMuted)
                        soundDevice.PlayLooping();
                    else
                        soundDevice.Stop();
                    isMuted = !isMuted;
                }
            }
        }
    }
}
