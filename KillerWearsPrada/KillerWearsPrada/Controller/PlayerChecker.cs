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
        
        private string attIDCurrentPalyer;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PlayerEnterKinectSensor.Arguments> PlayerEnterKinectSensorEvent;
        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<PlayerLeaveKinectSensor.Arguments> PlayerLeaveKinectSensorEvent;

        public PlayerChecker()
        {
            attIDCurrentPalyer = null;
            
        }

        /// <summary>
        /// Return a <see cref="string"/> value that represent the current <see cref="Model.Player"/>'s ID
        /// </summary>
        public string IDCurrentPlayer
        {
            get { return attIDCurrentPalyer; }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ID"></param>
        public void CheckPlayer(string ID)
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
                    if (attIDCurrentPalyer != ID)
                        RaisePlayerLeaveKinectSensorEvent();
                    break;
                default:
                    break;
            }

        }

        private Relation CheckRelation(string ActualID, string NewID)
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
        protected virtual void OnPlayerEnterKinectSensor(PlayerEnterKinectSensor.Arguments e)
        {
            if (PlayerEnterKinectSensorEvent != null)
            {
                PlayerEnterKinectSensorEvent(this, e );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void RaisePlayerEnterKinectSensorEvent(string ID)
        {
            PlayerEnterKinectSensor.Arguments wvParameters = new PlayerEnterKinectSensor.Arguments(ID);


            OnPlayerEnterKinectSensor(wvParameters);
        }
        #endregion

        #region Sezione per scatenare dell'evento PlayerLeaveKinectSensor
        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnPlayerLeaveKinectSensor(PlayerLeaveKinectSensor.Arguments e)
        {
           
            if (PlayerLeaveKinectSensorEvent != null)
            {
                PlayerLeaveKinectSensorEvent(this, e);
            }
        }

        public void RaisePlayerLeaveKinectSensorEvent()
        {
            //TODO completare i parametri
            PlayerLeaveKinectSensor.Arguments wvParameters = new PlayerLeaveKinectSensor.Arguments();


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
            public class Arguments : EventArgs
            {

                private string attID;

                public Arguments(string ID) : base()
                {
                    attID = ID;
                }

                public string ID
                {
                    get { return attID; }
                }
                
            }

        }

        public class PlayerLeaveKinectSensor
        {
            public class Arguments : EventArgs
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
