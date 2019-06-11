import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class TimetableEditHttpService{
    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){ }

    getAll() : Observable<any>{
        return Observable.create((observer) => {    
            this.http.get<any>(this.base_url + "/api/TimetableEdit").subscribe(data =>{
                observer.next(data);
                observer.complete();     
            })             
        });
    }
}