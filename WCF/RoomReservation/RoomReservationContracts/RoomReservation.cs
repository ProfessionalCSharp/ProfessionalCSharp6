using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace Wrox.ProCSharp.WCF.Contracts
{
    [DataContract(Namespace = "http://www.cninnovation.com/Services/2012")]
    public class RoomReservation : INotifyPropertyChanged
    {
        private int _id;

        [DataMember]
        public int Id
        {
            get { return _id; }
            set { SetProperty(ref _id, value); }
        }

        private string _roomName;

        [DataMember]
        [StringLength(30)]
        public string RoomName
        {
            get { return _roomName; }
            set { SetProperty(ref _roomName, value); }
        }

        private DateTime _startTime;

        [DataMember]
        public DateTime StartTime
        {
            get { return _startTime; }
            set { SetProperty(ref _startTime, value); }
        }

        private DateTime _endTime;

        [DataMember]
        public DateTime EndTime
        {
            get { return _endTime; }
            set { SetProperty(ref _endTime, value); }
        }

        private string _contact;

        [DataMember]
        [StringLength(30)]
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }

        private string _text;

        [DataMember]
        [StringLength(50)]
        public string Text
        {
            get { return _text; }
            set { SetProperty(ref _text, value); }
        }

        protected virtual void OnNotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void SetProperty<T>(ref T item, T value, [CallerMemberName] string propertyName = null)
        {
            if (!EqualityComparer<T>.Default.Equals(item, value))
            {
                item = value;
                OnNotifyPropertyChanged(propertyName);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
