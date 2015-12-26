using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KillerWearsPrada.Controller
{
    /// <summary>
    /// Contains information about the current <see cref="Model.Player"/>
    /// </summary>
    class PlayerChecker
    {

        private String attIDCurrentPalyer;



        public PlayerChecker()
        {
            
            //TODO finire il costruttore e implementare la chiamata per scatenare l'evento di cambiamento di giocatore
        }

        /// <summary>
        /// Return a <see cref="String"/> value that represent the current <see cref="Model.Player"/>'s ID
        /// </summary>
        public String IDCurrentPlayer
        {
            get { return attIDCurrentPalyer; }
        }


        /// <summary>
        /// Manage the generation of the event PlayerEnterKinectSensor that occure when a <see cref="Model.Player"/> 
        /// enter the KinectSensor region and is recognized by the game.
        /// </summary>
        public class PlayerEnterKinectSensor
        {
            /*
            /// <summary>
            /// Delegate that handle the event raised by <see cref="PlayerEnterKinectSensor"/>
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="e"></param>
            public delegate void PlayerStillOnKinectSensorEventHandler(object sender, PlayerEnterKinectSensorArgs e);
            */

            /// <summary>
            /// 
            /// </summary>
            public event EventHandler<Args> RaisePlayerEnterKinectSensor;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            protected virtual void OnPlayerEnterKinectSensor(Args e)
            {
                EventHandler<Args> wvHendeler = RaisePlayerEnterKinectSensor;
                if (wvHendeler != null)
                {
                    wvHendeler(this, e);
                }
            }

            /// <summary>
            /// 
            /// </summary>
            public void RaiseEvent()
            {
                //TODO mettere l'ID del giocatore entrato
                Args wvParameters = new Args("");


                OnPlayerEnterKinectSensor(wvParameters);
            }

            /*
            /// <summary>
            /// Instance of the delegate
            /// </summary>
            public static PlayerStillOnKinectSensorEventHandler PlayerStillOnKinectSensorChanged;
            */

            /*
            /// <summary>
            /// Raise the Event associated to the <see cref="PlayerStillOnKinectSensorEventHandler"/> delegate
            /// </summary>
            /// <param name="e"></param>
            public void RaiseEventPlayerStillOnKinectSensorChanged(PlayerEnterKinectSensorArgs e)
            {
                if (PlayerStillOnKinectSensorChanged != null)
                {
                    PlayerStillOnKinectSensorChanged(this, e);
                }
            }*/

            /// <summary>
            /// Contains information used by the event <see cref="PlayerEnterKinectSensor"/>
            /// </summary>
            public class Args : EventArgs
            {

                private String attID;

                public Args(String ID) : base()
                {
                    attID = ID;
                }

                public String ID
                {
                    get { return attID; }
                }


            }

        }

        public class PlayerLeaveKinectSensor
        {
            /// <summary>
            /// 
            /// </summary>
            public event EventHandler<Args> RaisePlayerLeaveKinectSensor;

            /// <summary>
            /// 
            /// </summary>
            /// <param name="e"></param>
            protected virtual void OnPlayerLeaveKinectSensor(Args e)
            {
                EventHandler<Args> wvHendeler = RaisePlayerLeaveKinectSensor;
                if (wvHendeler != null)
                {
                    wvHendeler(this, e);
                }
            }

            public void RaiseEvent()
            {
                //TODO completare i parametri
                Args wvParameters = new Args();


                OnPlayerLeaveKinectSensor(wvParameters);
            }

            public class Args : EventArgs
            {

            }
        }

        
        

    }
}
