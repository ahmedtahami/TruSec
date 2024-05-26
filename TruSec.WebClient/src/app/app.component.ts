import { Component } from '@angular/core';
import { SignalRService } from './services/signalR.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'TruSec.WebClient';
  public expression!: string;

  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener((data: string) => {
      this.expression = data;
    });
  }
}
