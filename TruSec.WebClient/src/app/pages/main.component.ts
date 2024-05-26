import { Component } from '@angular/core';
import { SignalRService } from '../services/signalR.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  public expression!: string;

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener((data: string) => {
      this.expression = data;
    });
  }
}
