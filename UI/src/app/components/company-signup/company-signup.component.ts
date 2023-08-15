import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators,
} from '@angular/forms';
import { Route, Router } from '@angular/router';
import validateAllForm from 'src/app/helpers/validateform';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './company-signup.component.html',
  styleUrls: ['./company-signup.component.css'],
})
export class CompanySignupComponent implements OnInit {
  type: string = 'password';
  isText: boolean = false;
  eyeIcon: string = 'fa-eye-slash';
  companySignUpForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.companySignUpForm = this.fb.group({
      companyName: ['', Validators.required],
      userName: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
  hideShowPass() {
    this.isText = !this.isText;
    this.isText ? (this.eyeIcon = 'fa-eye') : (this.eyeIcon = 'fa-eye-slash');
    this.isText ? (this.type = 'text') : (this.type = 'password');
  }

  onSignup() {
    if (this.companySignUpForm.valid) {
      this.auth.companySignUp(this.companySignUpForm.value).subscribe({
        next: () => {
          this.companySignUpForm.reset();
          this.router.navigate(['companyLogin']);
        },
        error: (err) => {
          alert(err?.error.message);
        },
      });
    } else {
      validateAllForm.validateAllFormFields(this.companySignUpForm);
    }
  }
}
