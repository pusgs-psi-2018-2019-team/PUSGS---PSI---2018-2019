import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { RouterModule,Routes} from '@angular/router';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { AuthHttpService } from 'src/services/http/auth.service';
import { TokenInterceptor } from './interceptors/token.interceptor';
import { HomeComponent } from './home/home.component';
import { RegisterComponent } from './register/register.component';
import { UiModule } from './ui/ui.module';
import { ReactiveFormsModule } from '@angular/forms';
import { RedvoznjeComponent } from './redvoznje/redvoznje.component';
import { RedVoznjeHttpService } from 'src/services/redvoznje.service';
import { HeaderAdminComponent } from './ui/header-admin/header-admin.component';
import { HeaderLogedInComponent } from './ui/header-loged-in/header-loged-in.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { CenovnikHttpService } from '../services/cenovnik.service';
import { ProfilComponent } from './profil/profil.component';
import { ProfilHttpService } from '../services/profil.service';

const routes : Routes = [
  {path : "login", component: LoginComponent},
  {path : "home", component: HomeComponent},
  {path : "register", component: RegisterComponent},
  {path : "redvoznje", component: RedvoznjeComponent},
  {path : "cenovnik", component: CenovnikComponent},
  {path : "profil", component: ProfilComponent},
  {path : "", component: HomeComponent, pathMatch:"full"},
  {path : "**", redirectTo: "login"},
]

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    HomeComponent,
    RegisterComponent,
    RedvoznjeComponent,
    HeaderAdminComponent,
    HeaderLogedInComponent,
    CenovnikComponent,
    ProfilComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    RouterModule.forRoot(routes),  
    ReactiveFormsModule,
    UiModule  
  ],
  providers: [{provide: HTTP_INTERCEPTORS, useClass: TokenInterceptor, multi: true}, AuthHttpService, RedVoznjeHttpService,CenovnikHttpService,ProfilHttpService],
  bootstrap: [AppComponent]
})
export class AppModule { }
