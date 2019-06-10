import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/models/user';

@Injectable()
export class AuthHttpService{
    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){

    }

    logIn(username: string, password: string) : Observable<any>{

        return Observable.create((observer) => {

            let data = `username=${username}&password=${password}&grant_type=password`;
            let httpOptions={
                headers:{
                    "Content-type": "application/x-www-form-urlencoded"
                }
            }
            this.http.post<any>(this.base_url + "/oauth/token",data,httpOptions).subscribe(data => {

                localStorage.jwt = data.access_token;
                let jwtData = localStorage.jwt.split('.')[1]
                let decodedJwtJsonData = window.atob(jwtData)
                let decodedJwtData = JSON.parse(decodedJwtJsonData)

    
                let role = decodedJwtData.role
        
                console.log('jwtData: ' + jwtData)
                console.log('decodedJwtJsonData: ' + decodedJwtJsonData)
                console.log('decodedJwtData: ' + decodedJwtData)
                console.log('Role ' + role)
                localStorage.setItem("role", role);

                observer.next("uspesno");
                localStorage.setItem("loggedUser",username);
                observer.complete();
            },
            err => {
                console.log(err);
                observer.next("neuspesno");
                observer.complete();
            });
        });
     
    }

    logOut(): Observable<any>{
        return Observable.create((observer) => {
            localStorage.setItem("loggedUser",undefined);
            localStorage.jwt = undefined;
            localStorage.role = undefined;
        });
    }

    register(user: User) : Observable<any>{

        return Observable.create((observer) => {
            let data = user;
            let httpOptions={
                headers:{
                    "Content-type": "application/json"
                }
            }
            this.http.post<any>(this.base_url + "/api/Account/Register",data,httpOptions).subscribe(data => {
                observer.next("uspesno");
                observer.complete();
            },
            err => {
                console.log(err);
                observer.next("neuspesno");
                observer.complete();
            });
        });
     
    }
}