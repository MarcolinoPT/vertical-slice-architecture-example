import { BrowserModule } from '@angular/platform-browser';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgxSpinnerModule } from "ngx-spinner";

import { AppComponent } from '@app/app.component';
import { AlertService, UsersService } from '@app/_services';
import { TableOptionsComponent } from '@app/table-options/table-options.component';
import { UserComponent } from '@app/user-display/user-display.component';
import { UserEditComponent } from '@app/user-edit/user-edit.component';
import { ErrorInterceptor } from '@app/_helpers';
import { AlertComponent } from '@app/_components';

@NgModule({
  declarations: [
    AppComponent,
    TableOptionsComponent,
    UserComponent,
    UserEditComponent,
    AlertComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    ReactiveFormsModule,
    HttpClientModule,
    BrowserAnimationsModule,
    NgxSpinnerModule
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA],
  providers: [
    UsersService,
    AlertService,
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
