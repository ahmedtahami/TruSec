import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { Company } from '../../models/Company';
import { CompaniesService } from './companies.service';

@Component({
  selector: 'app-companies',
  templateUrl: './companies.component.html',
  styleUrls: ['./companies.component.css']
})
export class CompaniesComponent implements OnInit {
  companies: Company[] = [];
  company: Company = {};
  companyDialogForEdit: boolean = false;
  companyDialogForNew: boolean = false;
  companyDialogForImport: boolean = false;
  deleteCompanyDialog: boolean = false;
  rowsPerPageOptions = [5, 10, 20];
  importDto: { file?: File } = {};

  constructor(private messageService: MessageService, private companyService: CompaniesService) { }

  ngOnInit(): void {
    this.getCompanies();
  }

  getCompanies() {
    this.companyService.getAllCompanies().subscribe((response: Company[]) => {
      this.companies = response;
    }, error => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
    });
  }

  openNew() {
    this.company = {};
    this.companyDialogForNew = true;
  }

  hideDialogForNew() {
    this.companyDialogForNew = false;
  }

  create(form: NgForm) {
    if (form.valid) {
      this.companyService.addCompany(this.company).subscribe((response: any) => {
        this.companyDialogForNew = false;
        this.getCompanies();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Company added successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openEdit(company: Company) {
    this.company = { ...company };
    this.companyDialogForEdit = true;
  }

  hideDialogForEdit() {
    this.companyDialogForEdit = false;
  }

  update(form: NgForm) {
    if (form.valid && this.company && this.company.id) {
      this.companyService.updateCompany(this.company.id, this.company).subscribe(() => {
        this.companyDialogForEdit = false;
        this.getCompanies();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Company updated successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  openDelete(company: Company) {
    this.company = { ...company };
    this.deleteCompanyDialog = true;
  }

  confirmDelete() {
    this.deleteCompanyDialog = false;
    if (this.company && this.company.id) {
      this.companyService.deleteCompany(this.company.id).subscribe(() => {
        this.getCompanies();
        this.messageService.add({ severity: 'success', summary: 'Successful', detail: 'Company deleted successfully', life: 3000 });
      }, (error: any) => {
        this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
      });
    }
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
}
