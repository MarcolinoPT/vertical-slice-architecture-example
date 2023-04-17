import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';

import { User } from '@app/_models';
import { UsersService } from '@app/_services/users.service';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlertService } from '../_services';

@Component({
  selector: 'table-options',
  templateUrl: './table-options.component.html'
})
export class TableOptionsComponent {
  @Input() users: User[] = [];
  @Output() usersChange = new EventEmitter<User[]>();
  @Input() userSelected: User = {} as User;
  @Output() userSelectedChange = new EventEmitter<User>();
  @Input() isEditing: boolean = false;
  @Output() isEditingChange = new EventEmitter<boolean>();
  @Input() form = this.fb.group({
    firstName: ['', [Validators.required]],
    lastName: ['', [Validators.required]],
    streetName: ['', [Validators.required]],
    houseNumber: ['', [Validators.required]],
    apartmentNumber: ['', []],
    postalCode: ['', [Validators.required]],
    town: ['', [Validators.required]],
    phoneNumber: ['', [Validators.required]],
    dateOfBirth: [
      '',
      [Validators.required, Validators.pattern('^[0-9]{4}-[0-9]{2}-[0-9]{2}$')],
    ],
  })
  @Output() formChange = new EventEmitter<any>();

  constructor(private fb: FormBuilder,
    private usersService: UsersService,
    private spinner: NgxSpinnerService,
    private alert: AlertService
  ) {
    this.fb = fb;
  }

  cancel() {
    this.alert.clear();
    this.spinner.show();
    if (this.userSelected.id === '-') {
      this.users.splice(0, 1);
    }
    this.userSelected = {} as User;
    this.isEditing = false;
    this.form.reset();
    this.userSelectedChange.emit(this.userSelected);
    this.isEditingChange.emit(this.isEditing);
    this.formChange.emit(this.form);
    this.usersService.getAll()
      .subscribe({
        next: result => {
          this.users = result!.users ?? this.users;
          this.usersChange.emit(this.users);
          this.spinner.hide();
        },
        error: _ => {
          this.spinner.hide();
          this.alert.error("Could not fetch users!");
        }
      });
  }
}
