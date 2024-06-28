import { Component } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { ApplicationUser } from '../../models/ApplicationUser';
import { ApplicationUserRegistrationDto } from '../../models/ApplicationUserRegistrationDto';
import { UserService } from './users.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent {
  users: ApplicationUser[] = [];
  user: ApplicationUserRegistrationDto = {};
  userDialogForNew: boolean = false;
  deleteUserDialog: boolean = false;
  rowsPerPageOptions = [5, 10, 20];
  importDto: { file?: File } = {};
  roles: { name: string }[] = [
    { "name": "Company Admin" },
    { "name": "Monitoring Staff" },
    { "name": "Super Admin" },
  ];
  constructor(private messageService: MessageService, private userService: UserService) { }

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers() {
    this.userService.getAll().subscribe((response: ApplicationUser[]) => {
      this.users = response;
    }, error => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
    });
  }

  openNew() {
    this.user = {};
    this.userDialogForNew = true;
  }

  hideDialogForNew() {
    this.userDialogForNew = false;
  }

  create(form: NgForm) {
    if (form.valid) {
      this.userService.addApplicationUser(this.user).subscribe((response: any) => {
        this.userDialogForNew = false;
        this.getUsers();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'User added successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openDelete(user: ApplicationUser) {
    this.user = { ...user };
    this.deleteUserDialog = true;
  }

  confirmDelete() {

  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
}
