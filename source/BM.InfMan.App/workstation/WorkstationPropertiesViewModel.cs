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
    using impl;
    using System;
    using System.ComponentModel;
    using System.Windows.Input;

    public class WorkstationPropertiesViewModel : INotifyPropertyChanged
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

        private PropertiesMode _propertiesMode;
        private WorkstationRepository _workstationRepository;

        public static WorkstationPropertiesViewModel ForAdd(
            WorkstationRepository workstationRepository)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            return new WorkstationPropertiesViewModel(
                PropertiesMode.Add,
                workstationRepository,
                null);
        }

        public static WorkstationPropertiesViewModel ForEdit(
            WorkstationRepository workstationRepository,
            Guid workstationId)
        {
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (workstationId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "workstationId"); }

            return new WorkstationPropertiesViewModel(
                PropertiesMode.Edit,
                workstationRepository,
                workstationId);
        }

        private WorkstationPropertiesViewModel(
            PropertiesMode propertiesMode,
            WorkstationRepository workstationRepository,
            Guid? workstationId)
        {
            if (propertiesMode == null) { throw new ArgumentNullException("propertiesMode"); }
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }

            _propertiesMode = propertiesMode;
            _workstationRepository = workstationRepository;

            this.CommitCommand = new RelayCommand(
                () => true,
                this.Commit);

            _propertiesMode.Apply(
                add: () =>
                {
                    this.WorkstationId = null;
                    this.Name = string.Empty;
                    this.KeyPath = string.Empty;
                },
                edit: () =>
                {
                    if (workstationId == null) { throw new ArgumentNullException("workstationId"); }

                    WorkstationEntity entity = _workstationRepository.WorkstationSingle(WorkstationFilter.ById(
                        workstationId.Value));

                    this.WorkstationId = workstationId;
                    this.Name = entity.Name;
                    this.KeyPath = entity.KeyPath;
                });
        }

        #region CommitCommand

        private ICommand _commitCommand = null;
        private bool _commitCommand_set = false;

        public ICommand CommitCommand
        {
            get
            {
                if (!_commitCommand_set)
                {
                    throw new InvalidOperationException("commitCommand not set.");
                }
                if (_commitCommand == null)
                {
                    throw new InvalidOperationException("commitCommand should not be null");
                }
                return _commitCommand;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "commitCommand cannot be null");
                }
                bool changing = !_commitCommand_set || _commitCommand != value;
                if (changing)
                {
                    _commitCommand_set = true;
                    _commitCommand = value;
                }
            }
        }

        private void ClearCommitCommand()
        {
            if (_commitCommand_set)
            {
                _commitCommand_set = false;
                _commitCommand = null;
            }
        }

        private bool HasCommitCommand()
        {
            return _commitCommand_set;
        }

        #endregion

        private Guid? WorkstationId { get; set; }

        #region Name

        private string _name;

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                this.OnPropertyChanged(nameof(this.Name));
            }
        }

        #endregion

        #region KeyPath

        private string _keyPath;

        public string KeyPath
        {
            get
            {
                return _keyPath;
            }
            set
            {
                _keyPath = value;
                this.OnPropertyChanged(nameof(this.KeyPath));
            }
        }

        #endregion

        private void Commit()
        {
            _propertiesMode.Apply(
                add: () =>
                {
                    _workstationRepository.WorkstationInsert(WorkstationFactory.Insert(
                        this.Name,
                        this.KeyPath));
                },
                edit: () =>
                {
                    _workstationRepository.WorkstationUpdate(WorkstationFactory.Update(
                        this.WorkstationId.Value,
                        this.Name,
                        this.KeyPath));
                });
        }
    }
}
