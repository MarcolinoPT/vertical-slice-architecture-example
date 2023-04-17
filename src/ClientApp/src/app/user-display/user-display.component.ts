import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder } from '@angular/forms';

import { User } from '@app/_models';
import { AlertService, UsersService } from '@app/_services';
import { NgxSpinnerService } from 'ngx-spinner';

@Component({
  selector: '[user-display]',
  templateUrl: './user-display.component.html',
})
export class UserComponent {
  @Input() userIndex: number = -1;
  @Input() users: User[] = [];
  @Output() usersChange = new EventEmitter<User[]>();
  @Input() userSelected: User = {} as User;
  @Output() userSelectedChange = new EventEmitter<User>();
  @Input() user: User = {} as User;
  @Input() isEditing: boolean = false;
  @Output() isEditingChange = new EventEmitter<boolean>();

  constructor(private fb: FormBuilder,
    private usersService: UsersService,
    private spinner: NgxSpinnerService,
    private alert: AlertService
  ) { }

  deleteUser(index: number, event: Event) {
    event.stopPropagation();
    this.alert.clear();
    if (confirm('Are you sure you want to delete this user?')) {
      this.spinner.show();
      this.usersService.delete(this.users[index].id)
        .subscribe(
          {
            next: _ => {
              this.users.splice(index, 1);
              this.usersChange.emit(this.users);
              this.spinner.hide();
              this.alert.success('User deleted successfully');
            },
            error: _ => {
              this.alert.error("Could not delete user");
            }
          }
        );
    } else {
      this.userSelected = {} as User;
      this.userSelectedChange.emit(this.userSelected);
      this.isEditing = false;
      this.isEditingChange.emit(this.isEditing);
    }
  }
}
