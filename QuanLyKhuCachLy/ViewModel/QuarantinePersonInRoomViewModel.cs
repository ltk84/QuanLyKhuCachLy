using QuanLyKhuCachLy.CustomUserControl;
using QuanLyKhuCachLy.Model;
using QuanLyKhuCachLy.Utility;
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
        public ICommand AddAllPersonToRoomUICommand { get; set; }
        public ICommand RemoveAllPersonFromRoomCommand { get; set; }

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
                //BufferWindow bufferWindow = new BufferWindow();
                //bufferWindow.ShowDialog();
                //CompleteQuarantinePerson();
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

            AddAllPersonToRoomUICommand = new RelayCommand<object>((p) =>
            {
                var CurrentRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == RoomID).FirstOrDefault();
                if (CurrentRoom == null) return false;

                var RoomCapacity = CurrentRoom.capacity;

                if (PersonNotRoomList.Count > 0 && RoomCapacity > QuarantinePersonList.Count) return true;
                return false;
            }, (p) =>
            {
                AddAllPersonToRoom();
            });

            RemoveAllPersonFromRoomCommand = new RelayCommand<Window>((p) =>
            {
                if (QuarantinePersonList.Count > 0) return true;
                return false;
            }, (p) =>
            {
                RemoveAllPersonFromRoom();
            });

            ToChangeCompleteDateCommand = new RelayCommand<Window>((p) =>
            {
                if (SelectedItem != null)
                    return true;
                return false;
            }, (p) =>
            {
                ChangeCompleteDate changeCDDialog = new ChangeCompleteDate();
                changeCDDialog.DataContext = this;
                ConstraintDate = QPArrivedDate;

                changeCDDialog.ShowDialog();
            });
        }

        #region method

        protected override void ChangeCompleteDate(Window p)
        {
            base.ChangeCompleteDate(p);

            RemovePersonFromRoom();

            SortPersonList();

            Parent.BackToRoomInformation();
        }

        void AddAllPersonToRoom()
        {
            var CurrentRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == RoomID).FirstOrDefault();
            if (CurrentRoom == null) return;

            var RoomCapacity = CurrentRoom.capacity;
            var PersonInRoomCount = QuarantinePersonList.Count;

            var diff = RoomCapacity - PersonInRoomCount;

            if (diff > PersonNotRoomList.Count)
            {
                foreach (var p in PersonNotRoomList)
                {
                    QuarantinePersonList.Add(p);
                }

                PersonNotRoomList.Clear();
            }
            else
            {
                for (int i = 0; i < RoomCapacity - PersonInRoomCount; i++)
                {
                    QuarantinePersonList.Add(PersonNotRoomList[i]);
                }

                for (int i = 0; i < RoomCapacity - PersonInRoomCount; i++)
                {
                    if (PersonNotRoomList.Count > 0)
                        PersonNotRoomList.Remove(PersonNotRoomList.First());
                }
            }

            SortNotRoomPersonList();
            SortPersonList();
        }

        void RemoveAllPersonFromRoom()
        {
            foreach (var p in QuarantinePersonList)
            {
                PersonNotRoomList.Add(p);
            }

            QuarantinePersonList.Clear();

            SortNotRoomPersonList();
            SortPersonList();
        }

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

                    QuarantinePersonList.Remove(SelectedItem);

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
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

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
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

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
                    ErrorDialog.ShowDialog();
                }
            }
        }

        void AddToRoomUI()
        {
            QuarantinePersonList.Add(NotRoomSelectedItem);
            PersonNotRoomList.Remove(NotRoomSelectedItem);

            SortPersonList();
        }
        void RemoveFromRoomUI()
        {
            PersonNotRoomList.Add(SelectedItem);
            QuarantinePersonList.Remove(SelectedItem);

            SortNotRoomPersonList();
        }

        protected override void CompleteQuarantinePerson()
        {
            using (var transaction = DataProvider.ins.db.Database.BeginTransaction())
            {
                try
                {
                    var Person = DataProvider.ins.db.QuarantinePersons.Where(x => x.id == SelectedItem.id).FirstOrDefault();
                    if (Person == null) return;

                    if (Person.arrivedDate > DateTime.Today) { throw new InvalidOperationException(); }
                    if (Person.leaveDate > DateTime.Today) Person.leaveDate = DateTime.Today;
                    Person.roomID = null;

                    RemoveFromRoomUI();

                    PersonNotRoomList = new ObservableCollection<QuarantinePerson>(PersonNotRoomList.OrderBy(x => x.id));

                    DataProvider.ins.db.SaveChanges();

                    Parent.BackToRoomInformation();

                    transaction.Commit();
                }
                catch (DbUpdateException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
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
                        if (p.arrivedDate > DateTime.Today)
                        {
                            throw new InvalidOperationException();
                        }
                        p.leaveDate = DateTime.Today;
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

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
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

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi cơ sở dữ liệu cập nhật";
                    ErrorDialog.ShowDialog();
                }
                catch (DbEntityValidationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi xác thực";
                    ErrorDialog.ShowDialog();
                }
                catch (NotSupportedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi database không hỗ trợ";
                    ErrorDialog.ShowDialog();
                }
                catch (ObjectDisposedException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi đối tượng database bị hủy";
                    ErrorDialog.ShowDialog();
                }
                catch (InvalidOperationException e)
                {
                    transaction.Rollback();
                    RollbackTransaction();

                    CustomUserControl.FailNotification ErrorDialog = new CustomUserControl.FailNotification();
                    var FailNotificationVM = ErrorDialog.DataContext as FailNotificationViewModel;
                    FailNotificationVM.Content = "Lỗi thao tác không hợp lệ";
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
            DBUtilityTracker.Rollback();
            QuarantinePersonList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == RoomID));
            InitPersonNotRoomList();
            SelectedItem = null;
        }

        void InitPersonNotRoomList()
        {
            var CurrentRoom = DataProvider.ins.db.QuarantineRooms.Where(x => x.id == RoomID).FirstOrDefault();
            if (CurrentRoom == null) return;

            var RoomSeverity = DataProvider.ins.db.Severities.Where(x => x.id == CurrentRoom.levelID).FirstOrDefault();
            if (RoomSeverity == null)
                PersonNotRoomList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == null && x.leaveDate > DateTime.Today && x.levelID == null));
            else
                PersonNotRoomList = new ObservableCollection<QuarantinePerson>(DataProvider.ins.db.QuarantinePersons.Where(x => x.roomID == null && x.leaveDate > DateTime.Today && x.levelID == RoomSeverity.id));

        }

        void InitRemainRoomList() => RemainRoomList = new ObservableCollection<Model.QuarantineRoom>(DataProvider.ins.db.QuarantineRooms.Where(x => x.id != RoomID && x.QuarantinePersons.Count < x.capacity));

        void SortPersonList() => QuarantinePersonList = new ObservableCollection<QuarantinePerson>(QuarantinePersonList.OrderBy(x => x.id));

        void SortNotRoomPersonList() => PersonNotRoomList = new ObservableCollection<QuarantinePerson>(PersonNotRoomList.OrderBy(x => x.id));

        #endregion
    }
}
