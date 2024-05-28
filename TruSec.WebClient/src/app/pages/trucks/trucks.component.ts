import { Component } from '@angular/core';
import { Truck } from '../../models/Truck';
import { MessageService } from 'primeng/api';
import { TrucksService } from './trucks.service';
import { CompaniesService } from '../companies/companies.service';
import { NgForm } from '@angular/forms';
import { Table } from 'primeng/table';
import { Company } from '../../models/Company';

@Component({
  selector: 'app-trucks',
  templateUrl: './trucks.component.html',
  styleUrls: ['./trucks.component.css']
})
export class TrucksComponent {
  trucks: Truck[] = [];
  companies: Company[] = [];
  truck: Truck = {
    company: { id: undefined }
  };
  truckDialogForEdit: boolean = false;
  truckDialogForNew: boolean = false;
  truckDialogForImport: boolean = false;
  deleteTruckDialog: boolean = false;
  rowsPerPageOptions = [5, 10, 20];
  importDto: { file?: File } = {};

  constructor(private messageService: MessageService, private trucksService: TrucksService, private companiesService: CompaniesService) { }

  ngOnInit(): void {
    this.getTrucks();
    this.companiesService.getAllCompanies().subscribe((res: any) => {
      this.companies = res;
    });
  }

  getTrucks() {
    this.trucksService.getAllTrucks().subscribe((response: Truck[]) => {
      this.trucks = response;
    }, error => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
    });
  }

  openNew() {
    this.truck = {
      company: {
        id: undefined
      }
    };
    this.truckDialogForNew = true;
  }

  hideDialogForNew() {
    this.truckDialogForNew = false;
  }

  create(form: NgForm) {
    if (form.valid) {
      this.trucksService.addTruck(this.truck).subscribe((response: any) => {
        this.truckDialogForNew = false;
        this.getTrucks();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Truck added successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openEdit(truck: Truck) {
    this.truck = { ...truck };
    this.truckDialogForEdit = true;
  }

  hideDialogForEdit() {
    this.truckDialogForEdit = false;
  }

  update(form: NgForm) {
    if (form.valid && this.truck && this.truck.id) {
      this.trucksService.updateTruck(this.truck.id, this.truck).subscribe(() => {
        this.truckDialogForEdit = false;
        this.getTrucks();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Truck updated successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openDelete(truck: Truck) {
    this.truck = { ...truck };
    this.deleteTruckDialog = true;
  }

  confirmDelete() {
    this.deleteTruckDialog = false;
    if (this.truck && this.truck.id) {
      this.trucksService.deleteTruck(this.truck.id).subscribe(() => {
        this.getTrucks();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Truck deleted successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
}
