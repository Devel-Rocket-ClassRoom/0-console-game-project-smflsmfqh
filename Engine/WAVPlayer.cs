using System;
using NAudio.Wave;

namespace Framework.Engine
{
    public enum sounds
    {
        Title,
        Chopstick,
        Moonlight,
        Menu,
    }
    class WAVPlayer
    {
        string audioFilePath;

        AudioFileReader audioFile;
        WaveOutEvent outputDevice;
        public WAVPlayer(sounds sound)
        {
            switch(sound)
            {
                case sounds.Title:
                    audioFilePath = "RhythmGame\\Assets\\0titleMusic.wav";
                    break;
                case sounds.Chopstick:
                    audioFilePath = "RhythmGame\\Assets\\1chopstick.wav";
                    break;
                case sounds.Moonlight:
                    audioFilePath = "RhythmGame\\Assets\\2moonlight.wav";
                    break;
                case sounds.Menu:
                    audioFilePath = "RhythmGame\\Assets\\3menuSelect.wav";
                    break;
            }

            audioFile = new AudioFileReader(audioFilePath);
            outputDevice = new WaveOutEvent();

            // 초기 필수
            outputDevice.Init(audioFile);
        }
        public void Play()
        {
            outputDevice.Play();
        }
        public void PlayLooping()
        {
            outputDevice.PlaybackStopped += PlaybackStoppedHandler;
            outputDevice.Play();
        }
        public void Stop()
        {
            outputDevice.PlaybackStopped -= PlaybackStoppedHandler;
            outputDevice.Stop();
        }
        public void SetVolume(float volume)
        {
            outputDevice.Volume = volume;
        }

            // --- 현재 재생 위치 ---
        public double GetCurrentMs()
        {
            return audioFile.CurrentTime.TotalMilliseconds;
        }

            // --- 곡 전체 길이 ---
        public double GetTotalMs()
        {
            return audioFile.TotalTime.TotalMilliseconds;
        }
        public bool IsPlaying()
        {
            return outputDevice.PlaybackState == PlaybackState.Playing;
        }
        public void Dispose()
        {
            outputDevice.Dispose();
            audioFile.Dispose();
        }
        public void PlaybackStoppedHandler(object sender, StoppedEventArgs e)
        {

            // 오디오 파일의 위치를 처음으로 재설정
            audioFile.Position = 0;
            outputDevice.Play();
        }
    }
}