import { Component } from '@angular/core';
import { LayoutService } from "./service/app.layout.service";

@Component({
    selector: 'app-footer',
    templateUrl: './app.footer.component.html'
})
export class AppFooterComponent {
  date: any = new Date().getFullYear();
    constructor(public layoutService: LayoutService) { }
}
