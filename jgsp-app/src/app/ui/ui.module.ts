import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LayoutComponent } from './layout/layout.component';
import { FooterComponent } from './footer/footer.component';
import { HeaderComponent } from './header/header.component';

@NgModule({
  declarations: [LayoutComponent, HeaderComponent, FooterComponent],
  exports: [LayoutComponent],
  imports: [
    CommonModule
  ]
})
export class UiModule { }
