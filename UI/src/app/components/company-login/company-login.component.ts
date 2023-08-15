import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Router } from '@angular/router';
import validateAllForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './company-login.component.html',
  styleUrls: ['./company-login.component.css'],
})
export class CompanyLoginComponent implements OnInit {
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  companyLoginForm!: FormGroup;
  message: string = 'message';
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.companyLoginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  hideShowPass() {
    this.isText = !this.isText;

    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }

  onLogin() {
    if (this.companyLoginForm.valid) {
      console.log(this.companyLoginForm.value);

      this.auth.companyLogin(this.companyLoginForm.value).subscribe({
        next: () => {
          this.companyLoginForm.reset();

          this.router.navigate(['company']);
        },
        error: (err) => {
          alert(err?.error.message);
        },
      });
    } else {
      validateAllForm.validateAllFormFields(this.companyLoginForm);
      alert('Your form is invalid');
      //throw error using toster with required fields
    }
  }
}