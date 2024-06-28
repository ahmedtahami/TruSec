import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { AccountService } from '../services/account.service';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {
  userId: string = '';
  token: string = '';
  loading: boolean = true;
  message: string = '';
  errorMessage: string = '';
  showPassword: boolean = false;
  showConfirmPassword: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private accountService: AccountService
  ) { }

  ngOnInit(): void {
    this.route.queryParams.subscribe(params => {
      this.userId = params['userId'];
      this.token = params['token'];
      this.loading = false;
    });
  }

  togglePasswordVisibility(): void {
    this.showPassword = !this.showPassword;
  }

  toggleConfirmPasswordVisibility(): void {
    this.showConfirmPassword = !this.showConfirmPassword;
  }

  onSubmit(form: NgForm): void {
    if (form.invalid || form.value.password !== form.value.confirmPassword) {
      return;
    }
    this.loading = true;
    const { password } = form.value;
    this.accountService.resetPassword({ userId: this.userId, token: this.token, password }).subscribe(
      response => {
        this.message = 'Password reset successfully';
        this.loading = false;
      },
      error => {
        this.errorMessage = `Error: ${error.message}`;
        this.loading = false;
      }
    );
  }

  redirectToLogin(): void {
    this.router.navigate(['/login']);
  }
}
