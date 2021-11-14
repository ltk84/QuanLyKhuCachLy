﻿using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace QuanLyKhuCachLy.ViewModel
{
    public class QuarantinePersonInRoomViewModel : QuarantinePersonViewModel
    {
        #region property

        private QuarantineRoomViewModel _Parent;
        public QuarantineRoomViewModel Parent
        {
            get => _Parent; set
            {
                _Parent = value;
                RoomID = _Parent.RoomID;
                OnPropertyChanged();
            }
        }

        private int _RoomID;
        public int RoomID
        {
            get => _RoomID; set
            {
                _RoomID = value;
                QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));
                OnPropertyChanged();
            }
        }

        private static QuarantinePersonInRoomViewModel _ins;
        public static QuarantinePersonInRoomViewModel ins
        {
            get
            {
                if (_ins == null) _ins = new QuarantinePersonInRoomViewModel();
                return _ins;
            }
            set => _ins = value;
        }

        private ObservableCollection<QuarantinePerson> _PersonNotRoomList;
        public ObservableCollection<QuarantinePerson> PersonNotRoomList
        {
            get => _PersonNotRoomList;
            set
            {
                _PersonNotRoomList = value;
                OnPropertyChanged();
            }
        }

        private QuarantinePerson _NotRoomSelectedItem;
        public QuarantinePerson NotRoomSelectedItem
        {
            get => _NotRoomSelectedItem;
            set
            {
                _NotRoomSelectedItem = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdatePersonListCommand { get; set; }
        public ICommand ToUpdateListCommand { get; set; }
        public ICommand AddPersonToRoomUI { get; set; }
        public ICommand RemovePersonFromRoomUI { get; set; }


        #endregion

        public QuarantinePersonInRoomViewModel()
        {

            QuarantinePersonList = new ObservableCollection<QuarantinePerson>();
            PersonNotRoomList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == null));

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Parent.ToPersonInformation();
            });

            ToMainCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                Parent.BackToRoomInformation();
            });

            ToEditCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                EditQuarantinePersonInRoom editScreen = new EditQuarantinePersonInRoom();
                editScreen.ShowDialog();
            });

            UpdatePersonListCommand = new RelayCommand<Window>((p) =>
            {

                return true;
            }, (p) =>
            {
                UpdateList();
                p.Close();
            });

            ToUpdateListCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                UpdatePersonListOfRoom UpdatePersonList = new UpdatePersonListOfRoom();
                UpdatePersonList.ShowDialog();
            });



            AddPersonToRoomUI = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                AddToRoomUI();
            });

            RemovePersonFromRoomUI = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                RemoveFromRoomUI();
            });

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                p.Close();
                RollbackTransaction();
            });
        }

        #region method

        void AddToRoomUI()
        {
            QuarantinePersonList.Add(NotRoomSelectedItem);
            PersonNotRoomList.Remove(NotRoomSelectedItem);
        }
        void RemoveFromRoomUI()
        {
            PersonNotRoomList.Add(SelectedItem);
            QuarantinePersonList.Remove(SelectedItem);
        }

        void UpdateList()
        {
            List<QuarantinePerson> ListInDB = new List<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));

            foreach (var p in QuarantinePersonList)
            {
                if (!ListInDB.Contains(p))
                {
                    p.roomID = RoomID;
                    DataProvider.ins.db.SaveChanges();
                }
            }

            foreach (var pInDB in ListInDB)
            {
                if (!QuarantinePersonList.Contains(pInDB))
                {
                    pInDB.roomID = null;
                    DataProvider.ins.db.SaveChanges();
                }
            }

            QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));
        }

        void RollbackTransaction()
        {
            DataProvider.ins.db.ChangeTracker.Entries().Where(e => e.Entity != null).ToList().ForEach(e => e.State = EntityState.Detached);
            QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));
        }
        #endregion
    }
}
