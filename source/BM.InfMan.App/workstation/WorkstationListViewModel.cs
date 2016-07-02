/*
    Infrastructure Manager
	http://bren.cc/infman

	Copyright (c) 2016, Brendon Matheson

    This program is free software; you can redistribute it and/or modify it under the terms of the GNU General Public
    License as published by the Free Software Foundation; either version 2 of the License, or (at your option) any
    later version.

    This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied
    warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more
    details.

    You should have received a copy of the GNU General Public License along with this program; if not, write to the
    Free Software Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.
*/

namespace cc.bren.infman.workstation
{
    using cc.bren.infman.framework;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Windows.Input;

    public class WorkstationListViewModel : INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(sender, e);
            }
        }

        private void OnPropertyChanged(
            string propertyName)
        {
            if (propertyName == null) { throw new ArgumentNullException("propertyName"); }

            this.OnPropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion

        private WorkstationRepository _workstationRepository;
        private UserInterfaceService _userInterfaceService;

        public WorkstationListViewModel(
            WorkstationRepository workstationRepository,
            UserInterfaceService userInterfaceService)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (userInterfaceService == null) { throw new ArgumentNullException("userInterfaceService"); }

            _workstationRepository = workstationRepository;
            _userInterfaceService = userInterfaceService;

            this.AddCommand = new RelayCommand(
                () => true,
                this.Add);
            this.EditCommand = new RelayCommand(
                () => this.SelectedWorkstation != null,
                this.Edit);
            this.RemoveCommand = new RelayCommand(
                () => this.SelectedWorkstation != null,
                this.Remove);
            this.CloseCommand = new RelayCommand(
                () => true,
                this.Close);

            this.Workstations = new ObservableCollection<WorkstationListItemViewModel>();
            this.SelectedWorkstation = null;

            this.Refresh();

            this.PropertyChanged += WorkstationListViewModel_PropertyChanged;
        }

        public ICommand AddCommand { get; private set; }

        public ICommand EditCommand { get; private set; }

        public ICommand RemoveCommand { get; private set; }

        public ICommand CloseCommand { get; private set; }

        public ObservableCollection<WorkstationListItemViewModel> Workstations { get; private set; }

        #region SelectedWorkstation

        private WorkstationListItemViewModel _selectedWorkstation;

        public WorkstationListItemViewModel SelectedWorkstation
        {
            get
            {
                return _selectedWorkstation;
            }
            set
            {
                _selectedWorkstation = value;
                this.OnPropertyChanged(nameof(SelectedWorkstation));
            }
        }

        #endregion

        private void Refresh()
        {
            IList<WorkstationEntity> workstations = _workstationRepository.WorkstationList(WorkstationFilter.All());
            this.Workstations.Clear();
            this.Workstations.AddRange(workstations, e => e.ToListItemViewModel());
            this.SelectedWorkstation = null;
        }

        private void Add()
        {
            _userInterfaceService.WorkstationAdd(
                _workstationRepository);
        }

        private void Edit()
        {
            _userInterfaceService.WorkstationEdit(
                _workstationRepository,
                this.SelectedWorkstation.WorkstationId);
        }

        private void Remove()
        {
            _workstationRepository.WorkstationDelete(this.SelectedWorkstation.ToEntity());
            this.Workstations.Remove(this.SelectedWorkstation);
            this.SelectedWorkstation = null;
        }

        private void Close()
        {
            Console.WriteLine("Close");
        }

        private void WorkstationListViewModel_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "SelectedWorkstation")
            {
                // TODO: This seems wrong
                ((RelayCommand)this.AddCommand).Refresh();
                ((RelayCommand)this.EditCommand).Refresh();
                ((RelayCommand)this.RemoveCommand).Refresh();
            }
        }
    }
}
