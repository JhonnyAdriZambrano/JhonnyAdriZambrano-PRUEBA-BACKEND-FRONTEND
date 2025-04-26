import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { MainRoutingModule } from './main-routing.module';
import { LayoutComponent } from './layout/layout.component';
import { DashboardModule } from '../dashboard/dashboard.module'; 


@NgModule({
  declarations: [
    LayoutComponent,
  ],
  imports: [
    CommonModule,
    MainRoutingModule,
    DashboardModule
  ]
})
export class MainModule { }
