import { Component, Input } from '@angular/core';
import { FormGroup } from '@angular/forms';

import { User } from '@app/_models';

@Component({
  selector: '[user-edit]',
  templateUrl: './user-edit.component.html',
})
export class UserEditComponent {
  @Input() user: User = {} as User;
  @Input()
  parentGroup!: FormGroup;
}
