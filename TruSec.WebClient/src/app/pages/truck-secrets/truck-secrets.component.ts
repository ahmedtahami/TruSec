import { Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Truck } from '../../models/Truck';
import { TruckSecret } from '../../models/TruckSecret';
import { TruckSecretService } from './truck-secrets.service';

@Component({
  selector: 'app-truck-secrets',
  templateUrl: './truck-secrets.component.html',
  styleUrls: ['./truck-secrets.component.css']
})
export class TruckSecretsComponent implements OnChanges {
  @Input() truck!: Truck;
  truckSecrets: TruckSecret[] = [];
  truckSecret: TruckSecret = {};
  truckSecretDialogForEdit: boolean = false;
  truckSecretDialogForNew: boolean = false;
  truckSecretDialogForImport: boolean = false;
  deleteTruckSecretDialog: boolean = false;
  rowsPerPageOptions = [5, 10, 20];
  importDto: { file?: File } = {};

  constructor(private messageService: MessageService, private truckSecretService: TruckSecretService) { }
  ngOnChanges(changes: SimpleChanges): void {
    if (changes['truck'] && changes['truck'].currentValue) {
      this.getTruckSecrets();
    }
  }

  getTruckSecrets() {
    debugger;
    if (this.truck && this.truck.id) {
      this.truckSecretService.getTruckSecretsByTruck(this.truck.id).subscribe((response: TruckSecret[]) => {
        this.truckSecrets = response;
      }, error => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openNew() {
    this.truckSecret = {};
    this.truckSecretDialogForNew = true;
  }

  hideDialogForNew() {
    this.truckSecretDialogForNew = false;
  }

  create(form: NgForm) {
    if (form.valid) {
      this.truckSecret.truckId = this.truck.id;
      this.truckSecretService.addTruckSecret(this.truckSecret).subscribe((response: any) => {
        this.truckSecretDialogForNew = false;
        this.getTruckSecrets();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'truckSecret added successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openEdit(truckSecret: TruckSecret) {
    this.truckSecret = { ...truckSecret };
    this.truckSecretDialogForEdit = true;
  }

  hideDialogForEdit() {
    this.truckSecretDialogForEdit = false;
  }

  update(form: NgForm) {
    if (form.valid && this.truckSecret && this.truckSecret.id) {
      this.truckSecretService.updateTruckSecret(this.truckSecret.id, this.truckSecret).subscribe(() => {
        this.truckSecretDialogForEdit = false;
        this.getTruckSecrets();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'truckSecret updated successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openDelete(truckSecret: TruckSecret) {
    this.truckSecret = { ...truckSecret };
    this.deleteTruckSecretDialog = true;
  }

  confirmDelete() {
    this.deleteTruckSecretDialog = false;
    if (this.truckSecret && this.truckSecret.id) {
      this.truckSecretService.deleteTruckSecret(this.truckSecret.id).subscribe(() => {
        this.getTruckSecrets();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'truckSecret deleted successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
}
