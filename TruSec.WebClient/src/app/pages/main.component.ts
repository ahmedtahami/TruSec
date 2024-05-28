import { Component } from '@angular/core';
import { SignalRService } from '../services/signalR.service';
import * as L from 'leaflet';

@Component({
  selector: 'app-dashboard',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.css']
})
export class MainComponent {
  public expression!: string;
  private map!: L.Map;
  private marker!: L.Marker;
  public latitude: number = 2233.2222;
  public longitude: number = 3.44;
  constructor(private signalRService: SignalRService) { }

  ngOnInit() {
    this.initMap();
    this.signalRService.startConnection();
    this.signalRService.addTransferDataListener((data: any) => {
      debugger;
      this.expression = data.expression ?? '';
      this.latitude = data.latitude ?? NaN;
      this.longitude = data.longitude ?? NaN;
      this.updateMarker();
    });
  }

  private initMap(): void {
    this.map = L.map('map').setView([0, 0], 2); // Initial map view

    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      maxZoom: 18,
    }).addTo(this.map);

    this.marker = L.marker([0, 0]).addTo(this.map)
      .bindPopup('Truck Location');
  }

  private updateMarker(): void {
    if (this.marker) {
      this.marker.setLatLng([this.latitude, this.longitude]);
      this.map.setView([this.latitude, this.longitude], 13);
      this.marker.getPopup()?.setContent(`Truck Location<br>Expression: ${this.expression}`);
      this.marker.openPopup();
    }
  }
}
