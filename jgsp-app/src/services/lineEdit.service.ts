import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable()
export class LineEditHttpService{
    base_url = "http://localhost:52295"
    constructor(private http: HttpClient){ }

    
}