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

namespace cc.bren.infman.framework
{
    using System;
    using System.Windows.Input;

    public class RelayCommand : ICommand
    {
        #region CanExecuteChanged

        public event EventHandler CanExecuteChanged;

        private void OnCanExecuteChanged()
        {
            if (this.CanExecuteChanged != null)
            {
                this.CanExecuteChanged(this, EventArgs.Empty);
            }
        }

        #endregion

        private Func<bool> _canExecute;
        private Action _action;

        public RelayCommand(
            Func<bool> canExecute,
            Action action)
        {
            if (canExecute == null) { throw new ArgumentNullException("canExecute"); }
            if (action == null) { throw new ArgumentNullException("action"); }

            _canExecute = canExecute;
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute();
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public void Refresh()
        {
            // TODO: This seems wrong
            this.OnCanExecuteChanged();
        }
    }
}
