import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { User } from 'src/app/user';

@Injectable()
export class AuthHttpService{
    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){ }

    redVoznjeVremena(username: string, password: string) : Observable<any>{

        return Observable.create((observer) => {

            let data = `username=${username}&password=${password}&grant_type=password`;
            let httpOptions={
                headers:{
                    "Content-type": "application/x-www-form-urlencoded"
                }
            }
            this.http.post<any>(this.base_url + "api/RedVoznje",data,httpOptions).subscribe(data => {
                localStorage.jwt = data.access_token;
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

}