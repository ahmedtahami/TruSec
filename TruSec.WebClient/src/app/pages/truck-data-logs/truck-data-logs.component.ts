import { AfterViewInit, Component, Input, OnChanges, SimpleChanges } from '@angular/core';
import { MessageService } from 'primeng/api';
import { Table } from 'primeng/table';
import { TruckDataLog } from '../../models/TruckDataLog';
import { TruckDataLogService } from './truck-data-logs.service';

@Component({
  selector: 'app-truck-data-logs',
  templateUrl: './truck-data-logs.component.html',
  styleUrls: ['./truck-data-logs.component.css']
})
export class TruckDataLogsComponent implements OnChanges {
  @Input() truckId!: number;
  list: TruckDataLog[] = [];
  rowsPerPageOptions = [5, 10, 20];
  constructor(private messageService: MessageService, private truckDataLogService: TruckDataLogService) { }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes['truckId'] && changes['truckId'].currentValue) {
      this.getlist();
    }
  }

  getlist() {
    this.truckDataLogService.getTruckDataLogByTruck(this.truckId).subscribe((response: TruckDataLog[]) => {
      this.list = response.map(log => ({
        ...log,
        timeStamp: new Date(log.timeStamp ?? '')
      }));
    }, error => {
      this.messageService.add({ severity: 'error', summary: 'Error', detail: error.message });
    });
  }

  onGlobalFilter(table: Table, event: Event) {
    table.filterGlobal((event.target as HTMLInputElement).value, 'contains');
  }
}
