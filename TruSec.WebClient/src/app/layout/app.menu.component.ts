import { OnInit } from '@angular/core';
import { Component } from '@angular/core';
import { LayoutService } from './service/app.layout.service';
import * as fonts from '@fortawesome/free-solid-svg-icons';
import * as f1 from '@fortawesome/fontawesome-svg-core'
@Component({
  selector: 'app-menu',
  templateUrl: './app.menu.component.html'
})
export class AppMenuComponent implements OnInit {

  model: any[] = [];

  constructor(public layoutService: LayoutService) { }

  ngOnInit() {
    this.model = [
      {
        label: 'Home',
        items: [
          { label: 'Dashboard', icon: fonts.faHome, routerLink: ['/dashboard'] }
        ]
      },

    ];
  }
}
