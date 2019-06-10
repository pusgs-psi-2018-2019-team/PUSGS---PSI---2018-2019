import { Component, OnInit } from '@angular/core';
import { AuthHttpService } from 'src/services/http/auth.service';
import { Router } from '@angular/router';
import { NgForm, FormBuilder, Validators } from '@angular/forms';
import { User } from '../models/user';

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
    PhoneNumber: ['', Validators.required],
    ImageUrl: [''],
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

  public imagePath;
  imgURL: any;
  public msg: string;
 
  preview(files) {
    if (files.length === 0)
      return;
 
    var mimeType = files[0].type;
    if (mimeType.match(/image\/*/) == null) {
      this.msg = "Only images are supported.";
      return;
    }
 
    var reader = new FileReader();
    this.imagePath = files;
    reader.readAsDataURL(files[0]); 
    reader.onload = (_event) => { 
      console.log(reader);
      this.imgURL = reader.result; 
    }
  }

}
