import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { AngularFireAuth } from '@angular/fire/compat/auth';
import { GoogleAuthProvider, GithubAuthProvider, FacebookAuthProvider} from '@angular/fire/auth';


@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private baseUrl: string = 'http://localhost:5039/api/'
  constructor(private http: HttpClient, private router: Router, private fireauth : AngularFireAuth) {}

  signUp(userObj: any) {
    return this.http.post<any>(`${this.baseUrl}user/register`, userObj);
  }

  login(loginObj: any) {
    return this.http.post<any>(`${this.baseUrl}user/authenticate`, loginObj);
  }
  companySignUp(companyObj: any) {
    return this.http.post<any>(
      `${this.baseUrl}company/companyRegister`,
      companyObj
    );
  }
  companyLogin(loginObj: any) {
    return this.http.post<any>(
      `${this.baseUrl}company/companyAuthenticate`,
      loginObj
    );
  }

  signOut(){
    localStorage.clear();
    this.router.navigate(['login'])
  }

  storeToken(tokenValue: string){
    localStorage.setItem('token', tokenValue)
  }

  getToken(){
    return localStorage.getItem('token')
  }

  isLoggedIn(): boolean{
    return !!localStorage.getItem('token')
  }

  //Sign in with Google
  googleSignIn() {
    return this.fireauth.signInWithPopup(new GoogleAuthProvider).then(res =>{

      this.router.navigate(['/dashboard']);
      localStorage.setItem('token', JSON.stringify(res.user?.uid));

    }, err => {
      alert(err.message);
    })
  }


}
