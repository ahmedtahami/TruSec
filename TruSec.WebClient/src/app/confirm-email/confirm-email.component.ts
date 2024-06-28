import { Component } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-confirm-email',
  templateUrl: './confirm-email.component.html',
  styleUrls: ['./confirm-email.component.css']
})
export class ConfirmEmailComponent {
  message!: string;
  errorMessage!: string;
  loading: boolean = true;
  constructor(
    private route: ActivatedRoute,
    private accountService: AccountService,
    private router: Router
  ) { }

  ngOnInit(): void {
    debugger;
    this.route.queryParams.subscribe((params: any) => {
      const userId = params['userId'];
      const token = params['token'];
      this.confirmEmail(userId, token);
    });
  }

  confirmEmail(userId: string, token: string): void {
    this.accountService.confirmEmail(userId, token).subscribe(
      response => {
        this.message = response.message;
        this.loading = false;
      },
      error => {
        this.errorMessage = `Error: ${error?.message ?? 'Unknown Error Occured'}`;
        this.loading = false;
      }
    );
  }

  redirectToLogin() {
    this.router.navigate(['/login'])
  }
}
