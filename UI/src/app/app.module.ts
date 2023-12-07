import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './components/dashboard/dashboard.component';
import { CompanyComponent } from './company/company.component';
import { FormsModule } from '@angular/forms';
import { CompanyLoginComponent } from './components/company-login/company-login.component';
import { CompanySignupComponent } from './components/company-signup/company-signup.component'; // Import the FormsModule
import { ConverterComponent } from './components/converter/converter.component';
import { HomeComponent } from './components/home/home.component';
import { AngularFireModule } from '@angular/fire/compat'
import { environment } from 'src/environments/environment';
import { NgToastModule } from 'ng-angular-popup';
import { ResetComponent } from './components/reset/reset.component';
import { CompanyApiRequest } from './services/CompanyApiRequest.service';

@NgModule({

  declarations: [AppComponent,LoginComponent, SignupComponent, DashboardComponent, CompanyComponent, CompanyLoginComponent, CompanySignupComponent,ConverterComponent,HomeComponent, ResetComponent],

  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    NgToastModule,
    FormsModule,
    AngularFireModule.initializeApp(environment.firebase)

  ],
  providers: [
    CompanyApiRequest
    

  ],

  bootstrap: [AppComponent],
})
export class AppModule {}
