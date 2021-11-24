using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
                InitPersonNotRoomList();
                InitRemainRoomList();

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

        #region command
        public ICommand UpdatePersonListCommand { get; set; }
        public ICommand ToUpdateListCommand { get; set; }
        public ICommand AddPersonToRoomUI { get; set; }
        public ICommand RemovePersonFromRoomUI { get; set; }
        public ICommand CancelUpdatePersonListCommand { get; set; }
        public ICommand RemovePersonFromRoomCommand { get; set; }

        #endregion

        #region change room



        #endregion


        #endregion

        public QuarantinePersonInRoomViewModel() : base()
        {

            QuarantinePersonList = new ObservableCollection<QuarantinePerson>();
            PersonNotRoomList = new ObservableCollection<QuarantinePerson>();
            RemainRoomList = new ObservableCollection<Model.QuarantineRoom>();

            ToViewCommand = new RelayCommand<object>((p) =>
            {
                return true;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
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
                ResetToDeaultTabEditAfterEdit();
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
                var room = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == RoomID).FirstOrDefault();
                if (room == null) return false;

                if (QuarantinePersonList.Count >= room.capacity) return false;

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
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                RemoveFromRoomUI();
            });

            CancelCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                if (SelectedItem != null)
                {
                    InjectionRecordViewModel.RollbackTransaction(SelectedItem.id);
                    TestingResultViewModel.RollbackTransaction(SelectedItem.id);
                    DestinationHistoryViewModel.RollbackTransaction(SelectedItem.id);
                }
                p.Close();
            });

            CancelUpdatePersonListCommand = new RelayCommand<Window>((p) =>
            {
                return true;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                CompleteQuarantinePerson();
                RollbackTransaction();
                p.Close();
            });

            RemovePersonFromRoomCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                return false;
            }, (p) =>
            {
                BufferWindow bufferWindow = new BufferWindow();
                bufferWindow.ShowDialog();
                RemovePersonFromRoom();
            });
        }

        #region method

        protected override void ChangeRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    var NewRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == NewRoomSelected.id).FirstOrDefault();
                    if (NewRoom == null) return;

                    Person.roomID = NewRoom.id;

                    DataProvider.ins.db.SaveChanges();

                    RemoveFromRoomUI();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }
        }

        protected override void EditQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    QuarantinePerson Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    // Tạo địa chỉ hiện ở của người cách ly
                    Address PersonAddress = DataProvider.ins.db.Addresses.Where(x => x.id == Person.addressID).FirstOrDefault();

                    if (PersonAddress != null)
                    {
                        PersonAddress.apartmentNumber = QPApartmentNumber;
                        PersonAddress.streetName = QPStreetName;
                        PersonAddress.ward = QPSelectedWard;
                        PersonAddress.district = QPSelectedDistrict;
                        PersonAddress.province = QPSelectedProvince;

                        DataProvider.ins.db.SaveChanges();
                    }
                    else
                    {
                        PersonAddress = new Address()
                        {
                            apartmentNumber = QPApartmentNumber,
                            streetName = QPStreetName,
                            ward = QPSelectedWard,
                            district = QPSelectedDistrict,
                            province = QPSelectedProvince
                        };

                        if (PersonAddress.CheckValidateProperty())
                        {
                            DataProvider.ins.db.Addresses.Add(PersonAddress);
                            Person.addressID = PersonAddress.id;
                            DataProvider.ins.db.SaveChanges();
                        }
                    }


                    // Tạo thông tin sức khỏe
                    HealthInformation PersonHealthInformation = DataProvider.ins.db.HealthInformations.Where(x => x.quarantinePersonID == Person.id).FirstOrDefault();

                    if (PersonHealthInformation != null)
                    {
                        PersonHealthInformation.isCough = IsCough;
                        PersonHealthInformation.isDisease = IsDisease;
                        PersonHealthInformation.isFever = IsFever;
                        PersonHealthInformation.isLossOfTatse = IsLossOfTatse;
                        PersonHealthInformation.isTired = IsTired;
                        PersonHealthInformation.isOtherSymptoms = IsOtherSymptoms;
                        PersonHealthInformation.isShortnessOfBreath = IsShortnessOfBreath;
                        PersonHealthInformation.isSoreThroat = IsSoreThroat;

                        DataProvider.ins.db.SaveChanges();
                    }

                    // Tạo người cách ly
                    Person.name = QPName;
                    Person.dateOfBirth = QPDateOfBirth;
                    Person.sex = QPSelectedSex;
                    Person.citizenID = QPCitizenID;
                    Person.nationality = QPSelectedNationality;
                    Person.phoneNumber = QPPhoneNumber;
                    Person.healthInsuranceID = QPHealthInsuranceID;

                    if (QPSelectedLevel != null) Person.levelID = QPSelectedLevel.id;

                    DataProvider.ins.db.SaveChanges();

                    InitDisplayAddress(PersonAddress);
                    InitDisplayHealthInformation(PersonHealthInformation);

                    InjectionRecordViewModel.ApplyInjectionRecordToDB(Person.id, "EditOrDelete");
                    TestingResultViewModel.ApplyTestingResultToDb(Person.id, "EditOrDelete");
                    DestinationHistoryViewModel.ins.ApplayDestinationHistoryToDB(Person.id, "EditOrDelete");

                    transaction.Commit();

                    QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));


                    SelectedItem = Person;

                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }

        }

        void RemovePersonFromRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    Person.roomID = null;

                    RemoveFromRoomUI();

                    PersonNotRoomList = new ObservableCollection<QuarantinePerson>(PersonNotRoomList.OrderBy(x => x.id));

                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }
        }

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

        protected override void CompleteQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    Person.roomID = null;
                    Person.completeQuarantine = true;

                    RemoveFromRoomUI();

                    PersonNotRoomList = new ObservableCollection<QuarantinePerson>(PersonNotRoomList.OrderBy(x => x.id));

                    DataProvider.ins.db.SaveChanges();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }
        }

        public void CompeleteQuarantineRoom()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var p in QuarantinePersonList)
                    {
                        p.roomID = null;
                        p.completeQuarantine = true;
                    }

                    QuarantinePersonList.Clear();

                    DataProvider.ins.db.SaveChanges();

                    InitPersonNotRoomList();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }
        }

        public void ClearPersonList()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    foreach (var p in QuarantinePersonList)
                    {
                        p.roomID = null;
                    }

                    DataProvider.ins.db.SaveChanges();

                    QuarantinePersonList.Clear();

                    InitPersonNotRoomList();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    Window ErrorDialog = new Window
                    {
                        AllowsTransparency = true,
                        Background = Brushes.Transparent,
                        Width = 600,
                        Height = 400,
                        ResizeMode = ResizeMode.NoResize,
                        WindowStartupLocation = WindowStartupLocation.CenterScreen,
                        WindowStyle = WindowStyle.None,
                        Content = new FailNotification()
                    };
                    ErrorDialog.ShowDialog();
                }
            }
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
            InitPersonNotRoomList();
            if (SelectedItem != null) SelectedItem = QuarantinePersonList.Where(x => x.id == SelectedItem.id).FirstOrDefault();
        }

        void InitPersonNotRoomList() => PersonNotRoomList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == null && x.completeQuarantine != true));

        void InitRemainRoomList() => RemainRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms.Where(x => x.id != RoomID && x.QuarantinePersons.Count < x.capacity));
        #endregion
    }
}
