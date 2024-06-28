import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from './app.component';
import { ErrorPageComponent } from './errors/error-page/error-page.component';
import { PagesModule } from './pages/main.module';
import { NgxSpinnerModule } from 'ngx-spinner';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { ConfirmEmailComponent } from './confirm-email/confirm-email.component';
import { ResetPasswordComponent } from './reset-password/reset-password.component';
import { FormsModule } from '@angular/forms';

const routes: Routes =
  [
    {
      path: 'confirm-email',
      component: ConfirmEmailComponent
    },
    {
      path: 'reset-password',
      component: ResetPasswordComponent
    },
    {
      path: '500',
      component: ErrorPageComponent,
      data: { statusCode: '500', description: 'Whoops. Looks like something went wrong. Please visit us later.', button: null, route: null }
    },
    {
      path: '401',
      component: ErrorPageComponent,
      data: {
        statusCode: '401', description: 'Whoops. You do not have the necessary permisions.', button: 'Go To Home', route: '/'
      }
    },
    {
      path: '404',
      component: ErrorPageComponent,
      data: { statusCode: '404', description: 'Whoops. What you are trying to find is not available.', button: 'Go To Home', route: '/' }
    },
    {
      path: '**',
      redirectTo: '/404'
    },
  ];

@NgModule({
  declarations: [
    AppComponent,
    ErrorPageComponent,
    ConfirmEmailComponent,
    ResetPasswordComponent,
  ],
  imports: [
    BrowserModule,
    RouterModule.forRoot(routes, {
      initialNavigation: 'enabledBlocking',
      useHash: true
    }),
    PagesModule,
    FormsModule,
    NgxSpinnerModule,
    FontAwesomeModule
  ],
  providers: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
