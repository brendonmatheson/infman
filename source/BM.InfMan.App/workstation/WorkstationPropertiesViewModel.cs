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
    using cc.bren.infman.framework.eventing;
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

        private EventRouter _er;
        private PropertiesMode _propertiesMode;
        private WorkstationRepository _workstationRepository;
        private Action _closeAction;

        public static WorkstationPropertiesViewModel ForAdd(
            EventRouter er,
            WorkstationRepository workstationRepository,
            Action closeAction)
        {
            if (er == null) { throw new ArgumentNullException("er"); }
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (closeAction == null) { throw new ArgumentNullException("closeAction"); }

            return new WorkstationPropertiesViewModel(
                er,
                PropertiesMode.Add,
                workstationRepository,
                closeAction,
                null);
        }

        public static WorkstationPropertiesViewModel ForEdit(
            EventRouter er,
            WorkstationRepository workstationRepository,
            Action closeAction,
            Guid workstationId)
        {
            if (er == null) { throw new ArgumentNullException("er"); }
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (closeAction == null) { throw new ArgumentNullException("closeAction"); }
            if (workstationId == Guid.Empty) { throw new ArgumentException("Value cannot be empty.", "workstationId"); }

            return new WorkstationPropertiesViewModel(
                er,
                PropertiesMode.Edit,
                workstationRepository,
                closeAction,
                workstationId);
        }

        private WorkstationPropertiesViewModel(
            EventRouter er,
            PropertiesMode propertiesMode,
            WorkstationRepository workstationRepository,
            Action closeAction,
            Guid? workstationId)
        {
            if (er == null) { throw new ArgumentNullException("er"); }
            if (propertiesMode == null) { throw new ArgumentNullException("propertiesMode"); }
            if (workstationRepository == null) { throw new ArgumentNullException("workstationRepository"); }
            if (closeAction == null) { throw new ArgumentNullException("closeAction"); }

            _er = er;
            _propertiesMode = propertiesMode;
            _workstationRepository = workstationRepository;
            _closeAction = closeAction;

            this.CommitCommand = new RelayCommand(
                () => true,
                this.Commit);

            this.CancelCommand = new RelayCommand(
                () => true,
                this.Cancel);

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

        #region CancelCommand

        private ICommand _cancelCommand = null;
        private bool _cancelCommand_set = false;

        public ICommand CancelCommand
        {
            get
            {
                if (!_cancelCommand_set)
                {
                    throw new InvalidOperationException("cancelCommand not set.");
                }
                if (_cancelCommand == null)
                {
                    throw new InvalidOperationException("cancelCommand should not be null");
                }
                return _cancelCommand;
            }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "cancelCommand cannot be null");
                }
                bool changing = !_cancelCommand_set || _cancelCommand != value;
                if (changing)
                {
                    _cancelCommand_set = true;
                    _cancelCommand = value;
                }
            }
        }

        private void ClearCancelCommand()
        {
            if (_cancelCommand_set)
            {
                _cancelCommand_set = false;
                _cancelCommand = null;
            }
        }

        private bool HasCancelCommand()
        {
            return _cancelCommand_set;
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

                    _er.Fire<WorkstationEditedEvent>(new WorkstationEditedEvent(this.WorkstationId.Value));
                });

            _closeAction();
        }

        private void Cancel()
        {
            _closeAction();
        }
    }
}
