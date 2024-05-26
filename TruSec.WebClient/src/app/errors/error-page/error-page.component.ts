import { Component } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, NavigationEnd, Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { faArrowRightToBracket } from '@fortawesome/free-solid-svg-icons'
import { filter } from 'rxjs';

@Component({
  selector: 'app-error-page',
  templateUrl: './error-page.component.html',
  styleUrls: ['./error-page.component.css']
})
export class ErrorPageComponent {
  public errorPage = {
    statusCode: 404,
    description: 'Whoops. What you are trying to find is not available.',
    button: 'Go To Home',
    route: '/'
  };
  faArrowRightToBracket = faArrowRightToBracket;
  constructor(
    private titleService: Title,
    private route: ActivatedRoute,
  ) {
    this.route.data.subscribe((response: any) => {
      this.errorPage.statusCode = response.statusCode ? response.statusCode : this.errorPage.statusCode;
      this.errorPage.description = response.description ? response.description : this.errorPage.description;
      this.errorPage.button = response.button;
      this.errorPage.route = response.route;
    });
  }

  ngOnInit(): void {
    this.titleService.setTitle(`${this.errorPage.statusCode} | ${environment.appName}`);
  }
}
