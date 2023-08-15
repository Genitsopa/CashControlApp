import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CompanyComponent } from './company/company.component'; // Import the CompanyComponent
import { CompanyLoginComponent } from './components/company-login/company-login.component';
import { CompanySignupComponent } from './components/company-signup/company-signup.component';
import { ConverterComponent } from './components/converter/converter.component';
import { HomeComponent } from './components/home/home.component';
import { ResetComponent } from './components/reset/reset.component';

const routes: Routes = [
  {
    path: '',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'companyLogin',
    component: CompanyLoginComponent,
  },
  {
    path: 'companySignup',
    component: CompanySignupComponent,
  },
  {
    path: 'dashboard',
    component: DashboardComponent, 
  },
  {
    path: 'company', 
    component: CompanyComponent, 
  },
  {
    path: 'converter',
    component: ConverterComponent,
  },
  {
    path:'reset',
    component: ResetComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
