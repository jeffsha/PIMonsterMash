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
            musicList.Add(0, AppDomain.CurrentDomain.BaseDirectory + "\\Forest.wav");
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
