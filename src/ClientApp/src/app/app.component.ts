import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { NgxSpinnerService } from "ngx-spinner";

import { AlertService, UsersService } from '@app/_services';
import { User } from '@app/_models';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent implements OnInit {
  users: User[] = [];
  userSelected: User = {} as User;
  isEditing: boolean = false;

  form = this.fb.group({
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
  });

  constructor(
    private fb: FormBuilder,
    private usersService: UsersService,
    private spinner: NgxSpinnerService,
    private alert: AlertService
  ) { }

  ngOnInit() {
    this.spinner.show();
    this.usersService.getAll()
      .subscribe({
        next: result => {
          this.users = result!.users ?? this.users;
          this.spinner.hide();
        },
        error: _ => {
          this.spinner.hide();
          this.alert.error("Could not fetch users!");
        }
      });
  }

  selectUser(user: User) {
    if (Object.keys(this.userSelected).length === 0) {
      this.userSelected = user;
      this.isEditing = true;
      this.form.patchValue({
        firstName: user.firstName,
        lastName: user.lastName,
        apartmentNumber: user.apartmentNumber,
        dateOfBirth: user.dateOfBirth,
        houseNumber: user.houseNumber,
        phoneNumber: user.phoneNumber,
        postalCode: user.postalCode,
        streetName: user.streetName,
        town: user.town,
      });
    }
  }

  save() {
    this.alert.clear();
    this.spinner.show();
    // Create a new user
    if (this.userSelected.id === '-') {
      this.usersService.create(this.form.value)
        .subscribe({
          next: result => {
            this.users[0] = result;
            this.spinner.hide();
            this.alert.success('User created successfully', { keepAfterRouteChange: true });
          },
          error: _ => {
            this.spinner.hide();
            this.alert.error("Could not create new user!");
          }
        });
      // Update user
    } else {
      const index = this.users.map((user) => user.id)
        .indexOf(this.userSelected.id);
      this.usersService.update(this.userSelected.id, this.form.value)
        .subscribe({
          next: result => {
            this.users[index] = result;
            this.spinner.hide();
            this.alert.success('User updated successfully', { keepAfterRouteChange: true });
          },
          error: _ => {
            this.spinner.hide();
            this.alert.error("Could not update user!");
          }
        });
    }
    // clean up
    this.userSelected = {} as User;
    this.isEditing = false;
    this.form.reset();
  }

  softAddUser() {
    this.users.unshift({
      id: '-',
      firstName: '',
      lastName: '',
      streetName: '',
      houseNumber: '',
      apartmentNumber: '',
      postalCode: '',
      town: '',
      phoneNumber: '',
      dateOfBirth: '',
      age: 0,
    });
    // Set it as selected so we can edit the fields
    this.userSelected = this.users[0];
    // Allow buttons to insert/cancel new user
    this.isEditing = true;
  }
}
