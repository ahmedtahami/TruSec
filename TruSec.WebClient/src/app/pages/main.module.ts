import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { CommonModule } from '@angular/common';
import { MessageService } from 'primeng/api';
import { AppLayoutModule } from '../layout/app.layout.module';
import { AppLayoutComponent } from '../layout/app.layout.component';
import { RouterModule, Routes } from '@angular/router';
import { MainComponent } from './main.component';
import { ButtonModule } from 'primeng/button';
import { InputTextModule } from 'primeng/inputtext';
import { FormsModule } from '@angular/forms';
import { CheckboxModule } from 'primeng/checkbox';
import { PasswordModule } from 'primeng/password';
import { BadgeModule } from 'primeng/badge';
import { TableModule } from 'primeng/table';
import { RippleModule } from 'primeng/ripple';
import { TooltipModule } from 'primeng/tooltip';
import { DropdownModule } from 'primeng/dropdown';
import { RadioButtonModule } from 'primeng/radiobutton';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputSwitchModule } from 'primeng/inputswitch';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { ToastModule } from 'primeng/toast';
import { ToolbarModule } from 'primeng/toolbar';
import { DialogModule } from 'primeng/dialog';
import { FileUploadModule } from 'primeng/fileupload';
import { TabViewModule } from 'primeng/tabview';
import { ImageModule } from 'primeng/image';
import { ColorPickerModule } from 'primeng/colorpicker';
import { CompaniesComponent } from './companies/companies.component';
import { TrucksComponent } from './trucks/trucks.component';
import { TruckSecretsComponent } from './truck-secrets/truck-secrets.component';
import { TruckDataLogsComponent } from './truck-data-logs/truck-data-logs.component';
import { UserCompaniesComponent } from './user-companies/user-companies.component';

const routes: Routes = [
  {
    path: '',
    component: AppLayoutComponent,
    children: [
      {
        path: 'dashboard',
        component: MainComponent,
      },
      {
        path: 'companies',
        component: CompaniesComponent,
      },
      {
        path: 'trucks',
        component: TrucksComponent,
      },
      {
        path: 'secrets',
        component: TruckSecretsComponent,
      },
      {
        path: 'datalogs',
        component: TruckDataLogsComponent,
      },
    ],
  }
];


@NgModule({
  declarations: [
    MainComponent,
    CompaniesComponent,
    TrucksComponent,
    TruckSecretsComponent,
    TruckDataLogsComponent,
    UserCompaniesComponent
  ],
  imports: [
    BrowserModule,
    RouterModule.forChild(routes),
    AppLayoutModule,
    CommonModule,
    ButtonModule,
    CheckboxModule,
    InputTextModule,
    FormsModule,
    PasswordModule,
    TableModule,
    FormsModule,
    ButtonModule,
    RippleModule,
    ToastModule,
    ToolbarModule,
    TooltipModule,
    InputTextModule,
    DropdownModule,
    RadioButtonModule,
    InputNumberModule,
    DialogModule,
    BadgeModule,
    InputSwitchModule,
    CalendarModule,
    FileUploadModule,
    MultiSelectModule,
    TabViewModule,
    ImageModule,
    ColorPickerModule
  ],
  providers: [MessageService]
})
export class PagesModule { }
