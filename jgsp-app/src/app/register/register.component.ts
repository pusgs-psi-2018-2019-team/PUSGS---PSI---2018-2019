import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from 'src/services/http/auth.service';
import { Router } from '@angular/router';
import { User } from '../user';
import { NgForm, FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent implements OnInit {

  registacijaForm = this.fb.group({
    Name: ['', Validators.required],
    Surname: ['', Validators.required],
    Username: ['', Validators.required],
    Password: ['', Validators.required],
    ConfirmPassword: ['', Validators.required],
    Email: ['', Validators.required],
    Address: ['', Validators.required],
    Date: ['', Validators.required]
  });
  
  constructor(private http:AuthHttpService, private router: Router, private fb: FormBuilder) { }

  ngOnInit() {
  }

  register(){
    let regModel: User = this.registacijaForm.value;
    this.http.register(regModel).subscribe(temp => {
      if(temp == "uspesno")
      {
        console.log(temp);
        this.router.navigate(["/home"])
      }
      else if(temp == "neuspesno")
      {
        console.log(temp);
        this.router.navigate(["/login"])
      }
    });
  }

}
