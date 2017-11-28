using System;
using Android.Media;
using Java.IO;

namespace Audacious.Droid.Utility
{
    public class VcAudioRecorder
    {

        #region Properties

        MediaRecorder mMediaRecorder;

        public string mAudioFilePath;

        #endregion


        public VcAudioRecorder()
        {
                        
        }

        #region public

        public void startRecording()
        {
            try
            {
                mediaRecorderReddy();
                mMediaRecorder.Prepare();
                mMediaRecorder.Start();
            }
            catch (IOException io)
            {
                io.PrintStackTrace();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("{0} - {1}, Exception: {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        public void stopRecording()
        {
            try
            {
                mMediaRecorder.Stop();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("{0} - {1}, Exception: {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

        #endregion


        private void mediaRecorderReddy ()
        {
            try
            {
                mMediaRecorder = new MediaRecorder();
                mMediaRecorder.SetAudioSource(AudioSource.Mic);
                mMediaRecorder.SetOutputFormat(OutputFormat.ThreeGpp);
                mMediaRecorder.SetAudioEncoder(AudioEncoder.AmrNb);
                mMediaRecorder.SetOutputFile(mAudioFilePath);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("{0} - {1}, Exception: {2}", this.GetType().Name, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message);
            }
        }

    }
}
