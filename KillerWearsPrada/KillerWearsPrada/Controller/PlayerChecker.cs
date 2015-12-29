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

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PlayerEnterKinectSensor.Args> RaisePlayerEnterKinectSensor;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PlayerLeaveKinectSensor.Args> RaisePlayerLeaveKinectSensor;

        public PlayerChecker()
        {
            attIDCurrentPalyer = null;
            
        }

        /// <summary>
        /// Return a <see cref="String"/> value that represent the current <see cref="Model.Player"/>'s ID
        /// </summary>
        public String IDCurrentPlayer
        {
            get { return attIDCurrentPalyer; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void CheckPlayer(String ID)
        {
            Relation wvRel = CheckRelation(attIDCurrentPalyer, ID);
            switch (wvRel)
            {
                case Relation.STILL_NOT_AVAILABLE:
                    return;
                case Relation.CP_NULL_NP_NULL:
                    return;
                case Relation.CP_NULL_NP_VALUE:
                    attIDCurrentPalyer = ID;
                    RaisePlayerEnterKinectSensorEvent(ID);
                    break;
                case Relation.CP_VALUE_NP_NULL:
                    attIDCurrentPalyer = null;
                    RaisePlayerLeaveKinectSensorEvent();
                    break;
                case Relation.CP_VALUE_NP_VALUE:
                    throw new NotImplementedException("Implementare il cotnrollo per capire se c'è un cambio di utente");
                    //break;
                default:
                    break;
            }

        }

        private Relation CheckRelation(String ActualID, String NewID)
        {
            Relation wvRel;
            if (ActualID == null)
            { 
                if (NewID == null)
                    wvRel = Relation.CP_NULL_NP_NULL;
                else
                    wvRel = Relation.CP_NULL_NP_VALUE;
            }
            else
            {
                if (NewID == null)
                    wvRel = Relation.CP_VALUE_NP_NULL;
                else
                    wvRel = Relation.CP_VALUE_NP_VALUE;
            }
            return wvRel;
        }


        #region Sezione per scatenare dell'evento PlayerEnterKinectSensor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerEnterKinectSensor(PlayerEnterKinectSensor.Args e)
        {
            EventHandler<PlayerEnterKinectSensor.Args> wvHendeler = RaisePlayerEnterKinectSensor;
            if (wvHendeler != null)
            {
                wvHendeler(this, e);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RaisePlayerEnterKinectSensorEvent(String ID)
        {
            PlayerEnterKinectSensor.Args wvParameters = new PlayerEnterKinectSensor.Args(ID);


            OnPlayerEnterKinectSensor(wvParameters);
        }
        #endregion

        #region Sezione per scatenare dell'evento PlayerLeaveKinectSensor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerLeaveKinectSensor(PlayerLeaveKinectSensor.Args e)
        {
            EventHandler<PlayerLeaveKinectSensor.Args> wvHendeler = RaisePlayerLeaveKinectSensor;
            if (wvHendeler != null)
            {
                wvHendeler(this, e);
            }
        }

        public void RaisePlayerLeaveKinectSensorEvent()
        {
            //TODO completare i parametri
            PlayerLeaveKinectSensor.Args wvParameters = new PlayerLeaveKinectSensor.Args();


            OnPlayerLeaveKinectSensor(wvParameters);
        }
        #endregion

        /// <summary>
        /// Manage the generation of the event PlayerEnterKinectSensor that occure when a <see cref="Model.Player"/> 
        /// enter the KinectSensor region and is recognized by the game.
        /// </summary>
        public class PlayerEnterKinectSensor
        {
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
            public class Args : EventArgs
            {

            }
        }

        public enum Relation
        {
            STILL_NOT_AVAILABLE=-1,
            CP_NULL_NP_NULL=0,
            CP_NULL_NP_VALUE=1,
            CP_VALUE_NP_NULL=2,
            CP_VALUE_NP_VALUE=4
    }
        
    }
}
